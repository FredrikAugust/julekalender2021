using System.Linq;

namespace aoc;

public class Heatmap
{
    private List<List<int>> tiles;

    public Heatmap(string stringRepresentation)
    {
        tiles = stringRepresentation
            .Trim()
            .Split("\n")
            .Select(line => line.Trim().Select(c => c - '0').ToList()) // Convert from string char to int
            .ToList();
    }

    public Tuple<int, int> GetBounds()
    {
        return new Tuple<int, int>(tiles[0].Count - 1, tiles.Count - 1);
    }

    public int FloodFill(int x, int y)
    {
        Queue<Tuple<int, int>> queue = new();
        queue.Enqueue(new Tuple<int, int>(x, y));

        HashSet<Tuple<int, int>> basin = new();

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var tile = GetTile(current.Item1, current.Item2);

            if (tile is null or 9 || basin.Contains(current)) continue;
            
            basin.Add(current);

            GetNeighborTiles(current.Item1, current.Item2)
                .ForEach(queue.Enqueue);
        }

        return basin.Count;
    }

    public List<Tuple<int, int>> GetNeighborTiles(int x, int y)
    {
        return new List<Tuple<int, int>>
            {
                new(x - 1, y),
                new(x + 1, y),
                new(x, y - 1),
                new(x, y + 1),
            }
            .Where(pos => GetTile(pos.Item1, pos.Item2) != null)
            .ToList();
    }

    public List<int> GetNeighbors(int x, int y)
    {
        var list = new List<int?>
            {
                GetTile(x - 1, y),
                GetTile(x + 1, y),
                GetTile(x, y - 1),
                GetTile(x, y + 1),
            }
            .Where(tile => tile != null)
            .Select(tile => tile!.Value);

        return list.ToList();
    }

    public int? GetTile(int x, int y)
    {
        if (x < 0)
            return null;

        if (x >= tiles[0].Count)
            return null;

        if (y >= tiles.Count)
            return null;

        if (y < 0)
            return null;

        return tiles[y][x];
    }
}