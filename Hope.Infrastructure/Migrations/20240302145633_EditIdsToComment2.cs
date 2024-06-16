using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hope.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditIdsToComment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_postOfLostPeoples_Peopleid",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_postOfLostthings_Thingsid",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "Thingsid",
                table: "Comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Peopleid",
                table: "Comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_postOfLostPeoples_Peopleid",
                table: "Comment",
                column: "Peopleid",
                principalTable: "postOfLostPeoples",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_postOfLostthings_Thingsid",
                table: "Comment",
                column: "Thingsid",
                principalTable: "postOfLostthings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_postOfLostPeoples_Peopleid",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_postOfLostthings_Thingsid",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "Thingsid",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Peopleid",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_postOfLostPeoples_Peopleid",
                table: "Comment",
                column: "Peopleid",
                principalTable: "postOfLostPeoples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_postOfLostthings_Thingsid",
                table: "Comment",
                column: "Thingsid",
                principalTable: "postOfLostthings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
