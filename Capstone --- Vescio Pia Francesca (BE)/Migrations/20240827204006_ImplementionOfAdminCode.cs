using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone_____Vescio_Pia_Francesca__BE_.Migrations
{
    /// <inheritdoc />
    public partial class ImplementionOfAdminCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminCode",
                table: "Users",
                type: "nvarchar(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminCode",
                table: "Users");
        }
    }
}
