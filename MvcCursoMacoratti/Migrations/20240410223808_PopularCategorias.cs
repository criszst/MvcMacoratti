using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcMacorattiLanchesMac.Migrations
{
    public partial class PopularCategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaId, CategoriaNome, Descricao) " +
                "VALUES(1, 'Normal','Lanche feito com ingredientes normais')");

            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaId, CategoriaNome,Descricao) " +
                "VALUES(2, 'Natural','Lanche feito com ingredientes integrais e naturais')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
