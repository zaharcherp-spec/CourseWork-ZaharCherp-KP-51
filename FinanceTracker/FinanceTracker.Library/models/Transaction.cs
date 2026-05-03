using FinanceTracker.Library.enums;

namespace FinanceTracker.Library.models;

public class Transaction
{
    public TransactionType transactionType { get; private set; }
    public decimal Amount { get; private set; }
    public Guid Sender { get; private set; }
    public Guid Getter { get; private set; }
    public DateTime DateTime { get; private set; } = DateTime.UtcNow;
}
