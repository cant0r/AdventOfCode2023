using System.Collections.Concurrent;
using System.Diagnostics;

namespace AdventOfCode.Solutions;

internal static class DayFive
{
    private static readonly string[] inputData = File.ReadAllLines("Data/dayFive.txt");

    internal static void Execute()
    {
        PartOne();
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        PartTwo();
        stopWatch.Stop();
        TimeSpan ts = stopWatch.Elapsed;

        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        Console.WriteLine("RunTime " + elapsedTime);
    }

    private static void PartOne()
    {
        var minimum = long.MaxValue;
        var seeds = new List<long>();
        var mapNames = new List<string>();
        var maps = new Dictionary<string, Dictionary<long, (long start, long length)>>();
        CompileDataset();

        foreach (var seed in seeds)
        {
            var nextLocation = seed;
            foreach (var mapKey in mapNames)
            {
                foreach (var startOffset in maps[mapKey].Keys)
                {
                    var range = maps[mapKey][startOffset];

                    if (range.start <= nextLocation && nextLocation <= (range.start + range.length))
                    {
                        nextLocation = startOffset + (nextLocation - range.start);
                        break;
                    }
                }
            }

            if (nextLocation < minimum)
            {
                minimum = nextLocation;
            }
        }

        Console.WriteLine($"Day 05 :: Part 1 solution => {minimum}");

        void CompileDataset()
        {
            seeds = inputData[0].Replace("seeds: ", string.Empty).Split().Select(s => Convert.ToInt64(s)).ToList();
            var currentMap = string.Empty;

            for (long i = 2; i < inputData.Length; i++)
            {
                if (inputData[i].Contains(":"))
                {
                    currentMap = inputData[i].Replace(":", string.Empty);
                    mapNames.Add(currentMap);
                    maps[currentMap] = new Dictionary<long, (long start, long length)>();
                    continue;
                }
                else if (string.IsNullOrEmpty(inputData[i]))
                {
                    continue;
                }
                else
                {
                    var mapEntry = inputData[i].Split().Select(s => Convert.ToInt64(s)).ToArray();
                    maps[currentMap][mapEntry[0]] = (start: mapEntry[1], length: mapEntry[2]);
                }
            }
        }
    }

    private static void PartTwo()
    {
        var minimums = new ConcurrentBag<long>();
        var seeds = new List<long>();
        var mapNames = new List<string>();
        var maps = new Dictionary<string, Dictionary<long, (long start, long length)>>();
        CompileDataset();

        Parallel.ForEach(Enumerable.Range(0, seeds.Count).Where(i => i % 2 == 0), i =>
        {
            var localMinimums = new ConcurrentBag<long>();
            Parallel.For(seeds[i], seeds[i] + seeds[i + 1], j =>
            {
                var nextLocation = j;
                foreach (var mapKey in mapNames)
                {
                    foreach (var startOffset in maps[mapKey].Keys)
                    {
                        var range = maps[mapKey][startOffset];

                        if (range.start <= nextLocation && nextLocation <= (range.start + range.length))
                        {
                            nextLocation = startOffset + (nextLocation - range.start);
                            break;
                        }
                    }
                }
                localMinimums.Add(nextLocation);
            });

            minimums.Add(localMinimums.Min());
        });

        Console.WriteLine($"Day 05 :: Part 2 solution => {minimums.Min()}");

        void CompileDataset()
        {
            seeds = inputData[0].Replace("seeds: ", string.Empty).Split().Select(s => Convert.ToInt64(s)).ToList();

            var currentMap = string.Empty;

            for (long i = 2; i < inputData.Length; i++)
            {
                if (inputData[i].Contains(":"))
                {
                    currentMap = inputData[i].Replace(":", string.Empty);
                    mapNames.Add(currentMap);
                    maps[currentMap] = new Dictionary<long, (long start, long length)>();
                    continue;
                }
                else if (string.IsNullOrEmpty(inputData[i]))
                {
                    continue;
                }
                else
                {
                    var mapEntry = inputData[i].Split().Select(s => Convert.ToInt64(s)).ToArray();
                    maps[currentMap][mapEntry[0]] = (start: mapEntry[1], length: mapEntry[2]);
                }
            }
        }
    }
}