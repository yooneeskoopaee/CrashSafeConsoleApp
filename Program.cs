using CrashSafeConsoleApp.Services;

class Program
{
    static void Main()
    {
        // ثبت داده در فایل، در صورت کرش کردن برنامه ریکاوری مقادیر ثبت شده و ادامه
        var store = new DataStore("data.log");

        Console.WriteLine($"Recovered {store.Records.Count} records.");

        while (true)
        {
            Console.Write("Enter data (or 'exit'): ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                break;
            
            store.Append(input);

            Console.WriteLine("Saved safely.");
        }
    }
}
