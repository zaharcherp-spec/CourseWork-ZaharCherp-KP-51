using FinanceTracker.App.CommandProviders;
using FinanceTracker.App.interfaces;


namespace FinanceTracker.App.Core;

public class AppStarter
{
    private ICommandProvider Provider;

    public AppStarter()
    {
        MenuChanger(new MenuCommandProvider());
    }

    private void MenuChanger(ICommandProvider NewProvider)
    {
        Provider = NewProvider;
    }

    public void Run()
    {
        while (true)
        {
            try
            {
                ShowMenu.ShowAndExecute(Provider.GetCommands());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}































































