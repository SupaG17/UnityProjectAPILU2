using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectMap.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class PrefabId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image_url",
                table: "GameObjects",
                newName: "PrefabId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrefabId",
                table: "GameObjects",
                newName: "Image_url");
        }
    }
}
