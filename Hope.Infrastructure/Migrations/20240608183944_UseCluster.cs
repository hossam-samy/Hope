using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hope.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UseCluster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cluster",
                table: "postOfLostthings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cluster",
                table: "postOfLostPeoples",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cluster",
                table: "postOfLostthings");

            migrationBuilder.DropColumn(
                name: "Cluster",
                table: "postOfLostPeoples");
        }
    }
}
