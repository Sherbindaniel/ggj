using UnityEngine;

public class CameraCinematic : MonoBehaviour
{
    public Camera cam;
    public Transform targetPoint;
    public float targetOrthoSize = 3.5f;
    public float speed = 4f;

    float originalSize;
    Vector3 originalPos;
    bool playing;

    void Start()
    {
        if (cam == null) cam = Camera.main;
        originalSize = cam.orthographicSize;
        originalPos = cam.transform.position;
    }

    void Update()
    {
        if (!playing) return;

        if (targetPoint != null)
        {
            Vector3 desired = new Vector3(targetPoint.position.x, targetPoint.position.y, cam.transform.position.z);
            cam.transform.position = Vector3.Lerp(cam.transform.position, desired, Time.deltaTime * speed);
        }

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetOrthoSize, Time.deltaTime * speed);
    }

    public void Play()
    {
        playing = true;
    }

    public void Stop()
    {
        playing = false;
        cam.orthographicSize = originalSize;
        cam.transform.position = originalPos;
    }
}
