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
    Rigidbody _rig;

    [SerializeField]
    private AgentData _data;
    public AgentData data => _data;

    private Color _color;
    private Node _currentNode, _nextNode;

    void Start()
    {
        _capsuleCollider.enabled = true;
    }

    private void OnEnable()
    {
        _data = AgentManager.Instance.agentData;
        transform.name = _data.name;

        _currentNode = BoardManager.Instance.GetRandomNode();

        var possibleDirections = BoardManager.Instance.grid.GetNeighbours(_currentNode);
        _nextNode = possibleDirections.ElementAt(Random.Range(0, possibleDirections.Count));

        transform.position = _currentNode.worldPos;

        _color = RandomizeColor();
        _capsule.material.color = _color;
    }

    private void FixedUpdate()
    {
        UpdatePosition();

        if (Vector3.Distance(transform.position, _nextNode.worldPos) <= 0.1)
            ChooseNextNode();
    }

    private void OnMouseDown()
    {
        SelectionManager.Instance.Select(this);
    }

    private void UpdatePosition()
    {
        var direction = Vector3.Normalize(_nextNode.worldPos - transform.position);
        _rig.velocity = direction * _data.speed;
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
        _data.hp -= 1;
        Events.UpdateAgent.Invoke(_data);
        ShowHit();
        if (_data.hp <= 0) Deactivate();
    }

    private void Deactivate()
    {
        SelectionManager.Instance.Deselect(this);
        Events.AgentDeath?.Invoke();
        StopAllCoroutines();

        gameObject.SetActive(false);
    }

}
