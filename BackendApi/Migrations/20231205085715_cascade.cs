using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendApi.Migrations
{
    /// <inheritdoc />
    public partial class cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Softwares_Shops_ShopId",
                table: "Softwares");

            migrationBuilder.AddForeignKey(
                name: "FK_Softwares_Shops_ShopId",
                table: "Softwares",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Softwares_Shops_ShopId",
                table: "Softwares");

            migrationBuilder.AddForeignKey(
                name: "FK_Softwares_Shops_ShopId",
                table: "Softwares",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id");
        }
    }
}
