namespace FinanceTracker.App.CommandProviders;

public static class ConsoleUI
{
    public static async Task ShowAndExecute(Dictionary<string, (string Name, Func<Task>)> actions)
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
            await chosen.Item2.Invoke();
        }
        else
        {
            Console.WriteLine("Команду не знайдено.");
        }
    }
}







