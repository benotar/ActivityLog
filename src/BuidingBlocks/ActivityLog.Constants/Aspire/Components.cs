namespace ActivityLog.Constants.Aspire;

public static class Components
{
    public static readonly string Postgres = nameof(Postgres).ToLowerInvariant();
    
    public static class Database
    {
        private const string Suffix = "Db";

        public static readonly string Workout = $"{nameof(Workout).ToLowerInvariant()}{Suffix}";
        
        public const int Port = 5432;
    }
}
