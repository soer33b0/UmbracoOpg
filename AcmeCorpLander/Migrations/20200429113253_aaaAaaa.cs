using Microsoft.EntityFrameworkCore.Migrations;

namespace AcmeCorpLander.Migrations
{
    public partial class aaaAaaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Submission",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    SerialNum = table.Column<int>(nullable: false),
                    Entries = table.Column<int>(nullable: false),
                    Wins = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submission", x => x.Email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Submission");
        }
    }
}
