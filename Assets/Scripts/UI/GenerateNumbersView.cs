using UnityEngine;

public class GenerateNumbersView : MonoBehaviour
{
    public void OnPressButton()
    {
        Events.GenerateNumbers.Invoke();
    }
}