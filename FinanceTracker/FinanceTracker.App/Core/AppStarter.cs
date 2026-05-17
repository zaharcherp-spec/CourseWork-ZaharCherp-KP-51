using FinanceTracker.App.CommandProviders;
using FinanceTracker.Library.Services;

namespace FinanceTracker.App.Core;

public class AppStarter
{
    private BaseCommandProvider _provider;
    private readonly AuthManager _authmanager;
    private readonly Manager _manager;

    public AppStarter(AuthManager authManager, Manager menager)
    {
        _authmanager = authManager;
        _manager = menager;
        _provider = new MenuCommandProvider(MenuChanger, _authmanager, _manager);
    }

    private void MenuChanger(BaseCommandProvider newProvider)
    {
        _provider = newProvider;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            try
            {
                await ConsoleUI.ShowAndExecute(_provider.GetCommands());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
