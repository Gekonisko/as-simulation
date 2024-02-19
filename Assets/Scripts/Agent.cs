using System.Linq;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField]
    MeshRenderer _capsule;

    private Node _currentNode, _nextNode;
    private float _speed;

    void Start()
    {
        _speed = AgentManager.Instance.agentSpeed;

        _currentNode = BoardManager.Instance.GetRandomNode();

        var possibleDirections = BoardManager.Instance.grid.GetNeighbours(_currentNode);
        _nextNode = possibleDirections.ElementAt(Random.Range(0, possibleDirections.Count));

        transform.position = _currentNode.worldPos;

        _capsule.material.color = RandomizeColor();
    }

    void Update()
    {
        UpdatePosition();

        if (transform.position == _nextNode.worldPos)
            ChooseNextNode();
    }

    private void UpdatePosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, _nextNode.worldPos, _speed * Time.deltaTime);
    }

    private void ChooseNextNode()
    {
        _currentNode = _nextNode;

        var possibleDirections = BoardManager.Instance.grid.GetNeighbours(_currentNode);
        _nextNode = possibleDirections.ElementAt(Random.Range(0, possibleDirections.Count));

    }

    private Color RandomizeColor()
    {
        float r = Random.Range(0, 256) / 256.0f;
        float g = Random.Range(0, 256) / 256.0f;
        float b = Random.Range(0, 256) / 256.0f;

        return new Color(r, g, b);
    }
}
