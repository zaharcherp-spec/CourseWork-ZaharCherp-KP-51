namespace FinanceTracker.App.CommandProviders;

public static class ShowMenu
{
    // Цей клас відповідає за показування для користувача та виклику відповідних методів ,незалажно від користувача адміна чи юзера

    public static void ShowAndExecute(Dictionary<string, (string Name, Action)> Actions)
    {
        foreach (var r in Actions)
        {
            Console.WriteLine($"{r.Key}--{r.Value.Name}");
        }
        string? choice = Console.ReadLine();

        bool a = Actions.TryGetValue(choice??" ", out var choosen);

        if (a)
        {
            choosen.Item2.Invoke();
        }
        else
        {
            Console.WriteLine("Command is not found");
        }
    }
}





