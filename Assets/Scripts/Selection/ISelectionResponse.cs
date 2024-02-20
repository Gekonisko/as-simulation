using UnityEngine;

public interface ISelectionResponse
{
    public void OnSelect(Transform transform);
    public void OnDeselect(Transform transform);
}