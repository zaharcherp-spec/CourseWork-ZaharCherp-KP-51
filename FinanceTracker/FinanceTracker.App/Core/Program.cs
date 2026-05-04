using FinanceTracker.Library.Data;
using FinanceTracker.Library.Models;
using FinanceTracker.Library.Services;
using FinanceTracker.Library.Settings;

namespace FinanceTracker.App.Core;

class Program
{
    static async Task Main()
    {
        var settingsRepo = new JsonRepository<Settings>("settings.json");
        var loadedSettings = await settingsRepo.LoadAsync();

        Settings appSettings;

        if (loadedSettings.Count == 0)
        {
            appSettings = new Settings(
                new Constrains.AuthConstraints(),
                new Constrains.TransferConstraints(),
                "users_data.json"
            );
            await settingsRepo.SaveAsync(new List<Settings> { appSettings });
        }
        else
        {
            appSettings = loadedSettings[0];
        }

        var jsonRepository = new JsonRepository<User>(appSettings.FileLoadPath);

        AuthManager authManager = new AuthManager(jsonRepository, appSettings.Auth);
        await authManager.InitAsync();

        AppStarter appStarter = new AppStarter(authManager);
        appStarter.Run();
    }
}















