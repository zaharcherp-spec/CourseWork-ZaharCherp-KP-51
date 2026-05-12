

namespace FinanceTracker.Library.Services;

public partial class Menager
{
    public async Task<bool> ChangePasswordAsync(string oldPassword, string newPassword)
    {
        if (_authManager.CurrentUser == null || oldPassword != _authManager.CurrentUser.Password)
        {
            return false;
        }

        if (newPassword.Length < _authManager.Constraints.MinPasswordLength)
        {
            return false;
        }

        _authManager.CurrentUser.ChangePassword(newPassword);
        await _authManager.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> ChangeUsernameAsync(string newUsername)
    {
        if (_authManager.CurrentUser == null)
        {
            return false;
        }

        if (newUsername.Length < _authManager.Constraints.MinUsernameLength)
        {
            return false;
        }

        if (_authManager.GetUserByUsername(newUsername) != null)
        {
            return false;
        }

        _authManager.CurrentUser.UpdateName(newUsername);
        await _authManager.SaveChangesAsync();
        return true;
    }

    public  Task<string> ShowUserData()
    {
     string data =_authManager.CurrentUser.ToString();

     return Task.FromResult(data);
    }
}
       

