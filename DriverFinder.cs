namespace DriverMatching;

public class DriverFinder
{
    private readonly DriverStore _store;

    public DriverFinder(DriverStore store)
    {
        _store = store;
    }

    /// <summary>
    /// Самый простой алгоритм: перебрать всех → отсортировать → взять первые 5
    /// </summary>
    public List<Driver> FindNearestLinearSort(Position orderPosition, int count = 5)
    {
        var allDrivers = _store.GetAll();

        if (allDrivers.Count == 0)
        {
            return new List<Driver>();
        }

        var sorted = allDrivers
            .OrderBy(d => DistanceCalculator.SquaredEuclidean(d.Position, orderPosition))
            .Take(count)
            .ToList();

        return sorted;
    }
}