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
		size = Game.Instance.BoardSize;

		ground.localScale = new Vector3(size.x, size.y, 1f);

		Vector2 offset = new Vector2((size.x - 1) * 0.5f, (size.y - 1) * 0.5f);

		for (int y = 0; y < size.y; y++)
		{
			for (int x = 0; x < size.x; x++)
			{
				GameTile tile = Instantiate(tilePrefab);
				tile.transform.SetParent(transform, false);
				tile.transform.localPosition = new Vector3(
					x - offset.x, y - offset.y, 0f
				);
			}
		}
	}
}