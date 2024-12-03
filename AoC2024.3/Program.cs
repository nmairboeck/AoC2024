
using System.Text.RegularExpressions;

Console.WriteLine("AoC 2024 Day 3 Part 1");

var lines = await System.IO.File.ReadAllLinesAsync("Input.txt");

var regex = new Regex(@"mul\([0-9]{1,3},[0-9]{1,3}\)");

var sum = 0;
foreach (var line in lines)
{
    var matches = regex.Matches(line);

    foreach (var match in matches)
    {
        var str = match.ToString()!;
        var parts = str.Split(',', StringSplitOptions.RemoveEmptyEntries);

        sum += int.Parse(parts[0][4..]) * int.Parse(parts[1][..^1]);
    }
}

Console.WriteLine($"The sum of all multiplications is {sum}");

lines = await System.IO.File.ReadAllLinesAsync("Input.txt");
var regexPart2 = new Regex(@"(mul\([0-9]{1,3},[0-9]{1,3}\))|(don\'t\(\))|(do\(\))");

var sumPart2 = 0;
var isEnabled = true;
foreach (var line in lines)
{
    var matches = regexPart2.Matches(line);

    foreach (var match in matches)
    {
        var str = match.ToString()!;
        if (str == "do()")
        {
            isEnabled = true;
        }
        else if (str == "don't()")
        {
            isEnabled = false;
        }
        else
        {
            if (isEnabled)
            {
                var parts = str.Split(',', StringSplitOptions.RemoveEmptyEntries);

                sumPart2 += int.Parse(parts[0][4..]) * int.Parse(parts[1][..^1]);
            }
        }
    }
}

Console.WriteLine($"The sum of all multiplications with enabling is {sumPart2}");
