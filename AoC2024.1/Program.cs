
Console.WriteLine("AoC 2024 Day 1 Part 1");

var lines = await System.IO.File.ReadAllLinesAsync("Input.txt");

var numbers = lines.Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());

var left = numbers.Select(n => n[0]).ToList().Order();
var right = numbers.Select(n => n[1]).ToList().Order();

var distances = left.Zip(right, (l, r) => Math.Abs(r - l));

var distance = distances.Sum();

Console.WriteLine($"The total distance is {distance}");

var occurences = right.GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count());

var similarities = left.Sum(l => (occurences.TryGetValue(l, out var count) ? count : 0) * l);

Console.WriteLine($"The similarity is {similarities}");
