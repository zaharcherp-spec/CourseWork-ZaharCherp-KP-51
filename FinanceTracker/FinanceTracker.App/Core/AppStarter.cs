using FinanceTracker.App.CommandProviders;
using FinanceTracker.App.interfaces;
using FinanceTracker.Library.Data;
using FinanceTracker.Library.Models;
using FinanceTracker.Library.Services;

namespace FinanceTracker.App.Core;

public class AppStarter
{
    private ICommandProvider Provider;
    private AuthManager AuthManager;

    public AppStarter()
    {
        
        var repository = new JsonRepository<User>("users_data");

        AuthManager = new AuthManager(repository);
        AuthManager.InitAsync().Wait();

        
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



































































