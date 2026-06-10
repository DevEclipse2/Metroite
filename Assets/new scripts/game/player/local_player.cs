///
/// for player, they are in many states of movement
/// when on ground, they can walk normally
/// if something is above them that they are unable to "lift" they are put into prone mode
/// in the air, the player body is oriented towards mouse directions
/// this midair position can either be in semi upright or in 'swimming' position
/// in the air, the player can push off of objects or swim *very* slowly
/// when the player lands, they have a critical angle, where they can either right themselves, or fall and have to climb back up
/// if the player is spun, their arm can act as a counterbalance to slow down
/// the player can look at any surface to grab onto and align


using UnityEngine;
using UnityEngine.InputSystem;

public class local_player : SpaceObject
{
    public bool paused = false;

    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float[] jumpMultiplier;


    [SerializeField]
    private float movementspeed = 6.0f;
    [SerializeField]
    private float jumpForce = 6.0f;
    [SerializeField]
    Transform cameraLook;
    [SerializeField]
    GameObject camera;
    [SerializeField]
    GameObject body;
    Vector2 inputdir;
    Vector2 movedir;
    private float verticalRotation = 0.0f;
    public float lowerRotationLimit = 80.0f;
    public float upperRotationLimit = 80.0f;
    bool controller;
    public float acceleration = 4.6f;
    float speedRamp;


    Vector3 additionalVelocity; // current vector velocity of all external forces

    Vector3 constantForceVec; // forces that act on the player continuously

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
        verticalRotation = Mathf.Clamp(verticalRotation, -upperRotationLimit * 2, lowerRotationLimit * 2);

        if (controller)
        {
            camera.transform.localRotation = Quaternion.Euler(verticalRotation + camera.transform.localRotation.x, 0, 0);
            body.transform.Rotate(0, mouseX, 0);
        }
        else
        {
            camera.transform.localRotation = Quaternion.Euler(verticalRotation * 0.4f + camera.transform.localRotation.x, 0, 0);
            body.transform.Rotate(0, mouseX * 0.4f, 0);
        }
    }

    
    public void OnJump(InputValue value)
    { if (paused) return;
        if (value != null)
        {
            additionalVelocity += jumpForce * body.transform.up;
        }
    }
    public void OnMove(InputValue value)
    {
        if (paused)
        {
            return;
        }

        if (value != null)
        {
            inputdir = value.Get<Vector2>();
            if(inputdir != Vector2.zero)
            {
                movedir = inputdir;
            }
            //cameraLook.rotation
        }
    }
    public override void PhysicsTick()
    {
        rb.linearVelocity = velocityVector;
        rb.angularVelocity = angularVelocity;
        rb.linearVelocity += movedir.y * speedRamp * body.transform.forward + movedir.x * speedRamp * body.transform.right;
        //reduce velocityVector by resistance
        Vector3 resisAmt = new Vector3((1 - resistance.x) * velocityVector.x, (1 - resistance.y) * velocityVector.y, (1 - resistance.z) * velocityVector.z);
        velocityVector -= resisAmt * Time.deltaTime;
        Vector3 AngresisAmt = new Vector3((1 - angularResistance.x) * angularVelocity.x, (1 - angularResistance.y) * angularVelocity.y, (1 - angularResistance.z) * angularVelocity.z);
        angularResistance -= AngresisAmt * Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        if (speedRamp < 0.03 && speedRamp > -0.03 && movedir.magnitude == 0)
        {
            speedRamp = 0;
        }
        else
        {
            if (speedRamp < inputdir.magnitude * movementspeed)
            {
                speedRamp += acceleration * Time.deltaTime;
            }
            else if (speedRamp > inputdir.magnitude * movementspeed)
            {
                speedRamp -= 4 * acceleration * Time.deltaTime;
            }
        }
        
        PhysicsTick();
        //something hitting the head would force player to crouch
    }
}
