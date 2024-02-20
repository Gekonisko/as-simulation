using System.Threading.Tasks;
using UnityEngine;

public class CameraAdjustment : MonoBehaviour
{
    private void Start()
    {
        LookAtBoard();
    }

    private void LookAtBoard()
    {
        var size = BoardManager.Instance.boardSize;
        transform.position = BoardManager.Instance.boardPos - (Vector3.forward * Mathf.Max(size.x, size.y));
    }
}