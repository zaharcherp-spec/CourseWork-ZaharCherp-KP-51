using FinanceTracker.Library.Models;
using FinanceTracker.Library.Services;

namespace FinanceTracker.App.CommandProviders.SubProviders;

public class StatisticsCommandProvider : BaseCommandProvider
{
    public StatisticsCommandProvider(
        Action<BaseCommandProvider> changer,
        AuthManager authManager,
        Manager manager
    )
        : base(changer, authManager, manager) { }

    public override Dictionary<string, (string Name, Func<Task> Action)> GetCommands()
    {
        return new Dictionary<string, (string Name, Func<Task> Action)>
        {
            { "1", ("Показати історію операцій (за сумою)", ShowStatistics) },
            { "2", ("Звіт за останні 7 днів", ShowWeeklySummary) },
            { "3", ("Звіт за поточний місяць", ShowMonthlySummary) },
            { "4", ("Звіт за довільний період", ShowCustomPeriodSummary) },
            { "0", ("Назад до меню Юзера", GoBack) }
        };
    }

    private Task ShowStatistics()
    {
        var list = _manager.GetStatistics().Result;
        Print(list);

        return Task.CompletedTask;
    }

    private void Print(List<FinanceOperation> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Операцій не знайдено.");
            return;
        }

        foreach (var r in list)
        {
            Console.WriteLine(r);
        }
    }

    private async Task ShowWeeklySummary()
    {
        var endDate = DateTime.Now;
        var startDate = endDate.AddDays(-7);

        await GenerateAndPrintSummary(startDate, endDate, "останні 7 днів");
    }

    private async Task ShowMonthlySummary()
    {
        var endDate = DateTime.Now;

        var startDate = new DateTime(endDate.Year, endDate.Month, 1);

        await GenerateAndPrintSummary(startDate, endDate, "поточний місяць");
    }

    private async Task ShowCustomPeriodSummary()
    {
        Console.Write("Введіть початкову дату (формат дд.мм.рррр): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
        {
            Console.WriteLine("Помилка: Неправильний формат дати.");
            return;
        }

        Console.Write("Введіть кінцеву дату (формат дд.мм.рррр): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
        {
            Console.WriteLine("Помилка: Неправильний формат дати.");
            return;
        }


        endDate = endDate.Date.AddDays(1).AddTicks(-1);

        await GenerateAndPrintSummary(startDate, endDate, $"період з {startDate:dd.MM.yyyy} по {endDate:dd.MM.yyyy}");
    }

    private async Task GenerateAndPrintSummary(DateTime startDate, DateTime endDate, string periodName)
    {
        var username = _authManager.CurrentUser.UserName;
        var summary = await _manager.GetSummaryAsync(username, startDate, endDate);

        Console.WriteLine($"\n Фінансовий звіт за {periodName} ");
        Console.WriteLine($"Поточний баланс:  {summary.CurrentBalance,10:N2}");
        Console.WriteLine($"Загальний дохід:  {summary.TotalIncome,10:N2}");
        Console.WriteLine($"Загальні витрати: {summary.TotalExpense,10:N2}");

        Console.WriteLine("\nДеталізація витрат за категоріями:");

        if (summary.ExpensesByCategory.Count == 0)
        {
            Console.WriteLine("  Витрат у цьому періоді не було.");
        }
        else
        {
            foreach (var category in summary.ExpensesByCategory)
            {
                Console.WriteLine($"  - {category.Key}: {category.Value:N2}");
            }
        }
    }

    private Task GoBack()
    {
        _changer(new UserCommandProvider(_changer, _authManager, _manager));
        return Task.CompletedTask;
    }
}














