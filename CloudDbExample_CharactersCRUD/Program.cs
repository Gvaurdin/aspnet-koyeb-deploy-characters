using CloudDbExample_CharactersCRUD;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        dbContext.Database.Migrate();
        Console.WriteLine("Миграции успешно применены!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка при применении миграций: {ex.Message}");
    }
}

// ����������� �����������
app.MapGet("/api", () => new { Message = "server is running" });
app.MapGet("/api/ping", () => new { Message = "pong" });

// API CRUD-��������

// GET /api/game-character
app.MapGet("/api/game-character", async (ApplicationDbContext db) =>
{
    // 200
    return await db.GameCharacters.ToListAsync();
});

// GET /api/game-character/{id}
app.MapGet("/api/game-character/{id:int}", async (int id, ApplicationDbContext db) =>
{
    GameCharacter? character = await db.GameCharacters.FirstOrDefaultAsync(c => c.Id == id);
    if (character == null)
    {
        // 404
        return Results.NotFound(new { Message = $"'{id}' not found" });
    }
    // 200
    return Results.Ok(character);
});

// POST /api/game-character
app.MapPost("/api/game-character", async (GameCharacter character, ApplicationDbContext db) =>
{
    // ����������� ������� ������
    character.Id = 0;
    character.CreatedAt = DateTime.UtcNow.Date;
    // �������� ������� ������
    if (string.IsNullOrEmpty(character.Name))
    {
        // 400
        return Results.BadRequest(new { Message = "'name' must be not empty string" });
    }
    if (character.HP <= 0)
    {
        // 400
        return Results.BadRequest(new { Message = "'hp' must be greater then zero" });
    }
    if (character.Damage < 0)
    {
        // 400
        return Results.BadRequest(new { Message = "'damage' must be greater or equal then zero" });
    }
    // ������� ������ � ������ ���������
    await db.GameCharacters.AddAsync(character);
    await db.SaveChangesAsync();
    // 200
    return Results.Ok(character);
});

// PATCH /api/game-character/{id}
app.MapPatch("/api/game-character/{id:int}", async (int id, GameCharacter character, ApplicationDbContext db) =>
{
    // ����� ������ ������
    GameCharacter? updated = await db.GameCharacters.FirstOrDefaultAsync(c => c.Id == id);
    if (updated == null)
    {
        // 404
        return Results.NotFound(new { Message = $"'{id}' not found" });
    }
    // �������� ������� ������
    if (string.IsNullOrEmpty(character.Name))
    {
        // 400
        return Results.BadRequest(new { Message = "'name' must be not empty string" });
    }
    if (character.HP <= 0)
    {
        // 400
        return Results.BadRequest(new { Message = "'hp' must be greater then zero" });
    }
    if (character.Damage < 0)
    {
        // 400
        return Results.BadRequest(new { Message = "'damage' must be greater or equal then zero" });
    }
    // ���� � ������� ��� ��, �� �������� (Id, CreatedAt �� �������)
    updated.Name = character.Name;
    updated.HP = character.HP;
    updated.Damage = character.Damage;
    await db.SaveChangesAsync();
    // 200
    return Results.Ok(updated);
});

// DELETE /api/game-character/{id}
app.MapDelete("/api/game-character/{id:int}", async (int id, ApplicationDbContext db) =>
{
    GameCharacter? deleted = await db.GameCharacters.FirstOrDefaultAsync(c => c.Id == id);
    if (deleted == null)
    {
        // 404
        return Results.NotFound(new { Message = $"'{id}' not found" });
    }
    db.GameCharacters.Remove(deleted);
    await db.SaveChangesAsync();
    // 204
    return Results.NoContent();
});

app.Run();
