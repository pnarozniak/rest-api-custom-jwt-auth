using Microsoft.EntityFrameworkCore.Migrations;

namespace rest_api_custom_jwt_auth.Migrations
{
    public partial class Added_UserRole_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User_Role",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserRole_pk", x => new { x.IdRole, x.IdUser });
                    table.ForeignKey(
                        name: "Role_UserRole",
                        column: x => x.IdRole,
                        principalTable: "Role",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "User_UserRole",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_IdUser",
                table: "User_Role",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Role");
        }
    }
}
