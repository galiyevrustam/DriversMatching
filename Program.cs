using BenchmarkDotNet.Running;
using DriverMatching.Benchmarks;

class Program
{
    static void Main(string[] args)
    {
        // Запуск бенчмарков
        BenchmarkRunner.Run<DriverFinderBenchmarks>();

        Console.WriteLine("\nБенчмарки завершены. Нажмите Enter...");
        Console.ReadLine();
    }
}