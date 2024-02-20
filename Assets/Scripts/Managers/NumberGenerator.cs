using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGenerator : MonoBehaviour
{
    private ITextGenerator[] textGenerators;

    private void Awake()
    {
        Events.GenerateNumbers += OnGenerate;
    }

    private void Start()
    {
        textGenerators = GetComponents<ITextGenerator>();
    }

    private void OnGenerate()
    {
        for (int i = 1; i <= 100; i++)
        {
            string line = "";
            foreach (ITextGenerator generator in textGenerators)
            {
                line += generator.Generate(i);
            }

            Debug.Log(line == "" ? i : line);
        }
    }

    private void OnDestroy()
    {
        Events.GenerateNumbers -= OnGenerate;
    }
}
