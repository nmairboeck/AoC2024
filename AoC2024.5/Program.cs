
var lines = File.ReadAllLines("Input.txt");

var rules = new List<(int before, int after)>();

var i = 0;
for (i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    if (line?.Length == 0)
    {
        // end of rules
        i++; // skip empty line
        break;
    }

    var parts = line.Split("|");
    var before = int.Parse(parts[0]);
    var after = int.Parse(parts[1]);
    rules.Add((before, after));
}

var middlePartsCorrect = 0;
var middlePartsIncorrect = 0;
for (; i < lines.Length; i++)
{
    var numbers = lines[i].Split(",").Select(int.Parse).ToArray();
    var relevantRules = rules.Where(r => Array.IndexOf(numbers, r.before) >= 0 && Array.IndexOf(numbers, r.after) >= 0).ToList();
    var orderCorrect = relevantRules
        .All(r => Array.IndexOf(numbers, r.before) < Array.IndexOf(numbers, r.after));
    if (orderCorrect)
    {
        middlePartsCorrect += numbers[numbers.Length / 2];
    }
    else
    {
        //sort the numbers by rule
        var sortedNumbers = numbers.ToList();
        sortedNumbers.Sort((a, b) =>
        {
            if (relevantRules.Any(r => r.before == a && r.after == b))
            {
                return -1;
            }

            if (relevantRules.Any(r => r.before == b && r.after == a))
            {
                return 1;
            }

            return 0;
        });

        middlePartsIncorrect += sortedNumbers[sortedNumbers.Count / 2];
    }
}

Console.WriteLine($"The sum of the middle numbers of correct rows is {middlePartsCorrect}");
Console.WriteLine($"The sum of the middle numbers of incorrect rows is {middlePartsIncorrect}");
