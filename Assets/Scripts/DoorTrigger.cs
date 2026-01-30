using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    public string sceneToLoad = "CityScene";
    public bool missionStarted = false; // you can set this from your General dialogue script

    private bool canTrigger = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTrigger) return;
        if (!other.CompareTag("Player")) return;

        if (missionStarted)
        {
            canTrigger = false;
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            // Before mission: send player back to bedroom position (same scene)
            other.transform.position = new Vector3(-6f, other.transform.position.y, other.transform.position.z);
        }
    }
}
