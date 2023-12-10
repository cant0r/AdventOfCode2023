using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions;

internal static class DaySix
{
    private static readonly string[] inputData = File.ReadAllLines("Data/daySix.txt");

    internal static void Execute()
    {
        PartOne();
        PartTwo();
    }

    private static void PartOne()
    {
        var accumulator = 1;
        var races = Regex.Replace(inputData[0], @"Time:\s+", string.Empty).Split().Where(n => int.TryParse(n, out _)).Select(n => Convert.ToInt32(n)).ToArray();
        var distances = Regex.Replace(inputData[1], @"Distance:\s+", string.Empty).Split().Where(n => int.TryParse(n, out _)).Select(n => Convert.ToInt32(n)).ToArray();

        for (int i = 0; i < races.Length; i++)
        {
            var possibleWins = 0;
            for (int j = 0; j <= races[i]; j++)
            {
                if (distances[i] < (races[i] - j) * j)
                {
                    possibleWins++;
                }
            }
            accumulator *= possibleWins;
        }

        Console.WriteLine($"Day 06 :: Part 1 solution => {accumulator}");
    }

    private static void PartTwo()
    {
        var accumulator = 1L;
        var race = Convert.ToInt64(Regex.Replace(inputData[0], @"(Time:\s+)|\s+", string.Empty));
        var distance = Convert.ToInt64(Regex.Replace(inputData[1], @"(Distance:\s+)|\s+", string.Empty));

        var possibleWins = 0L;
        for (long j = 0; j <= race; j++)
        {
            if (distance < (race - j) * j)
            {
                possibleWins++;
            }
        }
        accumulator *= possibleWins;

        Console.WriteLine($"Day 06 :: Part 2 solution => {accumulator}");
    }
}