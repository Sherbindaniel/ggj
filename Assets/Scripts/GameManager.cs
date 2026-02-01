using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Vector3 respawnPosition;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RespawnAtPlatform()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("PlatformScene"); // exact scene name
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = respawnPosition;

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
