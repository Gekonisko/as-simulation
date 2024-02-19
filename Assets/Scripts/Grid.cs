
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public readonly Node[,] nodes;
    private Vector2Int _size;
    private Vector2Int[] neighboursOffset = new Vector2Int[]
    { new(-1, 0), new(1, 0), new(0, -1), new(0, 1) };

    public Grid(Vector2Int size, Vector3 position)
    {
        nodes = new Node[size.x, size.y];
        _size = size;

        Vector2 offset = new Vector2(((size.x - 1) * 0.5f) - position.x, ((size.y - 1) * 0.5f) - position.y);

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                var pos = new Vector3(x - offset.x, y - offset.y, 0f);
                var node = new Node()
                {
                    gridPos = new Vector2Int(x, y),
                    globalPos = pos
                };

                nodes[x, y] = node;
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> result = new();
        foreach (Vector2Int offset in neighboursOffset)
        {
            var pos = new Vector2Int(node.gridPos.x + offset.x, node.gridPos.y + offset.y);

            if ((pos.x >= 0 && pos.x < _size.x) && (pos.y >= 0 && pos.y < _size.y))
                result.Add(nodes[pos.x, pos.y]);
        }
        return result;
    }

    public Vector3[] GetNodePositions()
    {
        Vector3[] positions = new Vector3[_size.x * _size.y];

        for (int y = 0; y < _size.y; y++)
        {
            for (int x = 0; x < _size.x; x++)
            {
                positions[x + (y * _size.y)] = nodes[x, y].globalPos;
            }
        }

        return positions;
    }


}