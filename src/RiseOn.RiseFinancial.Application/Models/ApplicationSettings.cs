namespace RiseOn.RiseFinancial.Application.Models;

public class ApplicationSettings
{
    public DatabaseSettings? Database { get; set; }
}

public class DatabaseSettings
{
    public string? ConnectionUrl { get; set; }
}