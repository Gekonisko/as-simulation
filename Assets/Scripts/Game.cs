using Chapter.Singleton;
using UnityEngine;

public class Game : Singleton<Game>
{
    private readonly int MinBoardSize = 1;

    [SerializeField]
    Vector2Int boardSize = new Vector2Int(11, 11);
    public Vector2Int BoardSize => boardSize;


    void OnValidate()
    {
        if (boardSize.x < MinBoardSize)
        {
            boardSize.x = MinBoardSize;
        }
        if (boardSize.y < MinBoardSize)
        {
            boardSize.y = MinBoardSize;
        }
    }
}