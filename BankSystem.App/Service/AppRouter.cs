public class AppRouter
{
    //Цей клас відповідає за вибір типу користувача програми та надання йому відповідних можливостей можливостей .Також в цьому класі
    // і знаходиться відповідні списки опцій ,які будуть представлені в менеджерахі.Цей клас може спілкуватись з корисутвачем через консоль.
    private AdminManeger Maneger;
    public AppRouter(Maneger Meneger)
    {
        this.Maneger = Meneger;
    }

    private Roles GetRoles()
    {
        Console.WriteLine("Your role: User / Admin");
        string? choice = Console.ReadLine().ToLower();

        switch (choice)
        {
            case "user":
                return Roles.User;

            case "admin":
                return Roles.Admin;

            default: return Roles.Uncknown;
        }
    }
    public BaseMenu Create()
    {
        Roles role = GetRoles();
        var dictionary = new Dictionary<string, (string Name, Action action)>();

        BaseMenu Menu;
        if (role == Roles.Admin)
        {

        }
        else if (role == Roles.User)
        {

        }
        else
        {
            Console.WriteLine("You have no options just yet / Starting Verification");
            Create();
        }
        return Menu = new BaseMenu(dictionary);
    }
}










