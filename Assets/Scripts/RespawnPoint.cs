using UnityEngine;

public class RespawnPointSetter : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.respawnPosition = transform.position;
    }
}
