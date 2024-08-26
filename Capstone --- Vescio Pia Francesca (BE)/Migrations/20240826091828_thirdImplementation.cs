using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone_____Vescio_Pia_Francesca__BE_.Migrations
{
    /// <inheritdoc />
    public partial class thirdImplementation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eco_Nations_NationId",
                table: "Eco");

            migrationBuilder.DropForeignKey(
                name: "FK_Guild_Nations_NationId",
                table: "Guild");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guild",
                table: "Guild");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Eco",
                table: "Eco");

            migrationBuilder.RenameTable(
                name: "Guild",
                newName: "Guilds");

            migrationBuilder.RenameTable(
                name: "Eco",
                newName: "Ecos");

            migrationBuilder.RenameIndex(
                name: "IX_Guild_NationId",
                table: "Guilds",
                newName: "IX_Guilds_NationId");

            migrationBuilder.RenameIndex(
                name: "IX_Eco_NationId",
                table: "Ecos",
                newName: "IX_Ecos_NationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guilds",
                table: "Guilds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ecos",
                table: "Ecos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleUsers",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUsers", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUsers_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUsers_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUsers_UsersId",
                table: "RoleUsers",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ecos_Nations_NationId",
                table: "Ecos",
                column: "NationId",
                principalTable: "Nations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guilds_Nations_NationId",
                table: "Guilds",
                column: "NationId",
                principalTable: "Nations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ecos_Nations_NationId",
                table: "Ecos");

            migrationBuilder.DropForeignKey(
                name: "FK_Guilds_Nations_NationId",
                table: "Guilds");

            migrationBuilder.DropTable(
                name: "RoleUsers");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guilds",
                table: "Guilds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ecos",
                table: "Ecos");

            migrationBuilder.RenameTable(
                name: "Guilds",
                newName: "Guild");

            migrationBuilder.RenameTable(
                name: "Ecos",
                newName: "Eco");

            migrationBuilder.RenameIndex(
                name: "IX_Guilds_NationId",
                table: "Guild",
                newName: "IX_Guild_NationId");

            migrationBuilder.RenameIndex(
                name: "IX_Ecos_NationId",
                table: "Eco",
                newName: "IX_Eco_NationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guild",
                table: "Guild",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Eco",
                table: "Eco",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Eco_Nations_NationId",
                table: "Eco",
                column: "NationId",
                principalTable: "Nations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guild_Nations_NationId",
                table: "Guild",
                column: "NationId",
                principalTable: "Nations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
