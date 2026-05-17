using FinanceTracker.Library.Data;
using FinanceTracker.Library.Models;
using FinanceTracker.Library.Services;
using FinanceTracker.Library.Settings;

namespace FinanceTracker.App.Core;

class Program
{
    static async Task Main()
    {
        string projectRoot = AppContext.BaseDirectory;

        while (!Directory.Exists(Path.Combine(projectRoot, "FinanceTracker.Library")) && Directory.GetParent(projectRoot) != null)
        {
            projectRoot = Directory.GetParent(projectRoot)!.FullName;
        }


        string fileLoadPath = Path.Combine(
            projectRoot,
            "FinanceTracker.Library",
            "Data",
            "users_data.json"
        );
        var authConstraints = new Constrains.AuthConstraints();
        var transferConstrains = new Constrains.TransferConstraints();
        var appSettings = new Settings(authConstraints, transferConstrains, fileLoadPath);

        var jsonRepository = new JsonRepository<User>(appSettings.FileLoadPath);

        AuthManager authManager = new AuthManager(jsonRepository, appSettings.Auth);
        Manager manager = new Manager(authManager, transferConstrains);
        await authManager.InitAsync();

        AppStarter appStarter = new AppStarter(authManager, manager);

        await appStarter.RunAsync();
    }
}

