using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArchLab6.Migrations
{
    public partial class AlterNewsTopicAddImageSrc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageSource",
                table: "Topics",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageSource",
                table: "Topics");
        }
    }
}
