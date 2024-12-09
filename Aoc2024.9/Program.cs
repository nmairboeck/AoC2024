
using System.Collections;

await Run("example.txt");
await Run("input.txt");

await RunPart2("example.txt");
await RunPart2("input.txt");

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

async Task RunPart2(string fileName)
{
    Console.WriteLine($"Running {fileName}");
    Console.WriteLine("---------------");
    var line = await File.ReadAllTextAsync(fileName);

    var compressed = GetNumbersPart2(line);
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

IEnumerable<int> GetNumbersPart2(string line)
{
    var fileIdFront = 0;

    var numbers = line.Select(c => c - 48).ToArray();

    var numberProvider = new NumberProviderPart2(ref numbers);

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
            do
            {
                var (number, amount) = numberProvider.GetNextBlock();
                if (numbers[i] >= amount)
                {
                    for (var j = 0; j < amount; j++)
                    {
                        yield return number;
                        numbers[i]--;
                    }
                }
                else
                {
                    while (numbers[i] > 0)
                    {
                        yield return 0;
                        numbers[i]--;
                    }
                }
            } while (numbers[i] > 0);
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

internal class NumberProviderPart2
{
    private readonly int[] _input;
    private int _currentFileId;
    private int _i;

    public NumberProviderPart2(ref int[] input)
    {
        _input = input;
        _currentFileId = (input.Length / 2);
        _i = _input.Length - 1;
    }

    public (int number, int amount) GetNextBlock()
    {
        var i = _i;
        _i -= 2;
        return (_currentFileId--, _input[i]);
    }
}
