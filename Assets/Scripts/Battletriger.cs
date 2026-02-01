using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    public BattleManager manager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            manager.StartBattle();
            gameObject.SetActive(false);
        }
    }
}
