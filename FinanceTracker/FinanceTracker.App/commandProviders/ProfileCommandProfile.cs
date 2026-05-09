using System.Security.Cryptography.X509Certificates;
using FinanceTracker.App.interfaces;
using FinanceTracker.Library.Services;

public class ProfileCommandProvider : BaseCommandProvider
{
    public ProfileCommandProvider(Action<BaseCommandProvider> action, AuthManager authManager, Menager menager) : base(action, authManager, menager)
    {

    }

    public override Dictionary<string, (string Name, Func<Task> Action)> GetCommands()
    {
        var dict = new Dictionary<string, (string Name, Func<Task> Action)>();

        return dict;
    }
}