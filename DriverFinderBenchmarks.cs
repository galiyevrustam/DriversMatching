using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using DriverMatching;
using System;
using System.Collections.Generic;

namespace DriverMatching.Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class DriverFinderBenchmarks
    {
        private DriverStore _store;
        private DriverFinder _finder;
        private Position _orderPosition;

        [Params(1000, 10000, 50000)]
        public int DriverCount { get; set; }

        [Params(100, 1000)]
        public int MapSize { get; set; }

        private const int NearestCount = 5;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _store = new DriverStore(MapSize, MapSize);
            _finder = new DriverFinder(_store);

            var random = new Random(42);

            for (int i = 0; i < DriverCount; i++)
            {
                string id = $"D{i:D6}";
                int x = random.Next(MapSize);
                int y = random.Next(MapSize);
                _store.AddOrUpdate(id, x, y);
            }

            // Заказ где-то посередине
            _orderPosition = new Position(MapSize / 2, MapSize / 2);
        }

        [Benchmark(Baseline = true)]
        public void LinearSort()
        {
            _finder.FindNearestLinearSort(_orderPosition, NearestCount);
        }

        [Benchmark]
        public void TopKList()
        {
            _finder.FindNearestTopKList(_orderPosition, NearestCount);
        }

        [Benchmark]
        public void GridSearch()
        {
            _finder.FindNearestGrid(_orderPosition, NearestCount, cellSize: 20);
        }
    }
}