
using System.Collections;

await Run("example.txt");
await Run("input.txt");

async Task Run(string fileName)
{
    Console.WriteLine($"Running {fileName}");
    Console.WriteLine("---------------");
    var line = await File.ReadAllTextAsync(fileName);

    var compressed = GetNumbers(line);
    long sum = 0;
    sum = compressed.Select((number, index) => (long)number * index).Sum();

    Console.WriteLine($"The result is {sum}.");
}

IEnumerable<int> GetNumbers(string line)
{
    var fileIdFront = 0;

    var numbers = line.Select(c => c - 48).ToArray();

    var numberProvider = new NumberProvider(ref numbers).GetEnumerator();

    for (var i = 0; i < line.Length; i++)
    {
        if (i % 2 == 0)
        {
            while (numbers[i]-- > 0)
            {
                yield return fileIdFront;
            }

            fileIdFront++;
        }
        else
        {
            for (var j = 0; j < numbers[i] && numberProvider.MoveNext(); j++)
            {
                yield return numberProvider.Current;
            }
        }
    }
}

internal class NumberProvider : IEnumerable<int>
{
    private readonly int[] _input;
    private int _currentFileId;

    public NumberProvider(ref int[] input)
    {
        _input = input;
        _currentFileId = (input.Length / 2);
    }

    public IEnumerator<int> GetEnumerator()
    {
        for (var i = _input.Length - 1; i > 0; i -= 2)
        {
            while (_input[i]-- > 0)
            {
                yield return _currentFileId;
            }

            _currentFileId--;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
