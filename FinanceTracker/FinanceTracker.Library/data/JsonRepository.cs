using System.Text.Json;

namespace FinanceTracker.Library.Data;

public class JsonRepository<T>
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _options;

    public JsonRepository(string fileName)
    {
        _filePath = fileName;
        _options = new JsonSerializerOptions { WriteIndented = true };
    }

    public List<T> Load()
    {
        if (!File.Exists(_filePath))
            return new List<T>();

        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new List<T>();
    }

    public void Save(List<T> items)
    {
        string json = JsonSerializer.Serialize(items, _options);
        File.WriteAllText(_filePath, json);
    }
}