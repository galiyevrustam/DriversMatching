using DriverMatching;
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Подбор водителей\n");

        var store = new DriverStore();

        // Добавляем водителей
        store.AddOrUpdate("D001", 10, 15);
        store.AddOrUpdate("D002", 3, 4);
        store.AddOrUpdate("D007", 20, 8);
        store.AddOrUpdate("D012", 11, 14);
        store.AddOrUpdate("D005", 1, 1);
        store.AddOrUpdate("D009", 9, 11);
        store.AddOrUpdate("D015", 15, 20);

        // Выводим всех для справки
        Console.WriteLine($"Всего водителей: {store.Count}");
        Console.WriteLine("Список водителей:");
        foreach (var driver in store.GetAll())
        {
            Console.WriteLine(driver);
        }

        // Точка заказа
        var order = new Position(10, 10);
        Console.WriteLine($"\nЗаказ находится здесь: {order}");

        // Ищем 5 ближайших
        var finder = new DriverFinder(store);
        var nearest = finder.FindNearestLinearSort(order, 5);

        Console.WriteLine("\n5 ближайших водителей:");
        foreach (var driver in nearest)
        {
            double dist = Math.Round(DistanceCalculator.Euclidean(driver.Position, order), 2);
            Console.WriteLine($"{driver}  —  {dist,6:0.00}");
        }
        Console.WriteLine("\n5 ближайших (Top-K List):");
        var nearest2 = finder.FindNearestTopKList(order, 5);

        foreach (var driver in nearest2)
        {
            double dist = Math.Round(DistanceCalculator.Euclidean(driver.Position, order), 2);
            Console.WriteLine($"{driver}  —  {dist,6:0.00}");
        }
        Console.WriteLine("\nНажмите Enter для выхода...");
        Console.ReadLine();

    }
}