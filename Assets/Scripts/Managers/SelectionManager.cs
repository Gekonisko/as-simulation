using Chapter.Singleton;
using UnityEngine;

public class SelectionManager : Singleton<SelectionManager>
{
    private ISelectionResponse _selectionResponse;
    private Transform _selectedObject;

    private void Awake()
    {
        base.Awake();
        _selectionResponse = GetComponent<ISelectionResponse>();
    }

    public void Select(Agent agent)
    {
        if (_selectedObject != null)
            _selectionResponse.OnDeselect(_selectedObject);

        Events.SelectAgent.Invoke(agent.data);

        _selectionResponse.OnSelect(agent.transform);
        _selectedObject = agent.transform;

    }



}