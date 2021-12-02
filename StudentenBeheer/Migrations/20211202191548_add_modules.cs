using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentenBeheer.Migrations
{
    public partial class add_modules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inschrijvingen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    InschrijvingsDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AfgelegdOp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Resultaat = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inschrijvingen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inschrijvingen_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inschrijvingen_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inschrijvingen_ModuleId",
                table: "Inschrijvingen",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Inschrijvingen_StudentId",
                table: "Inschrijvingen",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inschrijvingen");

            migrationBuilder.DropTable(
                name: "Module");
        }
    }
}
