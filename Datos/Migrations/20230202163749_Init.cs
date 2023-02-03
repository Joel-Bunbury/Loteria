using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carta",
                columns: table => new
                {
                    CartaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lema = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marcado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carta", x => x.CartaID);
                });

            migrationBuilder.CreateTable(
                name: "Tabla",
                columns: table => new
                {
                    TablaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabla", x => x.TablaID);
                });

            migrationBuilder.CreateTable(
                name: "CartaTabla",
                columns: table => new
                {
                    CartasCartaID = table.Column<int>(type: "int", nullable: false),
                    TablasTablaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartaTabla", x => new { x.CartasCartaID, x.TablasTablaID });
                    table.ForeignKey(
                        name: "FK_CartaTabla_Carta_CartasCartaID",
                        column: x => x.CartasCartaID,
                        principalTable: "Carta",
                        principalColumn: "CartaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartaTabla_Tabla_TablasTablaID",
                        column: x => x.TablasTablaID,
                        principalTable: "Tabla",
                        principalColumn: "TablaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartaTabla_TablasTablaID",
                table: "CartaTabla",
                column: "TablasTablaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartaTabla");

            migrationBuilder.DropTable(
                name: "Carta");

            migrationBuilder.DropTable(
                name: "Tabla");
        }
    }
}
