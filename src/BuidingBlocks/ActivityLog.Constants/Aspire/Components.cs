namespace ActivityLog.Constants.Aspire;

public static class Components
{
    public static readonly string ConnectionStrings = nameof(ConnectionStrings);
    public static class Postgres
    {
        private const string Suffix = "Db";
        
        public const string Name = "Postgres";
        
        public static readonly string User = $"{Name}:{nameof(User)}";
        
        public static readonly string Password = $"{Name}:{nameof(Password)}";

        public static readonly string Workout = $"{nameof(Workout)}{Suffix}";
        
        public static readonly string Identity = $"{nameof(Identity)}{Suffix}";

        public static readonly string IdentitySchema = nameof(Identity).ToLowerInvariant();
        
        public const int Port = 5432;
    }

    public static class RabbitMq
    {
        public const string Name = "RabbitMQ";

        public static readonly string User = $"{Name}:{nameof(User)}";
        
        public static readonly string Password = $"{Name}:{nameof(Password)}";
    }
}
