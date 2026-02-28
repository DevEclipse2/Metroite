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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);

        camera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        camera.transform.Rotate(0, mouseX, 0);
    }

    // Update is called once per frame
    void Update()
    {
            rb.linearVelocity = camera.transform.forward * movedir.y + camera.transform.right * movedir.x;
    }
}
