using GSM.Shared.EnvironmentVariable.Utils;

namespace GSM.Shared.EnvironmentVariable;

public static class EnvironmentUtils
{
    public static T Get<T>(string key)
    {
#if DEBUG
        return AppSettingUtils.Current.Get<T>(key);
# else
        return EnviromentVariables.Get<T>(key);
#endif
    }
}