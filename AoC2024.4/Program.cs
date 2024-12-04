var data = await File.ReadAllLinesAsync("Input.txt");

var rows = data.Length;
var cols = data[0].Length;
var word = "XMAS";
var wordLength = word.Length;

var count = 0;
for (var i = 0; i < rows; i++)
{
    for (var j = 0; j < cols; j++)
    {
        if (
            // Check horizontally to the right
            (j + wordLength <= cols && data[i][j..(j + wordLength)] == word) ||
            // Check horizontally to the left
            (j - wordLength >= -1 && data[i][(j - wordLength + 1)..(j + 1)].Reverse().SequenceEqual(word)) ||
            // Check vertically downwards
            (i + wordLength <= rows && Enumerable.Range(0, wordLength).All(k => data[i + k][j] == word[k])) ||
            // Check vertically upwards
            (i - wordLength >= -1 && Enumerable.Range(0, wordLength).All(k => data[i - k][j] == word[k])) ||
            // Check diagonally down-right
            (i + wordLength <= rows && j + wordLength <= cols && Enumerable.Range(0, wordLength).All(k => data[i + k][j + k] == word[k])) ||
            // Check diagonally down-left
            (i + wordLength <= rows && j - wordLength >= -1 && Enumerable.Range(0, wordLength).All(k => data[i + k][j - k] == word[k])) ||
            // Check diagonally up-right
            (i - wordLength >= -1 && j + wordLength <= cols && Enumerable.Range(0, wordLength).All(k => data[i - k][j + k] == word[k])) ||
            // Check diagonally up-left
            (i - wordLength >= -1 && j - wordLength >= -1 && Enumerable.Range(0, wordLength).All(k => data[i - k][j - k] == word[k]))
        )
        {
            count++;
        }
    }
}

Console.WriteLine($"Found {count} occurrences of XMAS");

var count2 = 0;

for (var i = 1; i < data.Length - 1; i++)
{
    for (var j = 1; j < data[i].Length - 1; j++)
    {
        if (
            data[i][j] == 'A' &&
            // Check diagonally up-left and down-right
            ((data[i - 1][j - 1] == 'M' && data[i + 1][j + 1] == 'S') || (data[i + 1][j + 1] == 'M' && data[i - 1][j - 1] == 'S')) &&
            // Check diagonally up-right and down-left
            ((data[i - 1][j + 1] == 'M' && data[i + 1][j - 1] == 'S') || (data[i + 1][j - 1] == 'M' && data[i - 1][j + 1] == 'S'))
        )
        {
            count2++;
        }
    }
}

Console.WriteLine($"The amount of x-mas is {count2}");
