using FinanceTracker.App.commandProviders;
using FinanceTracker.App.interfaces;
using FinanceTracker.App.assets;

namespace FinanceTracker.App.runner;

public class AppStarter
{
    private ShowMenu _currentMenu => GetMenu();
    private ICommandProvider Provider;

    public AppStarter()
    {
        ChangeGiver(new MenuCommandProvider());
    }

    public void ChangeGiver(ICommandProvider NewProvider)
    {
        Provider = NewProvider;
    }

    public void Run()
    {
        while (true)
        {
            try
            {
                _currentMenu.ShowAndExecute();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    private ShowMenu GetMenu()
    {
        ShowMenu Menu = new ShowMenu(Provider.GetCommands());
        return Menu;
    }
}



























































