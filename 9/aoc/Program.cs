// See https://aka.ms/new-console-template for more information

using aoc;

var text = File.ReadAllText("/Users/fredrik-alv/Code/github.com/fredrikaugust/julekalender2021/9/aoc/input.txt");

var heatmap = new Heatmap(text);

var bounds = heatmap.GetBounds();

List<Tuple<int, int>> lowPoints = new();

List<int> basins = new();

for (var y = 0; y <= bounds.Item2; y++)
{
    for (var x = 0; x <= bounds.Item1; x++)
    {
        var tile = heatmap.GetTile(x, y)!.Value;

        if (tile == 9) Console.Write("1");
        else Console.Write("0");

        var neighbors = heatmap.GetNeighbors(x, y);

        if (neighbors.All(neighbor => neighbor > tile))
        {
            lowPoints.Add(new Tuple<int, int>(x, y));
        }
    }

    Console.WriteLine();
}

foreach (var lowPoint in lowPoints)
{
    basins.Add(heatmap.FloodFill(lowPoint.Item1, lowPoint.Item2));
}
