public class Grid
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Node[,] Nodes { get; private set; }

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
        Nodes = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Nodes[x, y] = new Node(x, y, true); // Assuming all nodes are walkable initially
            }
        }
    }

    public void SetWalkable(int x, int y, bool isWalkable)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            Nodes[x, y].IsWalkable = isWalkable;
        }
    }
}

public class Node
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public Node Parent { get; set; }
    public float G { get; set; } // Cost from start to this node
    public float H { get; set; } // Estimated cost from this node to end
    public float F { get { return G + H; } } // Total cost
    public bool IsWalkable { get; set; }

    public Node(int x, int y, bool isWalkable)
    {
        X = x;
        Y = y;
        IsWalkable = isWalkable;
    }
}