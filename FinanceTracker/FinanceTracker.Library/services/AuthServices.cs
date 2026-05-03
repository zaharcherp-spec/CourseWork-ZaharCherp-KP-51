using FinanceTracker.Library.Data;
using FinanceTracker.Library.models; 

namespace FinanceTracker.Library.Services;

public class AuthManager
{
    private readonly JsonRepository<User> _repository;
    private List<User> _users;
    public User? CurrentUser { get; private set; }

    public AuthManager()
    {
        _repository = new JsonRepository<User>("users_data");
        _users = _repository.Load();
    }

    public bool Login(string username, string password)
    {
        var user = _users.FirstOrDefault(u => u.UserName == username && u.Password == password);
        if (user == null) return false;

        CurrentUser = user;
        return true;
    }

    public bool Register(string username, string password)
    {
        if (_users.Any(u => u.UserName == username))
            return false;

        var newUser = new User(username, password);
        _users.Add(newUser);

        _repository.Save(_users);
        return true;
    }

    public void Logout()
    {
        CurrentUser = null;
    }

    public void UpdateUsername(string newName)
    {
        if (CurrentUser == null || string.IsNullOrWhiteSpace(newName)) return;

        CurrentUser.UpdateName(newName);
        _repository.Save(_users);
    }
}