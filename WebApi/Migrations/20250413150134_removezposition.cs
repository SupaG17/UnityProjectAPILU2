using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectMap.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class removezposition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zposition",
                table: "GameObjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Zposition",
                table: "GameObjects",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
