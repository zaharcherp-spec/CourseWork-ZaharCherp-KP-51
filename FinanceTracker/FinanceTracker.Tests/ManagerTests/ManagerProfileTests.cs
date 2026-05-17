using FinanceTracker.Library.Services;
using FinanceTracker.Library.Models;
using FinanceTracker.Library.Data;
using FinanceTracker.Library.Settings;

namespace FinanceTracker.Tests.ManagerTests;

public class ManagerProfileTests : IAsyncDisposable
{
    private readonly List<string> _tempFiles = new();

    private async Task<(Manager manager, AuthManager auth)> SetupManagerWithLoggedInUserAsync(string username, string password)
    {
        var tempFile = Path.GetTempFileName();
        _tempFiles.Add(tempFile);
        
        var repository = new JsonRepository<User>(tempFile);
        var authConstraints = new Constrains.AuthConstraints
        {
            MinPasswordLength = 4,
            MinUsernameLength = 3
        };
        var auth = new AuthManager(repository, authConstraints);
        
        await auth.InitAsync();
        await auth.RegisterAsync(username, password);
        auth.Login(username, password);

        var transferConstraints = new Constrains.TransferConstraints();
        var manager = new Manager(auth, transferConstraints);

        return (manager, auth);
    }

    private async Task<(Manager manager, AuthManager auth)> SetupManagerWithoutLoggedInUserAsync()
    {
        var tempFile = Path.GetTempFileName();
        _tempFiles.Add(tempFile);
        
        var repository = new JsonRepository<User>(tempFile);
        var authConstraints = new Constrains.AuthConstraints();
        var auth = new AuthManager(repository, authConstraints);
        await auth.InitAsync();

        var transferConstraints = new Constrains.TransferConstraints();
        var manager = new Manager(auth, transferConstraints);

        return (manager, auth);
    }

    public ValueTask DisposeAsync()
    {
        foreach (var file in _tempFiles)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }
        return ValueTask.CompletedTask;
    }

    // --- Зміна пароля (ChangePasswordAsync) ---

    [Fact]
    public async Task ChangePassword_ValidData_ReturnsTrueAndChangesPassword()
    {
        var (manager, auth) = await SetupManagerWithLoggedInUserAsync("user1", "oldPass");

        var result = await manager.ChangePasswordAsync("oldPass", "newPass");

        Assert.True(result);
        Assert.Equal("newPass", auth.CurrentUser!.Password);
    }

    [Fact]
    public async Task ChangePassword_WrongOldPassword_ReturnsFalse()
    {
        var (manager, auth) = await SetupManagerWithLoggedInUserAsync("user1", "oldPass");

        var result = await manager.ChangePasswordAsync("wrongPass", "newPass");

        Assert.False(result);
        Assert.Equal("oldPass", auth.CurrentUser!.Password);
    }

    [Fact]
    public async Task ChangePassword_NewPasswordTooShort_ReturnsFalse()
    {
        var (manager, auth) = await SetupManagerWithLoggedInUserAsync("user1", "oldPass");

        var result = await manager.ChangePasswordAsync("oldPass", "12"); // Min length is 4

        Assert.False(result);
        Assert.Equal("oldPass", auth.CurrentUser!.Password);
    }

    [Fact]
    public async Task ChangePassword_NotLoggedIn_ReturnsFalse()
    {
        var (manager, _) = await SetupManagerWithoutLoggedInUserAsync();

        var result = await manager.ChangePasswordAsync("oldPass", "newPass");

        Assert.False(result);
    }

    // --- Зміна логіна (ChangeUsernameAsync) ---

    [Fact]
    public async Task ChangeUsername_ValidData_ReturnsTrueAndChangesUsername()
    {
        var (manager, auth) = await SetupManagerWithLoggedInUserAsync("oldUser", "password");

        var result = await manager.ChangeUsernameAsync("newUser");

        Assert.True(result);
        Assert.Equal("newUser", auth.CurrentUser!.UserName);
        Assert.NotNull(auth.GetUserByUsername("newUser"));
        Assert.Null(auth.GetUserByUsername("oldUser"));
    }

    [Fact]
    public async Task ChangeUsername_UsernameTooShort_ReturnsFalse()
    {
        var (manager, auth) = await SetupManagerWithLoggedInUserAsync("user1", "password");

        var result = await manager.ChangeUsernameAsync("us"); // Min length is 3

        Assert.False(result);
        Assert.Equal("user1", auth.CurrentUser!.UserName);
    }

    [Fact]
    public async Task ChangeUsername_UsernameAlreadyTaken_ReturnsFalse()
    {
        var (manager, auth) = await SetupManagerWithLoggedInUserAsync("user1", "password");
        await auth.RegisterAsync("existingUser", "password123");

        var result = await manager.ChangeUsernameAsync("existingUser");

        Assert.False(result);
        Assert.Equal("user1", auth.CurrentUser!.UserName);
    }

    [Fact]
    public async Task ChangeUsername_NotLoggedIn_ReturnsFalse()
    {
        var (manager, _) = await SetupManagerWithoutLoggedInUserAsync();

        var result = await manager.ChangeUsernameAsync("newUser");

        Assert.False(result);
    }

    // --- Вивід даних користувача (ShowUserData) ---

    [Fact]
    public async Task ShowUserData_ReturnsFormattedString()
    {
        var (manager, _) = await SetupManagerWithLoggedInUserAsync("user1", "password");

        var data = await manager.ShowUserData();

        Assert.False(string.IsNullOrEmpty(data));
        Assert.Contains("user1", data);
    }
}
