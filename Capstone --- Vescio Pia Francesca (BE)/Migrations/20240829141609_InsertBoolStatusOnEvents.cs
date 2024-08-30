using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone_____Vescio_Pia_Francesca__BE_.Migrations
{
    /// <inheritdoc />
    public partial class InsertBoolStatusOnEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsChanged",
                table: "Events",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChanged",
                table: "Events");
        }
    }
}
