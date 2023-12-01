using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal static class DayOne
    {
        internal static void Execute()
        {
            PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            var inputData = File.ReadAllLines("Data/dayOne.txt");
            var accumulator = 0;
            var firstDigitRegex = new Regex("[a-z]*([0-9])[a-z]*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var lastDigitRegex = new Regex("[a-z]*([0-9])[a-z]*", RegexOptions.RightToLeft | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            foreach (var line in inputData)
            {
                var firstMatch = firstDigitRegex.Match(line).Groups[1];
                var lastMatch = lastDigitRegex.Match(line).Groups[1];

                accumulator += Convert.ToInt32($"{firstMatch}{lastMatch}");
            }

            Console.WriteLine($"Day 01 :: Part 1 solution => {accumulator}");
        }

        private static void PartTwo()
        {
            var inputData = File.ReadAllLines("Data/dayOne.txt");
            var accumulator = 0;
            Dictionary<string, int> numberDictionary = new()
            {
                { "zero", 0 },
                { "one", 1},
                { "two", 2},
                { "three", 3},
                { "four", 4},
                { "five", 5},
                { "six", 6},
                { "seven", 7},
                { "eight", 8},
                { "nine", 9},
            };

            var wordPattern = string.Join("|", numberDictionary.Keys);

            var firstDigitRegex = new Regex($@"([0-9]|{wordPattern})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var lastDigitRegex = new Regex($@"([0-9]|{wordPattern})", RegexOptions.RightToLeft | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            foreach (var line in inputData)
            {
                var firstMatch = firstDigitRegex.Match(line).Groups[1].Value;
                var lastMatch = lastDigitRegex.Match(line).Groups[1].Value;

                if (numberDictionary.ContainsKey(firstMatch))
                {
                    firstMatch = Convert.ToString(numberDictionary[firstMatch]);
                }

                if (numberDictionary.ContainsKey(lastMatch))
                {
                    lastMatch = Convert.ToString(numberDictionary[lastMatch]);
                }

                accumulator += Convert.ToInt32($"{firstMatch}{lastMatch}");
            }

            Console.WriteLine($"Day 01 :: Part 2 solution => {accumulator}");
        }
    }
}