using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone_____Vescio_Pia_Francesca__BE_.Migrations
{
    /// <inheritdoc />
    public partial class modifyRole2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GuildId",
                table: "GuildRole",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GuildRoleId",
                table: "Characters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GuildRole_GuildId",
                table: "GuildRole",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_GuildRoleId",
                table: "Characters",
                column: "GuildRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_GuildRole_GuildRoleId",
                table: "Characters",
                column: "GuildRoleId",
                principalTable: "GuildRole",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GuildRole_Guilds_GuildId",
                table: "GuildRole",
                column: "GuildId",
                principalTable: "Guilds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_GuildRole_GuildRoleId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_GuildRole_Guilds_GuildId",
                table: "GuildRole");

            migrationBuilder.DropIndex(
                name: "IX_GuildRole_GuildId",
                table: "GuildRole");

            migrationBuilder.DropIndex(
                name: "IX_Characters_GuildRoleId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "GuildRole");

            migrationBuilder.DropColumn(
                name: "GuildRoleId",
                table: "Characters");
        }
    }
}
