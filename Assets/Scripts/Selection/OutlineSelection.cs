using UnityEngine;

public class OutlineSelection : MonoBehaviour, ISelectionResponse
{

    public void OnSelect(Transform transform)
    {
        var outline = transform.GetComponent<Outline>();
        if (outline != null) outline.enabled = true;
    }

    public void OnDeselect(Transform transform)
    {
        var outline = transform.GetComponent<Outline>();
        if (outline != null) outline.enabled = false;
    }
}