using FinanceTracker.App.CommandProviders;
using FinanceTracker.App.interfaces;
using FinanceTracker.Library.Services;

namespace FinanceTracker.App.Core;

public class AppStarter
{
    private ICommandProvider Provider;
    private AuthManager AuthManager;

    public AppStarter(AuthManager authManager)
    {
        AuthManager = authManager;
        Provider = new MenuCommandProvider(MenuChanger, AuthManager);
    }

    private void MenuChanger(ICommandProvider newProvider)
    {
        Provider = newProvider;
    }

    public void Run()
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowAndExecute(Provider.GetCommands());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}






































































