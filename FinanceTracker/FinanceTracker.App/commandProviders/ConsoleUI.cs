namespace FinanceTracker.App.CommandProviders;

public static class ConsoleUI
{
    public static void ShowAndExecute(Dictionary<string, (string Name, Action Action)> actions)
    {
        Console.WriteLine("\n---------------------------");
        foreach (var r in actions)
        {
            Console.WriteLine($"{r.Key} -- {r.Value.Name}");
        }
        Console.WriteLine("-----------------");
        Console.Write("Ваш вибір: ");

        string choice = Console.ReadLine() ?? "";

        if (actions.TryGetValue(choice, out var chosen))
        {
            Console.WriteLine();
            chosen.Action.Invoke();
        }
        else
        {
            Console.WriteLine("Команду не знайдено.");
        }
    }
}







