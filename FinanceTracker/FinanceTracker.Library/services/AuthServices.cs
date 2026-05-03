using FinanceTracker.Library.Data;
using FinanceTracker.Library.Models;

namespace FinanceTracker.Library.Services;

public class AuthManager
{
    private readonly JsonRepository<User> _repository;
    private List<User> _users = new();


    public User? CurrentUser { get; private set; }

    public AuthManager(JsonRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task InitAsync()
    {
        _users = await _repository.LoadAsync();
    }

    public async Task<bool> RegisterAsync(string username, string password)
    {
        if (_users.Any(u => u.UserName == username)) return false;

        var newUser = new User(username, password);
        _users.Add(newUser);
        await _repository.SaveAsync(_users);

        return true;
    }

    public bool Login(string username, string password)
    {
        var user = _users.FirstOrDefault(u => u.UserName == username && u.Password == password);
        if (user == null) return false;

        CurrentUser = user;

        return true;
    }

    public void Logout()
    {
        CurrentUser = null;
    }
}
