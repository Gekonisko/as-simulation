using UnityEngine;

public class Board : MonoBehaviour
{

	[SerializeField]
	Transform ground = default;

	[SerializeField]
	GameObject tilePrefab = default;

	Vector2Int size;

	private void Start()
	{
		size = BoardManager.Instance.boardSize;
		ground.position = BoardManager.Instance.boardPos;

		ground.localScale = new Vector3(size.x, size.y, 1f);

		foreach (Vector3 pos in BoardManager.Instance.grid.GetNodesPosition())
		{
			GameObject tile = Instantiate(tilePrefab);
			tile.transform.SetParent(transform, false);
			tile.transform.localPosition = pos;
		}
	}
}