using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    public partial class SeedCategories : Migration
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
                    table: "Categories",
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
                    table: "Categories",
                    keyColumn: "Name",
                    keyValue: categoryName
                );
            }
        }
    }
}
