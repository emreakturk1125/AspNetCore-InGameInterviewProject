using Microsoft.EntityFrameworkCore.Migrations;

namespace InGameProject.DataAccess.Migrations
{
    public partial class Edit_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InGameDb_InGameDb_MainCategoryId",
                table: "InGameDb");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_InGameDb_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserOperationClaims_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOperationClaims",
                table: "UserOperationClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InGameDb",
                table: "InGameDb");

            migrationBuilder.RenameTable(
                name: "UserOperationClaims",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "InGameDb",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_InGameDb_MainCategoryId",
                table: "Categories",
                newName: "IX_Categories_MainCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_MainCategoryId",
                table: "Categories",
                column: "MainCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_MainCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "UserOperationClaims");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "InGameDb");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_MainCategoryId",
                table: "InGameDb",
                newName: "IX_InGameDb_MainCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOperationClaims",
                table: "UserOperationClaims",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InGameDb",
                table: "InGameDb",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_InGameDb_InGameDb_MainCategoryId",
                table: "InGameDb",
                column: "MainCategoryId",
                principalTable: "InGameDb",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_InGameDb_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "InGameDb",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserOperationClaims_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "UserOperationClaims",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
