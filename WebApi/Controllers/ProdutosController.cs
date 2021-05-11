using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext __context;

        public ProdutosController(AppDbContext contexto)
        {
            __context = contexto;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutos()
        {
            return __context.Produtos.AsNoTracking().ToList();
        }

        [HttpGet("{prodId}", Name = "GetProduto")]
        public ActionResult<Produto> GetProdutoById(int prodId)
        {
            Produto prod = __context.Produtos.AsNoTracking().Where(produto => produto.ProdutoId == prodId).FirstOrDefault();

            if (prod == null)
            {
                return NotFound();
            }
            else
            {
                return prod;
            }
        }

        [HttpPost]
        public ActionResult PostProduto([FromBody]Produto prod)
        {
            // Valida os dados, não necessário pois faz automático já
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            __context.Produtos.Add(prod);
            __context.SaveChanges();

            return new CreatedAtRouteResult("GetProduto", 
                new { id = prod.ProdutoId }, prod
                );
        }

        [HttpPut("{prodId}")]
        public ActionResult PutProduto(int prodId, [FromBody] Produto prod)
        {

            if ((prodId != prod.ProdutoId) || (prodId == 0 || prod.ProdutoId == 0)) { return BadRequest(); }


            Produto ProdAtualiza = __context.Produtos.AsNoTracking().Where(produto => produto.ProdutoId == prodId).FirstOrDefault();

            if (ProdAtualiza != null)
            {
                // Modifica tudo
                __context.Entry(prod).State = EntityState.Modified;
                var save = __context.SaveChanges();

                if (save == 1)
                {
                    return Ok(
                        new
                        {
                            Success = true,
                            Message = "Produto " + prodId + " atualizado com sucesso!"
                        }
                    );
                }
                else
                {
                    return BadRequest();
                }
            }

            return BadRequest(
                new 
                {
                    Message = "Produto Id invalido"
                }
                );            
        }


        [HttpDelete("{prodId}")]
        public ActionResult RemoveProduto(int prodId)
        {
            
            Produto ProdRemove = __context.Produtos.AsNoTracking().Where(produto => produto.ProdutoId == prodId).FirstOrDefault();

            if (ProdRemove != null)
            {                
                __context.Produtos.Remove(ProdRemove);
                var save = __context.SaveChanges();

                if (save == 1)
                {
                    return Ok(
                        new
                        {
                            Success = true,
                            Message = "Produto " + prodId + " excluido com sucesso!"
                        }
                    ); ;
                }
                else
                {
                    return BadRequest();
                }
            }

            return BadRequest(
                new
                {
                    Success = false,
                    Message = "Produto Id invalido"
                }
                );
        }

    }
}
