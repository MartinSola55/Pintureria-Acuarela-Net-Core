using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pintureria_Acuarela.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Sales",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 2L,
                column: "Name",
                value: "Sherwin Williams");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 3L,
                column: "Name",
                value: "Venier");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 4L,
                column: "Name",
                value: "Megaflex");

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "ID", "IsActive", "Name" },
                values: new object[,]
                {
                    { 5L, true, "Quimex" },
                    { 6L, true, "Colorin" },
                    { 7L, true, "Plavicon" },
                    { 8L, true, "Sinteplast" },
                    { 9L, true, "Colvinil" },
                    { 10L, true, "Petrilac" },
                    { 11L, true, "Magiplast" },
                    { 12L, true, "Varios" },
                    { 13L, true, "El Galgo" },
                    { 14L, true, "Poxipol" },
                    { 15L, true, "Siloc" },
                    { 16L, true, "Pinciroli" },
                    { 17L, true, "Espatulas" },
                    { 18L, true, "Abrasivos" },
                    { 19L, true, "JMG" },
                    { 20L, true, "Trimas" },
                    { 21L, true, "Ferrobet" },
                    { 22L, true, "Cautisol" },
                    { 23L, true, "Mangueras" },
                    { 24L, true, "Zeocar" },
                    { 25L, true, "Dowen Pagio" },
                    { 26L, true, "Bosch" },
                    { 27L, true, "Daihatsu" },
                    { 28L, true, "Makita" }
                });

            migrationBuilder.UpdateData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 2L,
                column: "Volume",
                value: 0.75f);

            migrationBuilder.UpdateData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 3L,
                column: "Volume",
                value: 0.5f);

            migrationBuilder.UpdateData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 4L,
                column: "Volume",
                value: 0.25f);

            migrationBuilder.InsertData(
                table: "Capacities",
                columns: new[] { "ID", "Description", "Volume" },
                values: new object[,]
                {
                    { 6L, "4 Litros", 4f },
                    { 7L, "10 Litros", 10f },
                    { 8L, "16 Litros", 16f },
                    { 9L, "32 Litros", 32f },
                    { 10L, "440 cc", 0.44f },
                    { 11L, "250 cc", 0.25f },
                    { 12L, "120 cc", 0.12f },
                    { 13L, "30 cc", 0.03f },
                    { 14L, "2 Litros", 2f },
                    { 15L, "2.5 Litros", 2.5f },
                    { 16L, "24 Litros", 24f },
                    { 17L, "25 Litros", 25f },
                    { 18L, "5 Litros", 5f },
                    { 19L, "6 Litros", 6f },
                    { 20L, "30 Litros", 30f },
                    { 21L, "36 Litros", 36f },
                    { 22L, "40 Litros", 40f },
                    { 23L, "60 cc", 0.06f },
                    { 24L, "300 gramos", 0.3f },
                    { 25L, "600 gramos", 0.6f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_UserID",
                table: "Sales",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_AspNetUsers_UserID",
                table: "Sales",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_AspNetUsers_UserID",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_UserID",
                table: "Sales");

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 25L);

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 2L,
                column: "Name",
                value: "Alba");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 3L,
                column: "Name",
                value: "Black Decker");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 4L,
                column: "Name",
                value: "Colorin");

            migrationBuilder.UpdateData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 2L,
                column: "Volume",
                value: 0f);

            migrationBuilder.UpdateData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 3L,
                column: "Volume",
                value: 0f);

            migrationBuilder.UpdateData(
                table: "Capacities",
                keyColumn: "ID",
                keyValue: 4L,
                column: "Volume",
                value: 0f);
        }
    }
}
