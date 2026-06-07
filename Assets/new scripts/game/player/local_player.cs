using UnityEngine;
using UnityEngine.InputSystem;

public class local_player : MonoBehaviour
{
    public bool paused = false;

    [SerializeField]
    private float movementspeed = 6.0f;
    Rigidbody rb;
    [SerializeField]
    Transform cameraLook;
    [SerializeField]
    GameObject camera;
    [SerializeField]
    GameObject asteroid;

    Vector2 movedir;
    private float verticalRotation = 0.0f;
    public float verticalRotationLimit = 80.0f;
    bool controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (Gamepad.current != null)
        {
            // A gamepad is connected
            Debug.Log("Gamepad connected!");
            controller = true;
        }
        rb = GetComponent<Rigidbody>();
    }

    public void OnLook(InputValue value)
    {
        if(paused)
        {
            return;
        }

        float mouseX = value.Get<Vector2>().x;
        float mouseY = value.Get<Vector2>().y;

        verticalRotation -= mouseY;
        
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);
        if (controller)
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

    public void gravitate(Vector3 vector, float magnitude)
    {
        rb.AddForce(vector.normalized * magnitude, ForceMode.Acceleration);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = transform.forward * movedir.y * movementspeed + transform.right * movedir.x * movementspeed + transform.up * -3.81f;

        //something hitting the head would force player to crouch
    }
}
