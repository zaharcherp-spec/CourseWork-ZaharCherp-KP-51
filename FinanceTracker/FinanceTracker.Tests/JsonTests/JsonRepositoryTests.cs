using FinanceTracker.Library.Data;

namespace FinanceTracker.Tests.JsonTests;

public class JsonRepositoryTests : IDisposable
{
    private readonly string _testDirectory;
    private readonly string _testFilePath;
    private readonly JsonRepository<string> _repository;

    public JsonRepositoryTests()
    {
        _testDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        _testFilePath = Path.Combine(_testDirectory, "test_data.json");
        _repository = new JsonRepository<string>(_testFilePath);
    }

    // Перевіряє, що при відсутності файлу база не падає, а повертає пустий список
    [Fact]
    public async Task LoadAsync_FileDoesNotExist_ReturnsEmptyList()
    {
        var result = await _repository.LoadAsync();

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    // Перевіряє, що при збереженні автоматично створюється і папка, і сам файл
    [Fact]
    public async Task SaveAsync_CreatesDirectoryAndFile()
    {
        var data = new List<string> { "TestItem" };

        await _repository.SaveAsync(data);

        Assert.True(Directory.Exists(_testDirectory));
        Assert.True(File.Exists(_testFilePath));
    }

    // Перевіряє основний цикл: зберегли дані у файл -> зчитали -> переконалися, що нічого не загубилося
    [Fact]
    public async Task SaveAsync_ThenLoadAsync_ReturnsEquivalentList()
    {
        var data = new List<string> { "Item1", "Item2", "Item3" };

        await _repository.SaveAsync(data);
        var loadedData = await _repository.LoadAsync();

        Assert.Equal(data, loadedData);
    }

    // Захист від «дурня»: перевіряє, що пошкоджений (зламаний) JSON файл не крашить програму, а повертає пустий список
    [Fact]
    public async Task LoadAsync_InvalidJson_ReturnsEmptyList()
    {
        Directory.CreateDirectory(_testDirectory);
        await File.WriteAllTextAsync(_testFilePath, "INVALID JSON {");

        var result = await _repository.LoadAsync();

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    public void Dispose()
    {
        if (Directory.Exists(_testDirectory))
        {
            Directory.Delete(_testDirectory, true);
        }
    }
}






