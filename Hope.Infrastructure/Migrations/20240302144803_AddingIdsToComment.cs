using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hope.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingIdsToComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_postOfLostPeoples_PeopleId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_postOfLostthings_ThingsId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "ThingsId",
                table: "Comment",
                newName: "Thingsid");

            migrationBuilder.RenameColumn(
                name: "PeopleId",
                table: "Comment",
                newName: "Peopleid");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ThingsId",
                table: "Comment",
                newName: "IX_Comment_Thingsid");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_PeopleId",
                table: "Comment",
                newName: "IX_Comment_Peopleid");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_postOfLostPeoples_Peopleid",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_postOfLostthings_Thingsid",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "Thingsid",
                table: "Comment",
                newName: "ThingsId");

            migrationBuilder.RenameColumn(
                name: "Peopleid",
                table: "Comment",
                newName: "PeopleId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_Thingsid",
                table: "Comment",
                newName: "IX_Comment_ThingsId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_Peopleid",
                table: "Comment",
                newName: "IX_Comment_PeopleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_postOfLostPeoples_PeopleId",
                table: "Comment",
                column: "PeopleId",
                principalTable: "postOfLostPeoples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_postOfLostthings_ThingsId",
                table: "Comment",
                column: "ThingsId",
                principalTable: "postOfLostthings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
