using System.Text;

namespace AdventOfCode.Solutions;

internal static class DayThree
{
    private static readonly char[] digits = "0123456789".ToCharArray();
    private static readonly string[] inputData = File.ReadAllLines("Data/dayThree.txt");

    internal static void Execute()
    {
        PartOne();
        PartTwo();
    }

    private static void PartOne()
    {
        var accumulator = 0;
        var parsedDigits = new StringBuilder();

        for (int i = 0; i < inputData.Length; i++)
        {
            var encounteredSpecial = false;
            for (int j = 0; j < inputData[i].Length; j++)
            {
                if (!digits.Contains(inputData[i][j]))
                {
                    if (encounteredSpecial)
                    {
                        var number = parsedDigits.ToString();
                        accumulator += Convert.ToInt32(number);
                        encounteredSpecial = false;
                    }
                    parsedDigits.Clear();
                }
                else
                {
                    parsedDigits.Append(inputData[i][j]);

                    var topIndex = Math.Max(0, i - 1);
                    var bottomIndex = Math.Min(inputData.Length - 1, i + 1);
                    var leftIndex = Math.Max(0, j - 1);
                    var rightIndex = Math.Min(inputData[i].Length - 1, j + 1);

                    encounteredSpecial |= IsMarker(inputData[topIndex][leftIndex]);
                    encounteredSpecial |= IsMarker(inputData[topIndex][rightIndex]);
                    encounteredSpecial |= IsMarker(inputData[bottomIndex][leftIndex]);
                    encounteredSpecial |= IsMarker(inputData[bottomIndex][rightIndex]);
                    encounteredSpecial |= IsMarker(inputData[topIndex][j]);
                    encounteredSpecial |= IsMarker(inputData[bottomIndex][j]);
                    encounteredSpecial |= IsMarker(inputData[i][leftIndex]);
                    encounteredSpecial |= IsMarker(inputData[i][rightIndex]);
                }
            }

            if (encounteredSpecial)
            {
                var number = parsedDigits.ToString();
                accumulator += Convert.ToInt32(number);
            }
            parsedDigits.Clear();
        }

        Console.WriteLine($"Day 03 :: Part 1 solution => {accumulator}");
    }

    private static void PartTwo()
    {
        var accumulator = 0;
        var gearNumbers = new Dictionary<(int x, int y), string>();
        var parsedDigits = new StringBuilder();
        (int x, int y) gearMarkerIndex = (-1, -1);

        for (int i = 0; i < inputData.Length; i++)
        {
            var encounteredSpecial = false;
            for (int j = 0; j < inputData[i].Length; j++)
            {
                if (!digits.Contains(inputData[i][j]))
                {
                    if (encounteredSpecial)
                    {
                        var number = parsedDigits.ToString();

                        if (gearNumbers.ContainsKey(gearMarkerIndex))
                        {
                            accumulator += Convert.ToInt32(number) * Convert.ToInt32(gearNumbers[gearMarkerIndex]);
                        }
                        else
                        {
                            gearNumbers[gearMarkerIndex] = number;
                        }

                        encounteredSpecial = false;
                    }
                    parsedDigits.Clear();
                }
                else
                {
                    parsedDigits.Append(inputData[i][j]);
                    var localGearMarkerIndex = TryGetGearMarker(i, j);

                    if (localGearMarkerIndex != (-1, -1))
                    {
                        gearMarkerIndex = localGearMarkerIndex;
                        encounteredSpecial = true;
                    }
                }
            }

            if (encounteredSpecial)
            {
                var number = parsedDigits.ToString();

                if (gearNumbers.ContainsKey(gearMarkerIndex))
                {
                    accumulator += Convert.ToInt32(number) * Convert.ToInt32(gearNumbers[gearMarkerIndex]);
                }
                else
                {
                    gearNumbers[gearMarkerIndex] = number;
                }
            }
            parsedDigits.Clear();
        }

        Console.WriteLine($"Day 03 :: Part 2 solution => {accumulator}");
    }

    private static bool IsMarker(char borderCharacter)
    {
        return !digits.Contains(borderCharacter) && borderCharacter != '.';
    }

    private static (int x, int y) TryGetGearMarker(int x, int y)
    {
        var topIndex = Math.Max(0, x - 1);
        var bottomIndex = Math.Min(inputData.Length - 1, x + 1);
        var leftIndex = Math.Max(0, y - 1);
        var rightIndex = Math.Min(inputData[x].Length - 1, y + 1);

        for (int i = topIndex; i <= bottomIndex; i++)
        {
            for (int j = leftIndex; j <= rightIndex; j++)
            {
                if (inputData[i][j] == '*')
                {
                    return (i, j);
                }
            }
        }

        return (-1, -1);
    }
}