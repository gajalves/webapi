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
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext __context;

        public CategoriasController (AppDbContext context)
        {
            __context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = __context.Categorias.AsNoTracking().ToList();
            return Ok(new { 
                Status = "Ok",
                Categorias = categorias
            });
        }

        [HttpGet("{idCategoria}", Name = "GetCategoria")]
        public ActionResult<Categoria> GetById(int idCategoria)
        {            
            var categoria = __context.Categorias.AsNoTracking().Where(categoria => categoria.CategoriaId == idCategoria).FirstOrDefault();

            if (categoria == null) { return NotFound(); }

            return Ok(new
            {
                Status = "Ok",
                Categoria = categoria
            });
        }

        [HttpPost]
        public ActionResult PostCategoria([FromBody] Categoria categoria)
        {
            __context.Categorias.Add(categoria);
            __context.SaveChanges();

            return new CreatedAtRouteResult("GetCategoria", new  { idCategoria = categoria.CategoriaId }, categoria);
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

            return Ok();
        }
    }
}
