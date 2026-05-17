using FinanceTracker.App.CommandProviders.SubProviders;
using FinanceTracker.Library.Services;

namespace FinanceTracker.App.CommandProviders;

public class UserCommandProvider : BaseCommandProvider
{
    public UserCommandProvider(
        Action<BaseCommandProvider> changer,
        AuthManager authManager,
        Manager menager
    )
        : base(changer, authManager, menager) { }

    public override Dictionary<string, (string Name, Func<Task> Action)> GetCommands()
    {
        return new Dictionary<string, (string Name, Func<Task> Action)>
        {
            { "1", ("В меню фінансових операцій", GoToFinance) },
            { "2", ("В меню профілю (Зміна даних)", GoToProfile) },
            { "3", ("В меню особистої статистики", GoToStatistics) },
            { "0", ("Вийти з акаунту", Logout) },
        };
    }

    private Task GoToFinance()
    {
        _changer(new FinanceCommandProvider(_changer, _authManager, _manager));
        return Task.CompletedTask;
    }

    private Task GoToProfile()
    {
        _changer(new ProfileCommandProvider(_changer, _authManager, _manager));
        return Task.CompletedTask;
    }

    private Task GoToStatistics()
    {
        _changer(new StatisticsCommandProvider(_changer, _authManager, _manager));
        return Task.CompletedTask;
    }

    private Task Logout()
    {
        _authManager.Logout();
        _changer(new MenuCommandProvider(_changer, _authManager, _manager));
        return Task.CompletedTask;
    }
}

