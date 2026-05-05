using FinanceTracker.Library.Models;
using FinanceTracker.Library.Services;
using FinanceTracker.Library.Settings;

public class Menager
{
    private readonly AuthManager AuthManager;
    private readonly Constrains.TransferConstraints TransferConstrains;
    public Menager(AuthManager authManager, Constrains.TransferConstraints transferConstraints)
    {
        AuthManager = authManager;
        TransferConstrains = transferConstraints;

    }
        
        public async Task<bool> Deposit()
    {
        return true;
    }
}





