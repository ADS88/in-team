namespace Server.Api.Configuration
{
    /// <summary>
    /// Class to hold the database connection string
    /// </summary>
    public class PostgresSettings
    {
        public string Host {get; set;}

        public string Port {get; set;}

        public string Database {get; set;}

        public string Username {get; set;}

        public string Password {get; set;}

        public string ConnectionString{
            get {
                return $"Host={Host};port={Port};Database={Database};Username={Username};Password={Password}";
            }
        }
    }
}