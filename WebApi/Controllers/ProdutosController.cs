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

        [HttpGet("{id}")]
        public ActionResult<Produto> GetProdutoById(int id)
        {
            Produto prod = __context.Produtos.AsNoTracking().Where(produto => produto.ProdutoId == id).FirstOrDefault();

            if (prod == null)
            {
                return NotFound();
            }
            else
            {
                return prod;
            }
        }
    }
}
