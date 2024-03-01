using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    public partial class SeedGeneralCategories : Migration
    {
        private string[] categories = new[]
        {
            "Health & Wellness",
            "Education",
            "Entertainment",
            "Fitness",
            "Groceries",
            "Maintenance",
            "Charity",
            "Gifts",
            "Savings",
            "Investments",
            "Travel",
            "Clothing",
            "Transport",
            "Fuel",
            "Restaurant & Cafe",
            "Salary",
        };
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            foreach (var categoryName in categories)
            {
                migrationBuilder.InsertData(
                    table: "GeneralCategories",
                    columns: new[] { "Name", "CreatedDate", "UpdatedDate" },
                    values: new object[] { categoryName, DateTime.UtcNow, DateTime.UtcNow }
                );
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var categoryName in categories)
            {
                migrationBuilder.DeleteData(
                    table: "GeneralCategories",
                    keyColumn: "Name",
                    keyValue: categoryName
                );
            }
        }
    }
}