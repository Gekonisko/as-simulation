using UnityEngine;

public class GameBoard : MonoBehaviour
{

	[SerializeField]
	Transform ground = default;

	[SerializeField]
	GameTile tilePrefab = default;

	Vector2Int size;

	private void Start()
	{
		size = Game.Instance.boardSize;
		ground.position = Game.Instance.boardPos;

		ground.localScale = new Vector3(size.x, size.y, 1f);

		foreach (Vector3 pos in Game.Instance.grid.GetNodePositions())
		{
			GameTile tile = Instantiate(tilePrefab);
			tile.transform.SetParent(transform, false);
			tile.transform.localPosition = pos;
		}
	}
}