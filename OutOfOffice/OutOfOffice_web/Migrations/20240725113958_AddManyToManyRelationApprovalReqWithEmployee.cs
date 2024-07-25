using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutOfOffice_web.Migrations
{
    /// <inheritdoc />
    public partial class AddManyToManyRelationApprovalReqWithEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_Employees_ApproverId",
                table: "ApprovalRequests");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalRequests_ApproverId",
                table: "ApprovalRequests");

            migrationBuilder.CreateTable(
                name: "ApprovalRequestEmployee",
                columns: table => new
                {
                    ApprovalRequestsId = table.Column<int>(type: "int", nullable: false),
                    ApproversId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalRequestEmployee", x => new { x.ApprovalRequestsId, x.ApproversId });
                    table.ForeignKey(
                        name: "FK_ApprovalRequestEmployee_ApprovalRequests_ApprovalRequestsId",
                        column: x => x.ApprovalRequestsId,
                        principalTable: "ApprovalRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApprovalRequestEmployee_Employees_ApproversId",
                        column: x => x.ApproversId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequestEmployee_ApproversId",
                table: "ApprovalRequestEmployee",
                column: "ApproversId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalRequestEmployee");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequests_ApproverId",
                table: "ApprovalRequests",
                column: "ApproverId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_Employees_ApproverId",
                table: "ApprovalRequests",
                column: "ApproverId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
