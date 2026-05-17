using System.Text.Json.Serialization;
using FinanceTracker.Library.Enums;

namespace FinanceTracker.Library.Models;

public class FinanceOperation
{
    public string Id { get; private set; }
    public FinanceOperationTypes FinanceOperationType { get; private set; }
    public decimal Amount { get; private set; }
    public string SenderUsername { get; private set; }
    public string ReceiverUsername { get; private set; }
    public DateTime Date { get; private set; }
    public string Category { get; private set; }

    [JsonConstructor]
    public FinanceOperation(
        string id,
        FinanceOperationTypes financeoperationType,
        decimal amount,
        string senderUsername,
        string receiverUsername,
        DateTime date,
        string category
    )
    {
        Id = id;
        FinanceOperationType = financeoperationType;
        Amount = amount;
        SenderUsername = senderUsername;
        ReceiverUsername = receiverUsername;
        Date = date;
        Category = category ?? string.Empty;
    }

    public FinanceOperation(
        string sender,
        string receiver,
        decimal amount,
        FinanceOperationTypes type,
        string category
    )
    {
        Id = Guid.NewGuid().ToString();
        SenderUsername = sender;
        ReceiverUsername = receiver;
        Amount = amount;
        FinanceOperationType = type;
        Date = DateTime.Now;
        Category = category ?? string.Empty;
    }

    public override string ToString()
    {
        string operationSign = FinanceOperationType == FinanceOperationTypes.Withdrawal ? "-" : "+";
        string transferInfo = FinanceOperationType == FinanceOperationTypes.Transfer 
            ? $" | Від: {SenderUsername} -> Кому: {ReceiverUsername}" 
            : "";

        return $"[{Date:dd.MM.yyyy HH:mm}] {FinanceOperationType,-10} | Категорія: {Category,-10} | Сума: {operationSign}{Amount:N2}{transferInfo}";
    }
}
