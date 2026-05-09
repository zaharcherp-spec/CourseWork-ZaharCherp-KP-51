using FinanceTracker.App.CommandProviders;
using FinanceTracker.App.interfaces;
using FinanceTracker.Library.Services;

namespace FinanceTracker.App.Core;

public class AppStarter
{
    private BaseCommandProvider _provider;
    private readonly AuthManager _authmanager;
    private readonly Menager _menager;

    public AppStarter(AuthManager authManager, Menager menager)
    {
        _authmanager = authManager;
        _menager = menager;
        _provider = new MenuCommandProvider(MenuChanger, _authmanager, _menager);
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






































































