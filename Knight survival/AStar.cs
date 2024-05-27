using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

public class AStarPathfinding
{
    public static List<Vector2> FindPath(Node startNode, Node endNode, Grid grid)
    {
        var openSet = new List<Node>();
        var closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            var currentNode = openSet.OrderBy(node => node.F).First();

            if (currentNode == endNode)
                return RetracePath(startNode, endNode);

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            foreach (var neighbor in GetNeighbors(currentNode, grid))
            {
                if (closedSet.Contains(neighbor) || !neighbor.IsWalkable)
                    continue;

                var newMovementCostToNeighbor = currentNode.G + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.G || !openSet.Contains(neighbor))
                {
                    neighbor.G = newMovementCostToNeighbor;
                    neighbor.H = GetDistance(neighbor, endNode);
                    neighbor.Parent = currentNode;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return new List<Vector2>(); // Return empty path if no path is found
    }

    private static List<Node> GetNeighbors(Node node, Grid grid)
    {
        var neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.X + x;
                int checkY = node.Y + y;

                if (checkX >= 0 && checkX < grid.Width && checkY >= 0 && checkY < grid.Height)
                {
                    neighbors.Add(grid.Nodes[checkX, checkY]);
                }
            }
        }

        return neighbors;
    }

    private static List<Vector2> RetracePath(Node startNode, Node endNode)
    {
        var path = new List<Vector2>();
        var currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(new Vector2(currentNode.X, currentNode.Y));
            currentNode = currentNode.Parent;
        }
        path.Reverse();

        return path;
    }

    private static int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Math.Abs(nodeA.X - nodeB.X);
        int dstY = Math.Abs(nodeA.Y - nodeB.Y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}