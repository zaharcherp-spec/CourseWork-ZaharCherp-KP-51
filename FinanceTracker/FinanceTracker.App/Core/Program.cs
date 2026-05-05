using FinanceTracker.Library.Data;
using FinanceTracker.Library.Models;
using FinanceTracker.Library.Services;
using FinanceTracker.Library.Settings;

namespace FinanceTracker.App.Core;

class Program
{

    static async Task Main()
    {
        string fileLoadPath = Path.Combine("..", "FinanceTracker.Library", "Data", "users_data.json");
        var authConstraints = new Constrains.AuthConstraints();
        var transferConstrains = new Constrains.TransferConstraints();
        var appSettings = new Settings(authConstraints, transferConstrains, fileLoadPath);

        var jsonRepository = new JsonRepository<User>(appSettings.FileLoadPath);

        AuthManager authManager = new AuthManager(jsonRepository, appSettings.Auth);
        Menager menager = new Menager(authManager, transferConstrains);
        await authManager.InitAsync();

        AppStarter appStarter = new AppStarter(authManager, menager);

        appStarter.Run();
    }
}

















