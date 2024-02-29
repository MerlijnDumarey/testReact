using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityDataApi.Migrations
{
    /// <inheritdoc />
    public partial class RenameTokenToOID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "token",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Oid",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Oid",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "token",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
