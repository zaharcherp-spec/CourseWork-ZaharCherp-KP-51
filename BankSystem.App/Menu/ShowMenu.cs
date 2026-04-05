public class ShowMenu
{
    // Цей клас відповідає за показування для користувача та виклику відповідних методів ,незалажно від користувача адміна чи юзера

    private Dictionary<string, (string Name, Action action)> Actions = new();
    public ShowMenu(Dictionary<string, (string Name, Action)> commands)
    {
        Actions = commands;
    }
    public void ShowAndExecute()
    {
        foreach (var r in Actions)
        {
            Console.WriteLine($"{r.Key}--{r.Value.Name}");
        }
        string? choice = Console.ReadLine();

        bool a = Actions.TryGetValue(choice, out var choosen);

        if (a)
        {
            choosen.action.Invoke();
        }
        Console.WriteLine("Command is not found");
    }
}



