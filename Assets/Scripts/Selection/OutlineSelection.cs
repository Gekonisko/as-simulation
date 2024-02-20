using UnityEngine;

public class OutlineSelection : MonoBehaviour, ISelectionResponse
{

    public void OnSelect(Transform transform)
    {
        transform.GetComponent<Outline>().enabled = true;
    }

    public void OnDeselect(Transform transform)
    {
        transform.GetComponent<Outline>().enabled = false;
    }
}