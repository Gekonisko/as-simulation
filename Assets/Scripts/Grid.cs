
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid
{
    public readonly Vector2 nodeSize = Vector2.one;
    private readonly Node[,] _nodes;

    private Vector2Int _size;
    private Vector2 _offset;
    private Vector2Int[] neighboursOffset = new Vector2Int[]
    { new(-1, 0), new(1, 0), new(0, -1), new(0, 1) };

    public Grid(Vector2Int size, Vector3 position)
    {
        _nodes = new Node[size.x, size.y];
        _size = size;

        _offset = new Vector2(((size.x - 1) * 0.5f) - position.x, ((size.y - 1) * 0.5f) - position.y);

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                var pos = new Vector3(x - _offset.x, y - _offset.y, 0f);
                var node = new Node()
                {
                    gridPos = new Vector2Int(x, y),
                    worldPos = pos
                };

                _nodes[x, y] = node;
            }
        }
    }

    public Node? GetNode(Vector2Int pos) => IsPositionExists(pos) ? _nodes[pos.x, pos.y] : null;

    public Vector2Int? GetWorldToGridPosition(Vector3 pos)
    {
        int x = (int)MathF.Round((pos.x + _offset.x) / nodeSize.x);
        int y = (int)MathF.Round((pos.y + _offset.y) / nodeSize.y);
        var gridPos = new Vector2Int(x, y);

        return GetNode(gridPos)?.gridPos;
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> result = new();
        foreach (Vector2Int offset in neighboursOffset)
        {
            var pos = new Vector2Int(node.gridPos.x + offset.x, node.gridPos.y + offset.y);

            if (IsPositionExists(pos))
                result.Add(_nodes[pos.x, pos.y]);
        }
        return result;
    }

    public Node[] GetNodes()
    {
        var xLimit = Enumerable.Range(0, _nodes.GetUpperBound(0) + 1);
        var yLimit = Enumerable.Range(0, _nodes.GetUpperBound(1) + 1);
        return xLimit.SelectMany(x => yLimit.Select(y => _nodes[x, y])).ToArray();
    }

    public Vector3[] GetNodesPosition()
    {
        Vector3[] positions = new Vector3[_size.x * _size.y];

        for (int y = 0; y < _size.y; y++)
        {
            for (int x = 0; x < _size.x; x++)
            {
                positions[x + (y * _size.y)] = _nodes[x, y].worldPos;
            }
        }

        return positions;
    }

    private bool IsPositionExists(Vector2Int pos)
    {
        if ((pos.x >= 0 && pos.x < _size.x) && (pos.y >= 0 && pos.y < _size.y))
            return true;
        return false;
    }


}