using FinanceTracker.Library.Settings;

namespace FinanceTracker.Library.Services;

public partial class Manager
{
    private readonly AuthManager _authManager;
    private readonly Constrains.TransferConstraints _constraints;

    public Manager(AuthManager authManager, Constrains.TransferConstraints constraints)
    {
        _authManager = authManager;
        _constraints = constraints;
    }
}
