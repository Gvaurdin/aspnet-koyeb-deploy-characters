using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudDbExample_CharactersCRUD.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GameCharacters",
                columns: ["Id", "Name", "HP", "Damage", "CreatedAt"],
                values: new object[,]
                {
                    { 1, "Иван-Брутал", 500, 120, DateTime.UtcNow.Date },
                    { 2, "Мария-Целитель", 300, 50, DateTime.UtcNow.Date },
                    { 3, "Дмитрий-Уклонист", 400, 80, DateTime.UtcNow.Date },
                    { 4, "Елена-Маг", 250, 150, DateTime.UtcNow.Date },
                    { 5, "Сергей-Танк", 700, 100, DateTime.UtcNow.Date },
                    { 6, "Ольга-Лучник", 350, 90, DateTime.UtcNow.Date }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameCharacters",
                keyColumn: "Id",
                keyValues: [1, 2, 3, 4, 5, 6]
            );
        }
    }
}
