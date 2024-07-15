using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutOfOffice_web.Migrations
{
    /// <inheritdoc />
    public partial class AddEntitiesRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectMenager",
                table: "Projects",
                newName: "ProjectManagerId");

            migrationBuilder.RenameColumn(
                name: "LeaveRequest",
                table: "ApprovalRequests",
                newName: "LeaveRequestId");

            migrationBuilder.RenameColumn(
                name: "Approver",
                table: "ApprovalRequests",
                newName: "ApproverId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequests_ApproverId",
                table: "ApprovalRequests",
                column: "ApproverId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequests_LeaveRequestId",
                table: "ApprovalRequests",
                column: "LeaveRequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_Employees_ApproverId",
                table: "ApprovalRequests",
                column: "ApproverId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_LeaveRequests_LeaveRequestId",
                table: "ApprovalRequests",
                column: "LeaveRequestId",
                principalTable: "LeaveRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Projects_Id",
                table: "Employees",
                column: "Id",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_Employees_ApproverId",
                table: "ApprovalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_LeaveRequests_LeaveRequestId",
                table: "ApprovalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Projects_Id",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalRequests_ApproverId",
                table: "ApprovalRequests");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalRequests_LeaveRequestId",
                table: "ApprovalRequests");

            migrationBuilder.RenameColumn(
                name: "ProjectManagerId",
                table: "Projects",
                newName: "ProjectMenager");

            migrationBuilder.RenameColumn(
                name: "LeaveRequestId",
                table: "ApprovalRequests",
                newName: "LeaveRequest");

            migrationBuilder.RenameColumn(
                name: "ApproverId",
                table: "ApprovalRequests",
                newName: "Approver");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
