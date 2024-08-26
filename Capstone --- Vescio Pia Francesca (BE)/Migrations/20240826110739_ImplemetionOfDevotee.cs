using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone_____Vescio_Pia_Francesca__BE_.Migrations
{
    /// <inheritdoc />
    public partial class ImplemetionOfDevotee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EcoId",
                table: "Characters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_EcoId",
                table: "Characters",
                column: "EcoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Ecos_EcoId",
                table: "Characters",
                column: "EcoId",
                principalTable: "Ecos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Ecos_EcoId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_EcoId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "EcoId",
                table: "Characters");
        }
    }
}
