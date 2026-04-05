class Program
{
    // Це стартовий клас він відповідає за старт програми і ініціалізацію класів з бізнес логікою.
    static void Main()
    {
        AuthService authService = new AuthService();
        AppRouter appRouter = new AppRouter(authService);
        
        appRouter.Run();
    }
}














