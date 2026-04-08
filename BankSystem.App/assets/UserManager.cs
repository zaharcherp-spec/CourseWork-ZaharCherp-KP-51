using BankSystem.Library;

public class UserManager
{
    private User Current;
    public UserManager(User user)
    {
        Current = user;
    }

    public string GetBalance()
    {
        return Current.ShowCurrentBalance();
    }
}