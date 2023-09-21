using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendApi.Migrations
{
    /// <inheritdoc />
    public partial class fix_ondelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Softwares_SoftwareId",
                table: "Ratings");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Softwares_SoftwareId",
                table: "Ratings",
                column: "SoftwareId",
                principalTable: "Softwares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Softwares_SoftwareId",
                table: "Ratings");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Softwares_SoftwareId",
                table: "Ratings",
                column: "SoftwareId",
                principalTable: "Softwares",
                principalColumn: "Id");
        }
    }
}
