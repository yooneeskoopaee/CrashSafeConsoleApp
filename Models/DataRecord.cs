namespace CrashSafeConsoleApp.Models;

public record DataRecord(Guid Id, string Value, DateTime CreatedAt)
{
    public override string ToString() => $"{Id}|{CreatedAt:O}|{Value}";

    public static bool TryParse(string line, out DataRecord? record)
    {
        record = null;

        var parts = line.Split('|', 3);
        if (parts.Length != 3)
            return false;

        if (!Guid.TryParse(parts[0], out var id))
            return false;

        if (!DateTime.TryParse(parts[1], out var date))
            return false;

        record = new DataRecord(id, parts[2], date);
        return true;
    }
}
