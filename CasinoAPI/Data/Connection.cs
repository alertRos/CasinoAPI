using System.Data.Common;

public class Connection
{
    private string connectionString = string.Empty;
    public Connection()
    {
        var builder = new ConfigurationBuilder().SetBasePath
            (Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

        connectionString = builder.GetSection
            ("ConnectionStrings:mainConnection").Value;
    }

    public string getConnection()
    {
        return connectionString;
    }
}

