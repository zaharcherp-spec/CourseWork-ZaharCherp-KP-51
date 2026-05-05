using FinanceTracker.App.CommandProviders;
using FinanceTracker.App.interfaces;
using FinanceTracker.Library.Services;

namespace FinanceTracker.App.Core;

public class AppStarter
{
    private ICommandProvider _provider;
    private readonly AuthManager _authmenager;
    private readonly Menager _menager;

    public AppStarter(AuthManager authManager, Menager menager)
    {
        _authmenager = authManager;
        _menager = menager;
        _provider = new MenuCommandProvider(MenuChanger, _authmenager, _menager);
    }

    private void MenuChanger(ICommandProvider newProvider)
    {
        _provider = newProvider;
    }

    public void Run()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowAndExecute(_provider.GetCommands());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}






































































