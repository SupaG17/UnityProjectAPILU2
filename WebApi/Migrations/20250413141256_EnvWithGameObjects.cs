using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectMap.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class EnvWithGameObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Environment2DId",
                table: "GameObjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GameObjects_Environment2DId",
                table: "GameObjects",
                column: "Environment2DId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameObjects_Environments_Environment2DId",
                table: "GameObjects",
                column: "Environment2DId",
                principalTable: "Environments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameObjects_Environments_Environment2DId",
                table: "GameObjects");

            migrationBuilder.DropIndex(
                name: "IX_GameObjects_Environment2DId",
                table: "GameObjects");

            migrationBuilder.DropColumn(
                name: "Environment2DId",
                table: "GameObjects");
        }
    }
}
