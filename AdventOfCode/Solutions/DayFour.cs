using System.Text;

namespace AdventOfCode.Solutions;

internal static class DayFour
{
    private static readonly char[] digits = "0123456789".ToCharArray();
    private static readonly string[] inputData = File.ReadAllLines("Data/dayFour.txt");

    internal static void Execute()
    {
        PartOne();
        PartTwo();
    }

    private static void PartOne()
    {
        var accumulator = 0;

        Console.WriteLine($"Day 04 :: Part 1 solution => {accumulator}");
    }

    private static void PartTwo()
    {
        var accumulator = 0;

        Console.WriteLine($"Day 04 :: Part 2 solution => {accumulator}");
    }
}