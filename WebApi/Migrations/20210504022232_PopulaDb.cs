using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class PopulaDb : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("insert into \"Categorias\" (\"Nome\", \"ImageUrl\") values('Bebidas'," +
                "'https://images.unsplash.com/photo-1550496149-4cae86a0b466?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1350&q=80')");

            mb.Sql("insert into \"Categorias\" (\"Nome\", \"ImageUrl\") values('Comidas'," +
                "'https://images.unsplash.com/photo-1511690656952-34342bb7c2f2?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=700&q=80')");

            mb.Sql("insert into \"Categorias\" (\"Nome\", \"ImageUrl\") values('Doces'," +
                "'https://images.unsplash.com/photo-1519915028121-7d3463d20b13?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=634&q=80')");

            //---------------------------//
            
            mb.Sql("insert into \"Produtos\"(\"Nome\", \"Descricao\", \"Preco\", \"ImageUrl\", \"Estoque\", \"DataCadastro\", \"CategoriaId\") values('Coca cola', 'Refrigerante de cola 350ml', '5.45', " +
                "'https://araujo.vteximg.com.br/arquivos/ids/4042618-1000-1000/07894900010015.jpg', 50, now(), " +
                "(Select \"CategoriaId\" from \"Categorias\" where \"Nome\" = 'Bebidas'))");

            mb.Sql("insert into \"Produtos\" (\"Nome\", \"Descricao\", \"Preco\", \"ImageUrl\", \"Estoque\", \"DataCadastro\", \"CategoriaId\") values('Macarrao', 'Massa branca', '15.00', " +
                "'https://images.unsplash.com/photo-1572441713132-c542fc4fe282?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=700&q=80', 20, now(), " +
                "(Select \"CategoriaId\" from \"Categorias\" where \"Nome\" =  'Comidas'))");

            mb.Sql("insert into \"Produtos\" (\"Nome\", \"Descricao\", \"Preco\", \"ImageUrl\", \"Estoque\", \"DataCadastro\", \"CategoriaId\") values('Torta', 'Torta top 300g', '7.55', " +
                "'https://images.unsplash.com/photo-1533134242443-d4fd215305ad?ixid=MnwxMjA3fDB8MHxzZWFyY2h8MTB8fHBpZXxlbnwwfHwwfHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=60', 50, now(), " +
                "(Select \"CategoriaId\" from \"Categorias\" where \"Nome\" =  'Doces'))");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
            mb.Sql("Delete from Produtos");
        }
    }
}
