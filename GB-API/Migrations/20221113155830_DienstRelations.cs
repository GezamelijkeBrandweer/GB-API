using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBAPI.Migrations
{
    /// <inheritdoc />
    public partial class DienstRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Score",
                schema: "MIC-DB",
                table: "intensiteit",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                schema: "MIC-DB",
                table: "intensiteit");
        }
    }
}
