using UnityEngine;

public class PoloGenerator : MonoBehaviour, ITextGenerator
{
    public string Generate(int number)
    {
        if (number % 5 == 0)
            return "Polo";
        return "";
    }
}