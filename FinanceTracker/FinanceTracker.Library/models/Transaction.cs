public class Transaction
{
    public TransactionType transactionType { get; set; }
    public decimal Amount { get; set; }
    public Guid Sender { get; set; }
    public Guid Getter { get; set; }
    public DateTime DateTime { get; set; } = DateTime.UtcNow;
}
