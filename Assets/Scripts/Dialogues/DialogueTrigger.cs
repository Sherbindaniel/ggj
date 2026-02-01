using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueLine[] lines;
    bool used;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (used) return;
        if (!other.CompareTag("Player")) return;

        used = true;
        DialogueManager.Instance.StartDialogue(lines);
    }
}
