using System.Text.Json.Serialization;

namespace FinanceTracker.Library.Settings;

public class Settings
{
    public Constrains.AuthConstraints Auth { get; set; }
    public Constrains.TransferConstraints Transfer { get; set; }
    public string FileLoadPath { get; set; }

    [JsonConstructor]
    public Settings(
        Constrains.AuthConstraints auth,
        Constrains.TransferConstraints transfer,
        string fileLoadPath
    )
    {
        Auth = auth;
        Transfer = transfer;
        FileLoadPath = fileLoadPath;
    }
}
