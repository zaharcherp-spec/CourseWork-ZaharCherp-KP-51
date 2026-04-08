public interface ITaskGiver
{
    Dictionary<string, (string, Action)> GetCommands();
}