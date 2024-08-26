using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone_____Vescio_Pia_Francesca__BE_.Migrations
{
    /// <inheritdoc />
    public partial class ModifyOfEcos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ecos_Nations_NationId",
                table: "Ecos");

            migrationBuilder.AlterColumn<int>(
                name: "NationId",
                table: "Ecos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ecos_Nations_NationId",
                table: "Ecos",
                column: "NationId",
                principalTable: "Nations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ecos_Nations_NationId",
                table: "Ecos");

            migrationBuilder.AlterColumn<int>(
                name: "NationId",
                table: "Ecos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ecos_Nations_NationId",
                table: "Ecos",
                column: "NationId",
                principalTable: "Nations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
