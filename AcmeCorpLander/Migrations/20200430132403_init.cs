using Microsoft.EntityFrameworkCore.Migrations;

namespace AcmeCorpLander.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Submission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    SerialNum = table.Column<int>(nullable: false),
                    Wins = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submission", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Submission");
        }
    }
}
