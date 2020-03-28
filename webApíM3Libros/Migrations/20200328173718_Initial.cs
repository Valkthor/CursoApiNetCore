using Microsoft.EntityFrameworkCore.Migrations;

namespace webApíM3Libros.Migrations
{
    //esto se crea en forma automatica, se ejecuta el siguiente comando en el administrador de paquetes:
    // Add-Migration Initial
    public partial class Initial : Migration
    {
        //se indica que se va a crear una tabla
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Autores",
                // sabe el nombre de estas columnas porque se declararon en las entities de la clase autor
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
                },
                // por defecto siempre que una columna se llame ID, la va a colocar como PK
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Autores");
        }
    }
}
