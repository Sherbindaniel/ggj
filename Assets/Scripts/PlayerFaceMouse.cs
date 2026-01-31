using UnityEngine;

public class PlayerFaceMouse : MonoBehaviour
{
    public Camera cam;
   

    void Start()
    {
        if (cam == null)
            cam = Camera.main;
       
    }

    void Update()
    {
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
       

        if (mouseWorld.x >= transform.position.x)
        {
            // Face right
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            // Face left
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
}
