using FinanceTracker.Library.Data;
using FinanceTracker.Library.Models;
using FinanceTracker.Library.Settings;

namespace FinanceTracker.Library.Services;

public class AuthManager
{
    private readonly JsonRepository<User> _repository;
    private readonly Constrains.AuthConstraints _constraints;
    private List<User> _users = new();
    public Constrains.AuthConstraints Constraints => _constraints;

    public User? CurrentUser { get; private set; }

    public AuthManager(JsonRepository<User> repository, Constrains.AuthConstraints constraints)
    {
        _repository = repository;
        _constraints = constraints;
    }

    public async Task InitAsync()
    {
        _users = await _repository.LoadAsync();
    }

    public async Task<bool> RegisterAsync(string username, string password)
    {
        if (
            username.Length < _constraints.MinUsernameLength
            || password.Length < _constraints.MinPasswordLength
        )
        {
            return false;
        }

        bool userExists = false;
        foreach (var user in _users)
        {
            if (user.UserName == username)
            {
                userExists = true;
                break;
            }
        }

        if (userExists)
        {
            throw new ArgumentException("Такий юзер уже існує");
        }

        var newUser = new User(username, password);
        _users.Add(newUser);
        await _repository.SaveAsync(_users);

        return true;
    }

    public bool Login(string username, string password)
    {
        User? user = null;
        foreach (var u in _users)
        {
            if (u.UserName == username && u.Password == password)
            {
                user = u;
                break;
            }
        }

        if (user == null)
        {
            return false;
        }

        CurrentUser = user;
        return true;
    }

    public void Logout()
    {
        CurrentUser = null;
    }

    public async Task SaveChangesAsync()
    {
        await _repository.SaveAsync(_users);
    }

    public User? GetUserByUsername(string username)
    {
        foreach (var u in _users)
        {
            if (u.UserName == username)
            {
                return u;
            }
        }
        return null;
    }
}
