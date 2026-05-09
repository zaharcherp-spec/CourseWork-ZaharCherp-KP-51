using FinanceTracker.Library.Settings;

namespace FinanceTracker.Library.Services;

public partial class Menager
{
    private readonly AuthManager _authManager;
    private readonly Constrains.TransferConstraints _constraints;

    public Menager(AuthManager authManager, Constrains.TransferConstraints constraints)
    {
        _authManager = authManager;
        _constraints = constraints;
    }
}
