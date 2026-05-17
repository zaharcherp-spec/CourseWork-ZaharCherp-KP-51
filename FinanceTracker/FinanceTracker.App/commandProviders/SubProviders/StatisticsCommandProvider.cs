using FinanceTracker.Library.Services;

namespace FinanceTracker.App.CommandProviders.SubProviders;

public class StatisticsCommandProvider : BaseCommandProvider
{
    public StatisticsCommandProvider(
        Action<BaseCommandProvider> changer,
        AuthManager authManager,
        Manager menager
    )
        : base(changer, authManager, menager) { }

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
        };
    }
}

