using UnityEngine;

public struct Node
{
    public Vector2Int gridPos;
    public Vector3 worldPos;

    public override string ToString()
    {
        return $"g:{gridPos} w:{worldPos}";
    }
}