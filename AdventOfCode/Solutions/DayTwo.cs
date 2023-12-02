namespace AdventOfCode.Solutions;

internal static class DayTwo
{
    internal static void Execute()
    {
        PartOne();
        PartTwo();
    }

    private static void PartOne()
    {
        var inputData = File.ReadAllLines("Data/dayTwo.txt");
        var accumulator = 0;
        Dictionary<string, int> rules = new()
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        foreach (var line in inputData)
        {
            var gameData = line.Split(":");
            var gameId = Convert.ToInt32(gameData[0].Split()[1]);
            var validGame = true;
            var turns = gameData[1].Replace(";", ",").Split(",");

            foreach (var coloredCubes in turns)
            {
                var drawn = coloredCubes.Trim().Split();
                var limit = rules[drawn[1]];
                var drawnNumberOfCubes = Convert.ToInt32(drawn[0]);
                validGame &= drawnNumberOfCubes <= limit;

                if (!validGame)
                {
                    break;
                }
            }

            if (validGame)
            {
                accumulator += gameId;
            }
        }

        Console.WriteLine($"Day 02 :: Part 1 solution => {accumulator}");
    }

    private static void PartTwo()
    {
        var inputData = File.ReadAllLines("Data/dayTwo.txt");
        var accumulator = 0;

        foreach (var line in inputData)
        {
            Dictionary<string, int> maximums = new()
            {
                { "red", 1 },
                { "green", 1 },
                { "blue", 1 }
            };
            var turns = line.Split(":")[1].Replace(";", ",").Split(",");

            foreach (var coloredCubes in turns)
            {
                var drawn = coloredCubes.Trim().Split();
                var max = maximums[drawn[1]];
                var drawnNumberOfCubes = Convert.ToInt32(drawn[0]);
                
                if (drawnNumberOfCubes > max)
                {
                    maximums[drawn[1]] = drawnNumberOfCubes;
                }

            }
            var product = maximums.Values.Aggregate((int a, int b) => { return a * b; });
            accumulator += product;
        }

        Console.WriteLine($"Day 02 :: Part 2 solution => {accumulator}");
    }
}