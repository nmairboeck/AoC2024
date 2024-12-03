Console.WriteLine("AoC 2024 Day 2");
static bool CheckLine(int[] numbers)
{
    var sum = 0;
    for (var i = 0; i < numbers.Length - 1; i++)
    {
        sum += Math.Abs(numbers[i] - numbers[i + 1]) is <= 3 ? numbers[i].CompareTo(numbers[i + 1]) : 0;
    }

    return Math.Abs(sum) == numbers.Length - 1;
}

var lines = (await System.IO.File.ReadAllLinesAsync("Input.txt"))
    .Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());

var count = 0;
var countWithDampener = 0;

foreach (var numbers in lines)
{
    var isValid = CheckLine(numbers);

    if (isValid)
    {
        count++;
        countWithDampener++;
    }
    else
    {
        var isValidWithDampener = Enumerable.Range(0, numbers.Length).Select(i => numbers.Where((_, index) => index != i).ToArray()).Any(CheckLine);
        if (isValidWithDampener)
        {
            countWithDampener++;
        }
    }
}

Console.WriteLine($"The number of valid sequences is {count}");

Console.WriteLine($"The number of valid sequences with error dampener is {countWithDampener}");
