await Run("example.txt");
await Run("input.txt");

await RunPart2("example.txt");
await RunPart2("input.txt");

async Task Run(string fileName)
{
    Console.WriteLine($"Running {fileName}");
    Console.WriteLine("---------------");
    var data = await File.ReadAllLinesAsync(fileName);

    var rows = data.Length;
    var columns = data[0].Length;

    var antinodes = new bool[rows, columns];
    var antennas = new Dictionary<char, List<(int i, int j)>>();
    for (var i = 0; i < rows; i++)
    {
        for (var j = 0; j < columns; j++)
        {
            var current = data[i][j];
            if (char.IsAsciiLetterOrDigit(current))
            {
                if (!antennas.TryGetValue(current, out var foundAntennas))
                {
                    antennas[current] = [(i, j)];
                }
                else
                {
                    foundAntennas.Add((i, j));
                }
            }
        }
    }

    foreach (var foundAntenna in antennas)
    {
        var positions = foundAntenna.Value;
        for (var i = 0; i < positions.Count - 1; i++)
        {
            for (var j = i + 1; j < positions.Count; j++)
            {
                var distanceI = positions[j].i - positions[i].i;
                var distanceJ = positions[j].j - positions[i].j;

                var firstAntinode = (i: positions[i].i - distanceI, j: positions[i].j - distanceJ);
                var secondAntinode = (i: positions[j].i + distanceI, j: positions[j].j + distanceJ);
                if (firstAntinode.i >= 0 && firstAntinode.i < rows && firstAntinode.j >= 0 && firstAntinode.j < columns)
                {
                    antinodes[firstAntinode.i, firstAntinode.j] = true;
                }

                if (secondAntinode.i >= 0 && secondAntinode.i < rows && secondAntinode.j >= 0 && secondAntinode.j < columns)
                {
                    antinodes[secondAntinode.i, secondAntinode.j] = true;
                }
            }
        }
    }

    var antinodecount = 0;
    for (var i = 0; i < rows; i++)
    {
        for (var j = 0; j < columns; j++)
        {
            if (antinodes[i, j])
            {
                antinodecount++;
            }
        }
    }

    Console.WriteLine($"Antinode count: {antinodecount}");
}

async Task RunPart2(string fileName)
{
    Console.WriteLine($"Running part 2 {fileName}");
    Console.WriteLine("---------------");
    var data = await File.ReadAllLinesAsync(fileName);

    var rows = data.Length;
    var columns = data[0].Length;

    var antinodes = new bool[rows, columns];
    var antennas = new Dictionary<char, List<(int i, int j)>>();
    for (var i = 0; i < rows; i++)
    {
        for (var j = 0; j < columns; j++)
        {
            var current = data[i][j];
            if (char.IsAsciiLetterOrDigit(current))
            {
                if (!antennas.TryGetValue(current, out var foundAntennas))
                {
                    antennas[current] = [(i, j)];
                }
                else
                {
                    foundAntennas.Add((i, j));
                }
            }
        }
    }

    foreach (var foundAntenna in antennas)
    {
        var positions = foundAntenna.Value;
        for (var i = 0; i < positions.Count - 1; i++)
        {
            for (var j = i + 1; j < positions.Count; j++)
            {
                var distanceI = positions[j].i - positions[i].i;
                var distanceJ = positions[j].j - positions[i].j;

                var currentI = positions[i].i;
                var currentJ = positions[i].j;
                do
                {
                    antinodes[currentI, currentJ] = true;
                    currentI += distanceI;
                    currentJ += distanceJ;
                }
                while (currentI >= 0 && currentI < rows && currentJ >= 0 && currentJ < columns);

                currentI = positions[i].i;
                currentJ = positions[i].j;
                do
                {
                    antinodes[currentI, currentJ] = true;
                    currentI -= distanceI;
                    currentJ -= distanceJ;
                }
                while (currentI >= 0 && currentI < rows && currentJ >= 0 && currentJ < columns);
            }
        }
    }

    var antinodecount = 0;
    for (var i = 0; i < rows; i++)
    {
        for (var j = 0; j < columns; j++)
        {
            if (antinodes[i, j])
            {
                antinodecount++;
            }
        }
    }

    Console.WriteLine($"Antinode count: {antinodecount}");
}
