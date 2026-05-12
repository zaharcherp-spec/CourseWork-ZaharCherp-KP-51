using FinanceTracker.Library.Enums;

namespace FinanceTracker.Library.Models;

public class FinanceOperation
{
    public Guid Guid { get; private set; }
    public FinanceOperationTypes TransactionType { get; private set; }
    public decimal Amount { get; private set; }
    public string SenderUsername { get; private set; }
    public string ReceiverUsername { get; private set; }
    public DateTime DateTime { get; private set; } 

    public FinanceOperation(
        string sender,
        string receiver,
        decimal amount,
        FinanceOperationTypes type
    )
    {
        Guid= Guid.NewGuid();
        SenderUsername = sender;
        ReceiverUsername = receiver;
        Amount = amount;
        TransactionType = type;
        DateTime = DateTime.Now;
    }
}
