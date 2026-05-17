namespace FinanceTracker.Library.Models;

public class FinancialSummary
{
    public Dictionary<string, decimal> ExpensesByCategory { get; set; } = new();
    public decimal TotalExpense { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal CurrentBalance { get; set; }
}






