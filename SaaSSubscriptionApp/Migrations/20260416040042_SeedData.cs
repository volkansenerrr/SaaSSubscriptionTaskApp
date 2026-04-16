using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SaaSSubscriptionApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Ek Kullanıcı", 20m },
                    { 2, "Ek Depolama", 50m }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Standart", 100m },
                    { 2, "Premium", 200m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
