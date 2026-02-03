namespace DriverMatching;
using System.Collections.Generic;
public class DriverFinder
{
    private readonly DriverStore _store;

    public DriverFinder(DriverStore store)
    {
        _store = store;
    }
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
    public List<Driver> FindNearestTopKList(Position orderPosition, int count = 5)
    {
        var allDrivers = _store.GetAll();
        if (allDrivers.Count == 0)
        {
            return new List<Driver>();
        }

        var best = new List<(Driver Driver, double DistSq)>();

        foreach (var driver in allDrivers)
        {
            double distSq = DistanceCalculator.SquaredEuclidean(driver.Position, orderPosition);

            if (best.Count < count)
            {
                best.Add((driver, distSq));
                continue;
            }

            int worstIndex = 0;
            double worstDist = best[0].DistSq;

            for (int i = 1; i < best.Count; i++)
            {
                if (best[i].DistSq > worstDist)
                {
                    worstDist = best[i].DistSq;
                    worstIndex = i;
                }
            }

            if (distSq < worstDist)
            {
                best[worstIndex] = (driver, distSq);
            }
        }

        best.Sort((a, b) => a.DistSq.CompareTo(b.DistSq));

        var result = new List<Driver>(count);
        foreach (var item in best)
        {
            result.Add(item.Driver);
        }

        return result;
    }
    /// Алгоритм 3 — поиск в ближайших ячейках сетки (grid-based)
    public List<Driver> FindNearestGrid(Position orderPosition, int count = 5, int cellSize = 10)
    {
        var allDrivers = _store.GetAll();
        if (allDrivers.Count == 0)
        {
            return new List<Driver>();
        }

        var grid = new Dictionary<(int Row, int Col), List<Driver>>();

        foreach (var driver in allDrivers)
        {
            int row = driver.Position.Y / cellSize;
            int col = driver.Position.X / cellSize;

            var key = (row, col);

            if (!grid.TryGetValue(key, out var cellList))
            {
                cellList = new List<Driver>();
                grid[key] = cellList;
            }

            cellList.Add(driver);
        }

        int orderRow = orderPosition.Y / cellSize;
        int orderCol = orderPosition.X / cellSize;

        var candidates = new List<Driver>();

        for (int dr = -1; dr <= 1; dr++)
        {
            for (int dc = -1; dc <= 1; dc++)
            {
                var key = (orderRow + dr, orderCol + dc);

                if (grid.TryGetValue(key, out var cell))
                {
                    candidates.AddRange(cell);
                }
            }
        }

        if (candidates.Count < count)
        {
            candidates = allDrivers;
        }

        var sorted = candidates
            .OrderBy(d => DistanceCalculator.SquaredEuclidean(d.Position, orderPosition))
            .Take(count)
            .ToList();

        return sorted;
    }

}
