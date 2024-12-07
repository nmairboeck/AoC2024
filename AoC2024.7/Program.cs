using System.Diagnostics;

await Run("example.txt");

var stopwatch = Stopwatch.StartNew();
await Run("input.txt");
stopwatch.Stop();
Console.WriteLine($"Part 1 took {stopwatch.ElapsedMilliseconds}ms");

await RunPart2("example.txt");

stopwatch = Stopwatch.StartNew();
await RunPart2("input.txt");
stopwatch.Stop();
Console.WriteLine($"Part 2 took {stopwatch.ElapsedMilliseconds}ms");

async Task Run(string fileName)
{
    Console.WriteLine($"Running {fileName}");
    Console.WriteLine("---------------");

    var data = await File.ReadAllLinesAsync(fileName);

    long totalCalibrationResult = 0;
    foreach (var line in data)
    {
        var input = line.Split(": ", StringSplitOptions.RemoveEmptyEntries);

        var target = long.Parse(input[0]);

        var numbers = input[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

        var isValid = false;

        void Evaluate(long index, long currentValue)
        {
            if (isValid)
            {
                //only one valid result is needed
                return;
            }

            if (index == numbers.Length)
            {
                if (currentValue == target)
                {
                    isValid = true;
                }

                return;
            }

            // Try addition
            Evaluate(index + 1, currentValue + numbers[index]);

            // Try multiplication
            Evaluate(index + 1, currentValue * numbers[index]);
        }

        Evaluate(1, numbers[0]);

        if (isValid)
        {
            totalCalibrationResult += target;
        }
    }

    Console.WriteLine($"The total calibration result is {totalCalibrationResult}");
    Console.WriteLine();
}

async Task RunPart2(string fileName)
{
    Console.WriteLine($"Running {fileName}");
    Console.WriteLine("---------------");

    var data = await File.ReadAllLinesAsync(fileName);

    long totalCalibrationResult = 0;
    foreach (var line in data)
    {
        var input = line.Split(": ", StringSplitOptions.RemoveEmptyEntries);

        var target = long.Parse(input[0]);

        var numbers = input[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

        var isValid = false;

        void Evaluate(long index, long currentValue)
        {
            if (isValid || currentValue > target)
            {
                //only one valid result is needed
                return;
            }

            if (index == numbers.Length)
            {
                if (currentValue == target)
                {
                    isValid = true;
                }

                return;
            }

            // Try addition
            Evaluate(index + 1, currentValue + numbers[index]);

            // Try multiplication
            Evaluate(index + 1, currentValue * numbers[index]);

            // Try concatenation
            Evaluate(index + 1, long.Parse(currentValue.ToString() + numbers[index].ToString()));
        }

        Evaluate(1, numbers[0]);

        if (isValid)
        {
            totalCalibrationResult += target;
        }
    }

    Console.WriteLine($"The total calibration result is {totalCalibrationResult}");
    Console.WriteLine();
}