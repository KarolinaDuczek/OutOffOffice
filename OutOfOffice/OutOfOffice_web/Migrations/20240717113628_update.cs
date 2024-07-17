using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutOfOffice_web.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_PeoplePartners_PeoplePartnerId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "PeoplePartners");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PeoplePartnerId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PeoplePartnerId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "PeoplePartner",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeoplePartner",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "PeoplePartnerId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PeoplePartners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeoplePartners", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PeoplePartnerId",
                table: "Employees",
                column: "PeoplePartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_PeoplePartners_PeoplePartnerId",
                table: "Employees",
                column: "PeoplePartnerId",
                principalTable: "PeoplePartners",
                principalColumn: "Id");
        }
    }
}
