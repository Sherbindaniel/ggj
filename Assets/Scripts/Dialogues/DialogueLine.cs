using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public Transform speaker;
    [TextArea(2, 5)]
    public string text;
}
