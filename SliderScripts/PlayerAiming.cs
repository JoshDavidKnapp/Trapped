using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    //Variable for how fast the player turns
    public float turnSpeed = 10;

    //Main Camera Reference
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        //Sets the Main Camera
        mainCamera = Camera.main;
        //Turns off Cursor
        Cursor.visible = false;
        //Stops Cursor from moving
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Sets the y camera rotation
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        //Rotates the player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
    }
}
