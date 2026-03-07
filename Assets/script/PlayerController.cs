using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    public float movementspeed = 6.0f;
    public Rigidbody rb;
    public Transform cameraLook;
    public GameObject camera;
    public GameObject asteroid;
    Vector2 movedir;
    private float verticalRotation = 0.0f;
    public float verticalRotationLimit = 80.0f;
    bool controller;
    int controlmethod = 0;
    Build buildtool;
    GameObject looking;
    bool viewlock;
    public GameObject hudtip;
    HudTips hudTips;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        buildtool = GetComponent<Build>();
        if (Gamepad.current != null)
        {
            // A gamepad is connected
            Debug.Log("Gamepad connected!");
            controller = true;
        }
        hudTips = hudtip.GetComponent<HudTips>();
        rb = GetComponent<Rigidbody>();
    }
    public void OnViewLock(InputValue value)
    {
        viewlock = !viewlock;
        
        hudTips.ToggleRot();
    }
    public void OnMove( InputValue value)
    {
        if (value != null)
        {
            hudTips.Move();
            //Debug.Log(value.Get<Vector2>());
            movedir = value.Get<Vector2>();
            //cameraLook.rotation
            
        }
    }
    void OnCancel()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;

        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    public void OnLook(InputValue value)
    {
        hudTips.Look();
        //Debug.Log("look");
        float mouseX = value.Get<Vector2>().x;
        float mouseY = value.Get<Vector2>().y;

        verticalRotation -= mouseY;
        if (!viewlock)
        {
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
        else
        {
            asteroid.transform.Rotate(mouseY, mouseX, 0);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        rb.linearVelocity = transform.forward * movedir.y * movementspeed + transform.right * movedir.x * movementspeed + transform.up * -3.81f;
    }
}
