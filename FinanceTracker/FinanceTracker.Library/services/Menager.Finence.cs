namespace FinanceTracker.Library.Services;

public partial class Menager
{
    public async Task DepositAsync(decimal amount)
    {
        await Task.CompletedTask;
    }

    public async Task<bool> TransferAsync(string receiverUsername, decimal amount)
    {
        return true;
    }
}
