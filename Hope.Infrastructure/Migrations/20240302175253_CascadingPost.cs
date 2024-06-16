using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hope.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CascadingPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_postOfLostPeoples_Users_UserId",
                table: "postOfLostPeoples");

            migrationBuilder.AddForeignKey(
                name: "FK_postOfLostPeoples_Users_UserId",
                table: "postOfLostPeoples",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_postOfLostPeoples_Users_UserId",
                table: "postOfLostPeoples");

            migrationBuilder.AddForeignKey(
                name: "FK_postOfLostPeoples_Users_UserId",
                table: "postOfLostPeoples",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
