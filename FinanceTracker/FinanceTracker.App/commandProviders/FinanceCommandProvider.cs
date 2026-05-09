using FinanceTracker.App.interfaces;
using FinanceTracker.Library.Services;

namespace FinanceTracker.App.CommandProviders;

public class FinanceCommandProvider : BaseCommandProvider
{
    public FinanceCommandProvider(Action<BaseCommandProvider> changer, AuthManager authManager, Menager menager) : base(changer, authManager, menager) { }


    public override Dictionary<string, (string Name, Func<Task> Action)> GetCommands()
    {
        var dict = new Dictionary<string, (string Name, Func<Task> Action)>();

        return dict;
    }
}