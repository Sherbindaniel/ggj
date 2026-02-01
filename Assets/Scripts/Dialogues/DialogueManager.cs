using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    public GameObject bubbleRoot;
    public TMP_Text bubbleText;
    public Vector3 worldOffset = new Vector3(0f, 1.5f, 0f);

    DialogueLine[] lines;
    int index;
    bool playing;

    void Awake()
    {
        Instance = this;
        bubbleRoot.SetActive(false);
    }

    void Update()
    {
        if (!playing) return;

        if (Input.GetMouseButtonDown(0))
            Next();
    }

    public void StartDialogue(DialogueLine[] newLines)
    {
        if (newLines == null || newLines.Length == 0) return;

        lines = newLines;
        index = 0;
        playing = true;

        bubbleRoot.SetActive(true);
        ShowLine();
    }

    void ShowLine()
    {
        bubbleText.text = lines[index].text;

        if (lines[index].speaker != null)
            bubbleRoot.transform.position = lines[index].speaker.position + worldOffset;
    }

    void Next()
    {
        index++;

        if (index >= lines.Length)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }

    void EndDialogue()
    {
        playing = false;
        bubbleRoot.SetActive(false);
    }
}
