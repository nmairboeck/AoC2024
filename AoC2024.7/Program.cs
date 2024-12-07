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

        void Evaluate(int index, long currentValue)
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

        void Evaluate(int index, long currentValue)
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
            Evaluate(index + 1, concat(currentValue, numbers[index]));
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

//stolen from https://stackoverflow.com/a/26853517
static long concat(long a, long b)
{
    const uint c0 = 10, c1 = 100, c2 = 1000, c3 = 10000, c4 = 100000,
        c5 = 1000000, c6 = 10000000, c7 = 100000000, c8 = 1000000000;
    a *= b < c0 ? c0 : b < c1 ? c1 : b < c2 ? c2 : b < c3 ? c3 :
         b < c4 ? c4 : b < c5 ? c5 : b < c6 ? c6 : b < c7 ? c7 : c8;
    return a + b;
}