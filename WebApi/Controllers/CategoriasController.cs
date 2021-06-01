using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("v1")]
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly AppDbContext __context;
        private readonly ILogger __logger;

        public CategoriasController (AppDbContext context, ILogger<CategoriasController> log)
        {
            __context = context;
            __logger = log;            
        }

        [Route("BuscaCategorias")]
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetCategorias([BindRequired] bool produtos)
        {            
            List<Categoria> categorias = new List<Categoria>();
            __logger.LogInformation("Controller: " + this.ControllerContext.RouteData.Values["action"].ToString());
            try
            {
                if (produtos)
                {
                    categorias = __context.Categorias.Include(prod => prod.Produtos).ToList();
                }
                else
                {
                    categorias = __context.Categorias.AsNoTracking().ToList();
                }                                

                return Ok(new
                {
                    Status = "Ok",
                    Categorias = categorias
                });
            }
            catch (Exception ex)
            {
                __logger.LogInformation("Exception: " + ex.StackTrace);
                return StatusCode(500, new { Message = ex });
            }
            
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<Categoria>> GetCategorias()
        //{
        //    var categorias = __context.Categorias.AsNoTracking().ToList();
        //    return Ok(new { 
        //        Status = "Ok",
        //        Categorias = categorias
        //    });
        //}

        [HttpGet("{idCategoria}", Name = "GetCategoria")]
        public ActionResult<Categoria> GetById(int idCategoria)
        {      
            try
            {
                var categoria = __context.Categorias.AsNoTracking().Where(categoria => categoria.CategoriaId == idCategoria).FirstOrDefault();

                if (categoria == null) { return NotFound(new { Message = $"Categoria com id {idCategoria} não foi encontrada"}); }

                return Ok(new
                {
                    Status = "Ok",
                    Categoria = categoria
                });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = ex });
            }
            
        }

        [HttpPost]
        public ActionResult PostCategoria([FromBody] Categoria categoria)
        {
            try
            {
                __context.Categorias.Add(categoria);
                __context.SaveChanges();

                return new CreatedAtRouteResult("GetCategoria", new { idCategoria = categoria.CategoriaId }, categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.InnerException.Message });
            }
            
        }

        [HttpPut("{idCategoria}")]
        public ActionResult PutCategoria(int idCategoria, [FromBody] Categoria categoria)
        {
            if (idCategoria != categoria.CategoriaId)
            {
                return BadRequest();
            }

            var categoriaAtualiza = __context.Categorias.AsNoTracking().Where(categoria => categoria.CategoriaId == idCategoria).FirstOrDefault();

            if (categoriaAtualiza == null)
            {
                return NotFound();
            }

            __context.Entry(categoria).State = EntityState.Modified;
            var save = __context.SaveChanges();

            if (save == 1)
            {
                return Ok(
                       new
                       {
                           Success = true,
                           Message = "Categoria " + idCategoria + " atualizada com sucesso!"
                       }
                   );
            }

            return BadRequest();
        }

        [HttpDelete("{idCategoria}")]
        public ActionResult DeleteCategoria(int idCategoria)
        {
            var categoriaRemove = __context.Categorias.AsNoTracking().Where(categoria => categoria.CategoriaId == idCategoria).FirstOrDefault();

            if (categoriaRemove == null)
            {
                return NotFound();
            }
            else
            {
                __context.Remove(categoriaRemove);
                var save = __context.SaveChanges();

                if (save == 1)
                {
                    return Ok(
                        new
                        {
                            Success = true,
                            Message = "Categoria " + idCategoria + " excluida com sucesso!"
                        }
                    ); ;
                }
                else
                {
                    return BadRequest();
                }
            }            
        }
    }
}
