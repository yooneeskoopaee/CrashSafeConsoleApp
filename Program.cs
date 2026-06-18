using CrashSafeConsoleApp.Services;

class Program
{
    static void Main()
    {
        var store = new DataStore("data.log");

        Console.WriteLine($"Recovered {store.Records.Count} records.");

        while (true)
        {
            Console.Write("Enter data (or 'exit'): ");
            var input = Console.ReadLine();

            if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                break;

            if (string.IsNullOrWhiteSpace(input))
                continue;

            store.Append(input);

            Console.WriteLine("Saved safely.");
        }
    }
}
