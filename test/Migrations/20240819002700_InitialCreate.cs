using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cod_State = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Municipalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cod_Municipality = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Cod_State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipalities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Municipalities_States_Cod_State",
                        column: x => x.Cod_State,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cod_Parish = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Cod_Municipality = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parishes_Municipalities_Cod_Municipality",
                        column: x => x.Cod_Municipality,
                        principalTable: "Municipalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Centers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cod_Center = table.Column<int>(type: "int", nullable: false),
                    Cod_Parish = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Centers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Centers_Parishes_Cod_Parish",
                        column: x => x.Cod_Parish,
                        principalTable: "Parishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PollingStations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cod_PollingStation = table.Column<int>(type: "int", nullable: false),
                    PollingNumber = table.Column<int>(type: "int", nullable: false),
                    ValidVotes = table.Column<int>(type: "int", nullable: false),
                    NullVotes = table.Column<int>(type: "int", nullable: false),
                    EG = table.Column<int>(type: "int", nullable: false),
                    NM = table.Column<int>(type: "int", nullable: false),
                    LB = table.Column<int>(type: "int", nullable: false),
                    JABE = table.Column<int>(type: "int", nullable: false),
                    JOBR = table.Column<int>(type: "int", nullable: false),
                    AE = table.Column<int>(type: "int", nullable: false),
                    CF = table.Column<int>(type: "int", nullable: false),
                    DC = table.Column<int>(type: "int", nullable: false),
                    EM = table.Column<int>(type: "int", nullable: false),
                    BERA = table.Column<int>(type: "int", nullable: false),
                    Record = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Cod_Center = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollingStations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollingStations_Centers_Cod_Center",
                        column: x => x.Cod_Center,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Centers_Cod_Parish",
                table: "Centers",
                column: "Cod_Parish");

            migrationBuilder.CreateIndex(
                name: "IX_Municipalities_Cod_State",
                table: "Municipalities",
                column: "Cod_State");

            migrationBuilder.CreateIndex(
                name: "IX_Parishes_Cod_Municipality",
                table: "Parishes",
                column: "Cod_Municipality");

            migrationBuilder.CreateIndex(
                name: "IX_PollingStations_Cod_Center",
                table: "PollingStations",
                column: "Cod_Center");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PollingStations");

            migrationBuilder.DropTable(
                name: "Centers");

            migrationBuilder.DropTable(
                name: "Parishes");

            migrationBuilder.DropTable(
                name: "Municipalities");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
