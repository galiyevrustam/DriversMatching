using DriverMatching;
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Подбор водителей (начало)\n");

        var store = new DriverStore();

        // Добавляем несколько водителей
        store.AddOrUpdate("D001", 10, 15);
        store.AddOrUpdate("D002", 3, 4);
        store.AddOrUpdate("D007", 20, 8);
        store.AddOrUpdate("D012", 11, 14);  // обновляем или добавляем
        store.AddOrUpdate("D005", 1, 1);

        Console.WriteLine($"Всего водителей: {store.Count}");
        Console.WriteLine("Список всех водителей:");

        foreach (var driver in store.GetAll())
        {
            Console.WriteLine(driver);
        }

        Console.WriteLine("\nНажмите Enter для выхода...");
        Console.ReadLine();
    }
}