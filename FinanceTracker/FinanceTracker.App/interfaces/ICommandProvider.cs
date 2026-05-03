public interface ICommandProvider
{
    Dictionary<string, (string, Action)> GetCommands();
}