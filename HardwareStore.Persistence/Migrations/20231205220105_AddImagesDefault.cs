using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HardwareStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddImagesDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 12, 5, 16, 1, 5, 520, DateTimeKind.Local).AddTicks(7390), "https://localhost:7000/imagenes//electricidad.jpg" });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 12, 5, 16, 1, 5, 520, DateTimeKind.Local).AddTicks(7390), "https://localhost:7000/imagenes//fontaneria.jpg" });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 12, 5, 16, 1, 5, 520, DateTimeKind.Local).AddTicks(7390), "https://localhost:7000/imagenes//herramientas.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 12, 5, 14, 22, 40, 600, DateTimeKind.Local).AddTicks(4193), null });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 12, 5, 14, 22, 40, 600, DateTimeKind.Local).AddTicks(4193), null });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 12, 5, 14, 22, 40, 600, DateTimeKind.Local).AddTicks(4193), null });
        }
    }
}
