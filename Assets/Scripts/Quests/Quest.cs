using UnityEngine;

[System.Serializable]
public class Quest
{
    public string name;
    [TextArea(3, 10)]
    public string description;

    public string task;
}
