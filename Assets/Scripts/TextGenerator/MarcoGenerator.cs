using UnityEngine;

public class MarcoGenerator : MonoBehaviour, ITextGenerator
{
    public string Generate(int number)
    {
        if (number % 3 == 0)
            return "Marco";
        return "";
    }
}