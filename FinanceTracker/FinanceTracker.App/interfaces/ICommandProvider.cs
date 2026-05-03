namespace FinanceTracker.App.interfaces;
public interface ICommandProvider
{
    Dictionary<string, (string, Action)> GetCommands();
}