using FinanceTracker.Library.models;

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

    public bool ChangeName(string newName)
    {
        Current.ChangeName(newName);
        return true;
    }
}