namespace DriverMatching;

public class DriverStore
{
    private readonly Dictionary<string, Driver> _drivers = new();
    public int N { get; }
    public int M { get; }

    public DriverStore(int n, int m)
    {
        if (n <= 0 || m <= 0) throw new ArgumentException("Размеры карты должны быть > 0");
        N = n;
        M = m;
    }

    public DriverStore()
    {
        N = int.MaxValue;
        M = int.MaxValue;
    }
    public void AddOrUpdate(string id, int x, int y)
    {
        if (x < 0 || x >= N || y < 0 || y >= M)
            throw new ArgumentOutOfRangeException($"Координаты ({x}, {y}) вне карты {N}×{M}");

        var position = new Position(x, y);

        if (_drivers.TryGetValue(id, out var existing))
        {
            existing.UpdatePosition(position);
        }
        else
        {
            _drivers[id] = new Driver(id, position);
        }
    }

    public Driver? GetById(string id)
    {
        _drivers.TryGetValue(id, out var driver);
        return driver;
    }

    public List<Driver> GetAll()
    {
        return new List<Driver>(_drivers.Values);
    }

    public int Count => _drivers.Count;
}