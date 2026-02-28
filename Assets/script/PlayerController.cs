using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    public float movementspeed = 6.0f;
    public Rigidbody rb;
    public Transform cameraLook;
    public GameObject camera;
    Vector2 movedir;
    private float verticalRotation = 0.0f;
    public float verticalRotationLimit = 80.0f;
    bool controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Gamepad.current != null)
        {
            // A gamepad is connected
            Debug.Log("Gamepad connected!");
            controller = true;
        }
        rb = GetComponent<Rigidbody>();
    }
    public void OnMove( InputValue value)
    {
        if (value != null)
        {
            Debug.Log(value.Get<Vector2>());
            movedir = value.Get<Vector2>();
            //cameraLook.rotation
            
        }
    }
    public void OnLook(InputValue value)
    {
        
        float mouseX = value.Get<Vector2>().x;
        float mouseY = value.Get<Vector2>().y;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit , verticalRotationLimit);
        if(controller)
        {
            camera.transform.localRotation = Quaternion.Euler(verticalRotation + camera.transform.localRotation.x, 0, 0);
            transform.Rotate(0, mouseX, 0);
        }
        else
        {
            camera.transform.localRotation = Quaternion.Euler(verticalRotation * 0.4f + camera.transform.localRotation.x, 0, 0);
            transform.Rotate(0, mouseX * 0.4f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
            rb.linearVelocity = transform.forward * movedir.y + transform.right * movedir.x;
    }
}
