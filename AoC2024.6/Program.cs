
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

    var rows = data.Length;
    var columns = data[0].Length;

    var i = 0;
    var j = 0;
    var visited = new bool[rows, columns];

    for (i = 0; i < rows; i++)
    {
        j = data[i].IndexOf('^');
        if (j >= 0)
        {
            break;
        }
    }

    var direction = 0; // 0 = up, 1 = right, 2 = down, 3 = left
    while (i >= 0 && i < rows && j >= 0 && j < columns)
    {
        visited[i, j] = true;
        switch (direction)
        {
            case 0:
                if (i > 0 && data[i - 1][j] == '#')
                {
                    direction = (direction + 1) % 4;
                    break;
                }
                else
                {
                    i--;
                    break;
                }
            case 1:
                if (j < columns - 1 && data[i][j + 1] == '#')
                {
                    direction = (direction + 1) % 4;
                    break;
                }
                else
                {
                    j++;
                    break;
                }
            case 2:
                if (i < rows - 1 && data[i + 1][j] == '#')
                {
                    direction = (direction + 1) % 4;
                    break;
                }
                else
                {
                    i++;
                    break;
                }
            case 3:
                if (j > 0 && data[i][j - 1] == '#')
                {
                    direction = (direction + 1) % 4;
                    break;
                }
                else
                {
                    j--;
                    break;
                }
        }
    }

    //count visited fields
    var visitedFields = 0;
    for (i = 0; i < rows; i++)
    {
        for (j = 0; j < columns; j++)
        {
            if (visited[i, j])
            {
                visitedFields++;
            }
        }
    }

    Console.WriteLine($"The guard visited {visitedFields} fields");
    Console.WriteLine();
}

async Task RunPart2(string fileName)
{
    Console.WriteLine($"Running part 2 {fileName}");
    Console.WriteLine("---------------");

    var data = (await File.ReadAllLinesAsync(fileName)).Select(l => l.ToArray()).ToArray();

    var rows = data.Length;
    var columns = data[0].Length;

    var startingRow = 0;
    var startingColumn = 0;
    for (startingRow = 0; startingRow < rows; startingRow++)
    {
        startingColumn = Array.IndexOf(data[startingRow], '^');
        if (startingColumn >= 0)
        {
            break;
        }
    }

    var (visited, _) = VisitFields(data, rows, columns, startingRow, startingColumn);

    var loopCount = 0;
    for (var i = 0; i < rows; i++)
    {
        for (var j = 0; j < columns; j++)
        {
            if (i == startingRow && j == startingColumn)
            {
                // we are at the starting point, skip
                continue;
            }

            if (visited[i, j] != Direction.None)
            {
                // we have a visited field, try an obstacle
                //clone the data

                data[i][j] = '#';
                var (_, success) = VisitFields(data, rows, columns, startingRow, startingColumn);
                data[i][j] = '.';
                if (!success)
                {
                    loopCount++;
                }
            }
        }
    }

    Console.WriteLine($"The guard will hit a loop with {loopCount} combinations");
}

static (Direction[,], bool) VisitFields(char[][] data, int rows, int columns, int startingRow, int startingColumn)
{
    var i = startingRow;
    var j = startingColumn;
    var direction = Direction.Up;
    var visited = new Direction[rows, columns];
    var success = true;
    while (i >= 0 && i < rows && j >= 0 && j < columns)
    {
        if (visited[i, j] == Direction.None)
        {
            visited[i, j] = direction;
        }
        else if (visited[i, j].HasFlag(direction))
        {
            //we have been here before
            success = false;
            break;
        }
        else
        {
            visited[i, j] |= direction;
        }

        switch (direction)
        {
            case Direction.Up:
                if (i > 0 && data[i - 1][j] == '#')
                {
                    direction = Direction.Right;
                    break;

                }
                else
                {
                    i--;
                    break;
                }
            case Direction.Right:
                if (j < columns - 1 && data[i][j + 1] == '#')
                {
                    direction = Direction.Down;
                    break;
                }
                else
                {
                    j++;
                    break;
                }
            case Direction.Down:
                if (i < rows - 1 && data[i + 1][j] == '#')
                {
                    direction = Direction.Left;
                    break;
                }
                else
                {
                    i++;
                    break;
                }
            case Direction.Left:
                if (j > 0 && data[i][j - 1] == '#')
                {
                    direction = Direction.Up;
                    break;
                }
                else
                {
                    j--;
                    break;
                }
        }
    }

    return (visited, success);
}

[Flags]
internal enum Direction
{
    None = 0,
    Up = 1,
    Right = 2,
    Down = 4,
    Left = 8
}