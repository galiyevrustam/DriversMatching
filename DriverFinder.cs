namespace DriverMatching;
using System.Collections.Generic;
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

        // Сортируем по квадрату расстояния (быстрее, чем с корнем)
        var sorted = allDrivers
            .OrderBy(d => DistanceCalculator.SquaredEuclidean(d.Position, orderPosition))
            .Take(count)
            .ToList();

        return sorted;
    }
    /// <summary>
    /// Алгоритм 2: используем PriorityQueue (max-heap), чтобы держать только топ-K ближайших
    /// </summary>
    /// <summary>
    /// Алгоритм 2 — поддерживаем список из 5 лучших кандидатов, заменяем худшего при необходимости
    /// </summary>
    public List<Driver> FindNearestTopKList(Position orderPosition, int count = 5)
    {
        var allDrivers = _store.GetAll();
        if (allDrivers.Count == 0)
        {
            return new List<Driver>();
        }

        // Будем хранить список из count ближайших
        var best = new List<(Driver Driver, double DistSq)>();

        foreach (var driver in allDrivers)
        {
            double distSq = DistanceCalculator.SquaredEuclidean(driver.Position, orderPosition);

            // Если ещё не набрали count кандидатов — просто добавляем
            if (best.Count < count)
            {
                best.Add((driver, distSq));
                continue;
            }

            // Находим худшего (самое большое расстояние) в текущем топе
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

            // Если новый водитель ближе худшего — заменяем
            if (distSq < worstDist)
            {
                best[worstIndex] = (driver, distSq);
            }
        }

        // Сортируем результат по расстоянию (от ближайшего к дальнему)
        best.Sort((a, b) => a.DistSq.CompareTo(b.DistSq));

        // Возвращаем только водителей
        var result = new List<Driver>(count);
        foreach (var item in best)
        {
            result.Add(item.Driver);
        }

        return result;
    }
}
