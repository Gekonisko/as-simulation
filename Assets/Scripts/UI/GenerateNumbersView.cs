using UnityEngine;

public class GenerateNumbersView : MonoBehaviour
{
    public void PressButton()
    {
        Events.GenerateNumbers.Invoke();
    }
}