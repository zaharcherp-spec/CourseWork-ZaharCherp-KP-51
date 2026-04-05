//Це верифікаційний клас він взаємодіє з класом AppRouter і репозиторієм .
using BankSystem.Library;

public class AuthService
{
    public AuthService()
    {

    }

    public User GetUser()
    {
        return new User();
    }

    public bool GetPrivateStatus()
    {
        return true;
    }
}