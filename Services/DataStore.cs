using CrashSafeConsoleApp.Models;
using System.Text;

namespace CrashSafeConsoleApp.Services;

public class DataStore
{
    private readonly string _filePath;
    private readonly HashSet<Guid> _knownIds = new();
    private readonly List<DataRecord> _records = new();
    private readonly object _lock = new();

    public IReadOnlyCollection<DataRecord> Records => _records;

    public DataStore(string filePath)
    {
        _filePath = filePath;
        Recover();
    }

    private void Recover()
    {
        if (!File.Exists(_filePath))
            return;

        foreach (var line in File.ReadLines(_filePath))
        {
            if (!DataRecord.TryParse(line, out var record))
                continue;

            if (_knownIds.Add(record!.Id))
            {
                _records.Add(record);
            }
        }
    }

    public void Append(string value)
    {
        var record = new DataRecord(Guid.NewGuid(), value, DateTime.UtcNow);

        lock (_lock)
        {
            using var stream = new FileStream(
                _filePath,
                FileMode.Append,
                FileAccess.Write,
                FileShare.Read,
                4096,
                FileOptions.WriteThrough);

            using var writer = new StreamWriter(stream, Encoding.UTF8);

            writer.WriteLine(record.ToString());
            writer.Flush();
            stream.Flush(true);

            _records.Add(record);
            _knownIds.Add(record.Id);
        }
    }
}
