using System;
using UnityEngine;

[Serializable]
public struct AgentData
{
    [HideInInspector] public string name;
    public int hp;
    public float speed;
    public float showHitTime;
}
