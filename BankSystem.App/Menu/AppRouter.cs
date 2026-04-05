public class AppRouter
{
    //Цей клас відповідає за вибір типу користувача програми та надання йому відповідних можливостей можливостей .Також в цьому класі
    // і знаходиться відповідні списки опцій ,які будуть представлені в менеджерахі.Цей клас може спілкуватись з корисутвачем через консоль.
    private ShowMenu _currentMenu;
    private readonly AuthService authService;
    public AppRouter(AuthService authService)
    {
        _currentMenu = CreateWelcomeMenu();
        this.authService = authService;
    }
    public void Run()
    {
        while (true)
        {
            _currentMenu.ShowAndExecute();
        }
    }

    //Private methods
    private ShowMenu CreateWelcomeMenu()
    {
        var dict = new Dictionary<string, (string Name, Action action)>();

        dict.Add("1", ("Enter as User", () => _currentMenu = CreateUserMenu()));
        dict.Add("2", ("Enter as administarator", () => _currentMenu = CreateAdminMenu()));
        dict.Add("0", ("Exit", () => Environment.Exit(0)));

        return new ShowMenu(dict);
    }

    private ShowMenu CreateUserMenu()
    {
        var dict = new Dictionary<string, (string Name, Action action)>();

        dict.Add("0", ("Exit from your account", () => _currentMenu = CreateWelcomeMenu()));

        return new ShowMenu(dict);
    }

    private ShowMenu CreateAdminMenu()
    {
        var dict = new Dictionary<string, (string Name, Action action)>();
        dict.Add("0", ("Exit from Adminisrator Status", () => _currentMenu = CreateWelcomeMenu()));

        return new ShowMenu(dict);
    }
}
        








































