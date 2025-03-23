using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CloudDbExample_CharactersCRUD
{
    // GameCharacter - игровой персонаж в системе
    public class GameCharacter
    {
        public int Id { get; set; }                         // id, генерируется автоматически на уровне БД
        public string Name { get; set; } = string.Empty;    // имя - не пустая строка
        public int HP { get; set; }                         // величина здоровья - положительное целое
        public int Damage { get; set; }                     // величина урона - неотрицательное целое
        public DateTime CreatedAt { get; set; }             // дата создания

        public GameCharacter() { }
    }
}
