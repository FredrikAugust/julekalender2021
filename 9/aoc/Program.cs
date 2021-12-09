// See https://aka.ms/new-console-template for more information

using aoc;

var text = File.ReadAllText("/Users/fredrik-alv/Code/github.com/fredrikaugust/julekalender2021/9/aoc/input.txt");

var heatmap = new Heatmap(text);

var count = 0;
var bounds = heatmap.GetBounds();

// List<List<int>> basins = new();

for (int y = 0; y <= bounds.Item2; y++)
{
    for (int x = 0; x <= bounds.Item1; x++)
    {
        var tile = heatmap.GetTile(x, y)!.Value;

        if (tile == 9) Console.Write("x");

        var neighbors = heatmap.GetNeighbors(x, y);

        if (neighbors.All(neighbor => neighbor > tile))
        {
            Console.Write("o");
            count += tile + 1;
        }
        else
        {
            Console.Write(" ");
        }
    }

    Console.WriteLine();
}

Console.WriteLine(count);