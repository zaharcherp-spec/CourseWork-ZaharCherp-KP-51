class Program
{
    // Це стартовий клас він відповідає за старт програми і ініціалізацію класів з бізнес логікою.
    static void Main()
    {
        Maneger meneger = new Maneger();
        AppRouter appRouter = new AppRouter(meneger);

        var menu = appRouter.Create();

    }
}











