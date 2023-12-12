using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gara.Management.Application.Migrations
{
    /// <inheritdoc />
    public partial class addthumbnailrepairserivce : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thubmnail",
                table: "RepairServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thubmnail",
                table: "RepairServices");
        }
    }
}
