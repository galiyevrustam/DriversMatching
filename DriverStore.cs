namespace DriverMatching;

public class DriverStore
{
    private readonly Dictionary<string, Driver> _drivers = new();

    public void AddOrUpdate(string id, int x, int y)
    {
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