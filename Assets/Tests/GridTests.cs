using NUnit.Framework;
using UnityEngine;

public class GridTests
{
    [Test]
    public void ShouldReturnCorrectGridPosition1()
    {
        var size = new Vector2Int(5, 5);
        Grid grid = new Grid(size, Vector3.zero);

        var gridPos = grid.GetWorldToGridPosition(Vector3.zero);

        Assert.AreEqual(gridPos.Value, new Vector2Int(2, 2));
    }

    [Test]
    public void ShouldReturnCorrectGridPosition2()
    {
        var size = new Vector2Int(5, 5);
        Grid grid = new Grid(size, new Vector3(1, 0, 0));

        var gridPos = grid.GetWorldToGridPosition(Vector3.zero);

        Assert.AreEqual(gridPos.Value, new Vector2Int(1, 2));
    }

    [Test]
    public void ShouldGetNeighbours()
    {
        var size = new Vector2Int(5, 5);
        Grid grid = new Grid(size, Vector3.zero);

        var neighbours = grid.GetNeighbours(new Node() { gridPos = new Vector2Int(0, 0) });

        Assert.AreEqual(neighbours.Count, 2);
    }

    [Test]
    public void ShouldGetNodes()
    {
        var size = new Vector2Int(5, 5);
        Grid grid = new Grid(size, Vector3.zero);

        var nodes = grid.GetNodes();
        Assert.AreEqual(nodes.Length, 25);
    }
}
