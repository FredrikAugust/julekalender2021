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

    public List<int> GetNeighbors(int x, int y)
    {
        var list = new List<int?>()
        {
            GetTile(x - 1, y),
            GetTile(x + 1, y),
            GetTile(x, y - 1),
            GetTile(x, y + 1),
        }
            .Where(tile => tile != null)
            .Select(x => x!.Value);

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