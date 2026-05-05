using FinanceTracker.Library.Enums;

namespace FinanceTracker.Library.Models;

public class FinanceOperation
{
    public Guid Guid { get; private set; }
    public FinanceOperationTypes transactionType { get; private set; }
    public decimal Amount { get; private set; }
    public string SenderUsername { get; private set; }
    public string ReceiverUsername { get; private set; }
    public DateTime DateTime { get; private set; } = DateTime.UtcNow;

    public FinanceOperation(string sender, string receiver, decimal amount, FinanceOperationTypes type)
    {
        SenderUsername = sender;
        ReceiverUsername = receiver;
        Amount = amount;
        transactionType = type;
        DateTime = DateTime.Now;
    }
}
