using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todoapi.Migrations
{
    /// <inheritdoc />
    public partial class addpasswordsalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "passwordsalt",
                table: "User",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordsalt",
                table: "User");
        }
    }
}
