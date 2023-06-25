using Microsoft.Extensions.Configuration;

namespace GSM.Shared.EnvironmentVariable.Utils;

public class AppSettingUtils
{
    private AppSettingUtils(IConfiguration config) => this._configuration = config;

    private static AppSettingUtils? _appSettings = null!;
    private readonly IConfiguration _configuration;

    public static AppSettingUtils Current
    {
        get
        {
            if (_appSettings == null) _appSettings = GetCurrentSettings();

            return _appSettings;
        }
    }

    public T Get<T>(string key)
    {
        T? value = Current._configuration.GetValue<T>(key);

        if (value == null) throw new Exception($"No se encontro variable de entorno llamada {key}");

        return value;
    }

    private static AppSettingUtils GetCurrentSettings()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        IConfigurationRoot configuration = builder.Build();

        var settings = new AppSettingUtils(configuration);

        return settings;
    }
}