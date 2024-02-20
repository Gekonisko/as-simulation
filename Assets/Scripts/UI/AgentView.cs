using TMPro;
using UnityEngine;

public class AgentView : MonoBehaviour
{
    [SerializeField]
    private AgentData _selectedAgent;

    [SerializeField]
    private TextMeshProUGUI _name;

    [SerializeField]
    private TextMeshProUGUI _hp;

    private void Awake()
    {
        Events.UpdateAgent += OnAgentGetDamage;
        Events.SelectAgent += OnSelectAgent;
    }

    private void UpdateData()
    {
        _name.text = $"Name: {_selectedAgent.name}";
        _hp.text = $"HP: {_selectedAgent.hp}";
    }

    private void OnAgentGetDamage(AgentData data)
    {
        if (_selectedAgent.name == data.name)
        {
            _selectedAgent = data;
            UpdateData();
        }
    }

    private void OnSelectAgent(AgentData data)
    {
        _selectedAgent = data;
        UpdateData();
    }


    private void OnDestroy()
    {
        Events.UpdateAgent -= OnAgentGetDamage;
        Events.UpdateAgent -= OnSelectAgent;
    }
}
