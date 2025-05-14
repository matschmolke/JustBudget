using JustBudget;
using System.IO;
using System.Text.Json;

public static class SettingsManager
{
    private static string path = "settings.json";

    public static AppSettings Load()
    {
        if (!File.Exists(path))
            return new AppSettings();

        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<AppSettings>(json);
    }

    public static void Save(AppSettings settings)
    {
        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
    }
}
