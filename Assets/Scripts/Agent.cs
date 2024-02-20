using System.Collections;
using System.Linq;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField]
    MeshRenderer _capsule;
    [SerializeField]
    CapsuleCollider _capsuleCollider;

    [SerializeField]
    private AgentData _data;

    private Color _color;
    private Node _currentNode, _nextNode;

    void Start()
    {
        _data = AgentManager.Instance.agentData;
        _color = RandomizeColor();

        _currentNode = BoardManager.Instance.GetRandomNode();

        var possibleDirections = BoardManager.Instance.grid.GetNeighbours(_currentNode);
        _nextNode = possibleDirections.ElementAt(Random.Range(0, possibleDirections.Count));

        transform.position = _currentNode.worldPos;
        _capsuleCollider.enabled = true;

        _capsule.material.color = _color;
    }

    void Update()
    {
        UpdatePosition();

        if (transform.position == _nextNode.worldPos)
            ChooseNextNode();
    }

    private void UpdatePosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, _nextNode.worldPos, _data.speed * Time.deltaTime);
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

    private IEnumerator ShowHitAnimation()
    {
        _capsule.material.color = Color.red;

        yield return new WaitForSeconds(_data.showHitTime);

        _capsule.material.color = _color;

        yield return null;
    }

    private void ShowHit()
    {
        StopAllCoroutines();
        _capsule.material.color = _color;
        StartCoroutine(ShowHitAnimation());
    }

    private void OnTriggerEnter(Collider other)
    {
        _data.life -= 1;
        ShowHit();
        if (_data.life <= 0) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Events.AgentDeath?.Invoke();
        StopAllCoroutines();
    }


}
