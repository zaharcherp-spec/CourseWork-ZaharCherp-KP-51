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

    public async Task<List<T>> LoadAsync()
    {
        if (!File.Exists(_filePath))
            return new List<T>();

        try
        {
            string json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new List<T>();
        }
        catch (Exception e)
        {
            return new List<T>();
        }
    }

    public async Task SaveAsync(List<T> items)
    {
        string json = JsonSerializer.Serialize(items, _options);
        await File.WriteAllTextAsync(_filePath, json);
    }
}