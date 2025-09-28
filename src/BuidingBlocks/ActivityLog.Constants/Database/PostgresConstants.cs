namespace ActivityLog.Constants.Database;

public static class PostgresConstants
{
    public const int Port = 5432;
    public const string DbName = "ActivityLogDb";
    public const string DbNameParam = "db-name";
    public const string DbNameEnv = "POSTGRES_DB";
    public const string DbUserParam = "db-user";
    public const string DbPasswordParam = "db-password";
    public const string ContainerName = "activity_db";
    public const string Marker = "postgres";
    public const string Volume = "activity_data";
    public const string DbUserDefault = "admin";
    public const string DbPasswordDefault = "admin";
}
