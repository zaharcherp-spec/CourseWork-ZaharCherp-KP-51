using FinanceTracker.Library.Services;

namespace FinanceTracker.App.CommandProviders;

public abstract class BaseCommandProvider
{
    protected readonly Action<BaseCommandProvider> _changer;
    protected readonly AuthManager _authManager;
    protected readonly Manager _manager;

    protected BaseCommandProvider(
        Action<BaseCommandProvider> changer,
        AuthManager authManager,
        Manager menager
    )
    {
        _changer = changer;
        _authManager = authManager;
        _manager = menager;
    }

    public abstract Dictionary<string, (string Name, Func<Task> Action)> GetCommands();
}

