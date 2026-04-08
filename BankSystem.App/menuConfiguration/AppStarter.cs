public class AppStarter
{
    private ShowMenu _currentMenu => GetMenu();
    private ITaskGiver Giver;

    public AppStarter()
    {
        Giver = new GreeterGiver(ChangeGiver);
    }

    public void ChangeGiver(ITaskGiver newGiver)
    {
        Giver = newGiver;
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
        ShowMenu Menu = new ShowMenu(Giver.GetCommands());
        return Menu;
    }
}



























































