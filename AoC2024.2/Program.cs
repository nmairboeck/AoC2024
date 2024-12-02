Console.WriteLine("AoC 2024 Day 2");
static bool CheckLine(int[] numbers)
{
    var bigger = numbers[1] > numbers[0];
    var isValid = Math.Abs(numbers[0] - numbers[1]) is <= 3 and not 0;
    for (var i = 1; i < numbers.Length - 1 && isValid; i++)
    {
        if (Math.Abs(numbers[i + 1] - numbers[i]) is > 3 or 0
            || (bigger && (numbers[i] > numbers[i + 1]))
            || (!bigger && (numbers[i] < numbers[i + 1])))
        {
            isValid = false;
        }
    }

    return isValid;
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
        var errors = Enumerable.Range(0, numbers.Length).Select(i => numbers.Where((_, index) => index != i).ToArray()).Any(CheckLine);
        if (errors)
        {
            countWithDampener++;
        }
    }
}

Console.WriteLine($"The number of valid sequences is {count}");

Console.WriteLine($"The number of valid sequences with error dampener is {countWithDampener}");
