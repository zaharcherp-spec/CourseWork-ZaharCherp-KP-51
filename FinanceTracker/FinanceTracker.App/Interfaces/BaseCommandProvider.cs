using FinanceTracker.Library.Services;

namespace FinanceTracker.App.interfaces;

public abstract class BaseCommandProvider
{
    protected readonly Action<BaseCommandProvider> _changer;
    protected readonly AuthManager _authManager;
    protected readonly Menager _menager;

    protected BaseCommandProvider(
        Action<BaseCommandProvider> changer,
        AuthManager authManager,
        Menager menager
    )
    {
        _changer = changer;
        _authManager = authManager;
        _menager = menager;
    }

    public abstract Dictionary<string, (string Name, Func<Task> Action)> GetCommands();
}
