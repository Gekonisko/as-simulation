using Chapter.Singleton;
using UnityEngine;

public class Game : Singleton<Game>
{
    public readonly Vector3 boardPos = Vector3.zero;
    public readonly int minBoardSize = 1;

    private Grid _grid;
    public Grid grid => _grid;

    [SerializeField]
    Vector2Int _boardSize = new Vector2Int(11, 11);
    public Vector2Int boardSize => _boardSize;

    private void Awake()
    {
        base.Awake();
        _grid = new Grid(boardSize, boardPos);
    }


    void OnValidate()
    {
        if (_boardSize.x < minBoardSize)
        {
            _boardSize.x = minBoardSize;
        }
        if (_boardSize.y < minBoardSize)
        {
            _boardSize.y = minBoardSize;
        }
    }
}