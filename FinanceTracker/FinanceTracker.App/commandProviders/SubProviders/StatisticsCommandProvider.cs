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
            {
                "0",
                (
                    "Назад до меню Юзера",
                    () =>
                    {
                        _changer(new UserCommandProvider(_changer, _authManager, _manager));
                        return Task.CompletedTask;
                    }
                )
            },
            {
                "1",
                (
                    "Показати історію операцій (за сумою)",
                    ShowStatistics
                )
            }
        };
    }

    private Task ShowStatistics()
    {
        var list = _manager.GetStatistics();
        Print(list.Result);

        return Task.CompletedTask;
    }

    private void Print(List<FinanceOperation> list)
    {
        foreach (var r in list)
        {
            Console.WriteLine(r);
        }
    }
}











