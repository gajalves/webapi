using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    //[Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "É necessário informar o campo nome")]
        [MaxLength(80)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É necessário informar o campo descricao")]
        [MaxLength(300)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "É necessário informar o campo preco")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "É necessário informar o campo imageurl")]
        [MaxLength(300)]
        public string ImageUrl { get; set; }

        public float Estoque { get; set; }

        public DateTime DataCadastro { get; set; }

        public Categoria Categoria { get; set; }

        public int CategoriaId { get; set; }
    }
}
