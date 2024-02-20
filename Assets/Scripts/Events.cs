using System;

public class Events
{
    public static Action AgentDeath;
    public static Action<AgentData> SelectAgent;
    public static Action<AgentData> UpdateAgent;
}