using Microsoft.Win32;

namespace NotifyIconEx;

internal static class ThemeListener
{
    private static bool _dark = false;

    public static bool IsDarkMode => _dark;

    public delegate void ThemeChangedEventHandler(bool isDark);

    public static event ThemeChangedEventHandler ThemeChanged = null!;

    static ThemeListener()
    {
        SystemEvents.UserPreferenceChanged += UserPreferenceChanged;
        _dark = ReadDarkMode();
    }

    private static void UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        if (ReadDarkMode() != _dark)
        {
            _dark = !_dark;
            ThemeChanged?.Invoke(_dark);
        }
    }

    private const string REGISTRY_KEY_PATH = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

    private const string REGISTRY_VALUE_NAME = "AppsUseLightTheme";

    private static bool ReadDarkMode()
    {
        object? registryValueObject;
        using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY_PATH))
        {
            registryValueObject = key?.GetValue(REGISTRY_VALUE_NAME);
            if (registryValueObject != null)
            {
                int? registryValue = (int)registryValueObject;
                return registryValue <= 0;
            }
        }
        using (RegistryKey? key = Registry.LocalMachine.OpenSubKey(REGISTRY_KEY_PATH))
        {
            registryValueObject = key?.GetValue(REGISTRY_VALUE_NAME);
            if (registryValueObject != null)
            {
                int? registryValue = (int)registryValueObject;
                return registryValue <= 0;
            }
        }
        return false;
    }
}
