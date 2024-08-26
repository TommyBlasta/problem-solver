namespace ProblemSolver.Infrastructure.Persistence.Options
{
    public class PostgresDatabaseOptions
    {
        public static string ConfigurationKey => "PostgresDatabase";
        public string ConnectionString { get; set; } = string.Empty;
        public bool EnableSensitiveLogging { get; set; }
    }
}
