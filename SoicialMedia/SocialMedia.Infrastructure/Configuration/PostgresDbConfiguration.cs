namespace SocialMedia.Infrastructure.Configuration;

public class PostgresDbConfiguration
{
    #region Properties

    public string DbHost { get; set; } = string.Empty;

    public string DbPort { get; set; } = string.Empty;

    public string DbName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    
    public string ConnectionString => $"Host={DbHost}; Database={DbName}; Port={DbPort}; Username={UserName}; Password={Password}";


    #endregion
}