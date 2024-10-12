using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone_____Vescio_Pia_Francesca__BE_.Migrations
{
    /// <inheritdoc />
    public partial class RoleOfCharacter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Characters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_RoleId",
                table: "Characters",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_GuildRole_RoleId",
                table: "Characters",
                column: "RoleId",
                principalTable: "GuildRole",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_GuildRole_RoleId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_RoleId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Characters");
        }
    }
}
