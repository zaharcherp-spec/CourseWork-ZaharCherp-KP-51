using FinanceTracker.Library.Services;

namespace FinanceTracker.App.Core;

class Program
{
    // Це стартовий клас він відповідає за старт програми і ініціалізацію класів з бізнес логікою.
    static void Main()
    {
        
        AppStarter appStarter = new AppStarter();

        appStarter.Run();
    }
}
















