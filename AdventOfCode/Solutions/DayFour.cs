using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions;

internal static class DayFour
{
    private static readonly string[] inputData = File.ReadAllLines("Data/dayFour.txt");

    internal static void Execute()
    {
        PartOne();
        PartTwo();
    }

    private static void PartOne()
    {
        var accumulator = 0;

        for (int i = 0; i < inputData.Length; i++)
        {
            var cardValue = 0;
            var numbers = inputData[i].Replace($"Card {i + 1}:", string.Empty).Trim();
            var numberPartitions = numbers.Split("|");
            var winningNumberSet = Regex.Replace(numberPartitions[0].Trim(), @"\s\s+", @" ").Split(" ").ToHashSet();
            var myNumbers = Regex.Replace(numberPartitions[1].Trim(), @"\s\s+", @" ").Split(" ");

            foreach (var myNumber in myNumbers)
            {
                if (winningNumberSet.Contains(myNumber))
                {
                    if (cardValue == 0)
                    {
                        cardValue = 1;
                    }
                    else
                    {
                        cardValue *= 2;
                    }
                }
            }

            accumulator += cardValue;
        }

        Console.WriteLine($"Day 04 :: Part 1 solution => {accumulator}");
    }

    private static void PartTwo()
    {
        var accumulator = 0;
        var cardCache = new Dictionary<int, List<string>>();

        for (int i = 0; i < inputData.Length; i++)
        {
            var cardValue = GetCardValue(inputData[i], i);

            for (int j = 1; j <= cardValue && i + j < inputData.Length; j++)
            {
                cardCache.TryGetValue(i + j, out var cardCopies);

                if (cardCopies is null)
                {
                    cardCache[i + j] = new() { inputData[i + j] };
                }
                else
                {
                    cardCopies.Add(inputData[i + j]);
                    cardCache[i + j] = cardCopies;
                }
            }

            cardCache.TryGetValue(i, out var clones);
            foreach (var copyCard in clones ?? [])
            {
                var copyCardValue = GetCardValue(copyCard, i);

                for (int j = 1; j <= copyCardValue && i + j < inputData.Length; j++)
                {
                    cardCache.TryGetValue(i + j, out var cardCopies);

                    if (cardCopies is null)
                    {
                        cardCache[i + j] = new() { inputData[i + j] };
                    }
                    else
                    {
                        cardCopies.Add(inputData[i + j]);
                        cardCache[i + j] = cardCopies;
                    }
                }

                accumulator += 1;
            }

            accumulator += 1;
        }

        Console.WriteLine($"Day 04 :: Part 2 solution => {accumulator}");

        int GetCardValue(string card, int cardIndex)
        {
            var cardValue = 0;
            var numbers = card.Replace($"Card {cardIndex + 1}:", string.Empty).Trim();
            var numberPartitions = numbers.Split("|");
            var winningNumberSet = Regex.Replace(numberPartitions[0].Trim(), @"\s\s+", @" ").Split(" ").ToHashSet();
            var myNumbers = Regex.Replace(numberPartitions[1].Trim(), @"\s\s+", @" ").Split(" ");

            foreach (var myNumber in myNumbers)
            {
                if (winningNumberSet.Contains(myNumber))
                {
                    cardValue += 1;
                }
            }

            return cardValue;
        }
    }
}