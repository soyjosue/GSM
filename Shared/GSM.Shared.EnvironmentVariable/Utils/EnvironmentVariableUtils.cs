using System.Text.Json;

namespace GSM.Shared.EnvironmentVariable.Utils;

public static class EnvironmentVariableUtils
{
    public static T? Get<T>(string key)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentException("Debe suministrar el nombre de la variable de entorno.");
        
        string? value = Environment.GetEnvironmentVariable(key);

        if (value == null) throw new Exception($"No se encontro variable de entorno llamada {key}");

        return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(value));
    }
}