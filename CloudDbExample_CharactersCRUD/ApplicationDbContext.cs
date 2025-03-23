using Microsoft.EntityFrameworkCore;

namespace CloudDbExample_CharactersCRUD
{
    // ApplicationDbContext - менеджер для работы с БД
    public class ApplicationDbContext : DbContext
    {
        // GameCharacters - коллекция-провайдер к таблице БД
        public required DbSet<GameCharacter> GameCharacters { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // OnConfiguring - переопределение метода для настройки подключения к БД
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();

                // читаем переменную окружения DB_HOST
                string dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
                string useConnection = config["UseConnection"] ?? "LocalConnection";
                if (dbHost == "localhost")
                {
                    useConnection = "LocalConnection";
                }
                else if (dbHost == "docker")
                {
                    useConnection = "DockerizedConnection";
                }
                string? connectionString = config.GetConnectionString(useConnection);
                optionsBuilder.UseNpgsql(connectionString);
            }

        }
    }
}
