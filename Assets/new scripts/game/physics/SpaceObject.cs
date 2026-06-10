using UnityEngine;

/// <summary>
/// base class for all physics objects in space
/// </summary>
public class SpaceObject : MonoBehaviour
{
    [SerializeField] protected Vector3 velocityVector;
    [SerializeField] protected Vector3 angularVelocity;
    [SerializeField] protected Vector3 resistance;
    [SerializeField] protected Vector3 angularResistance;
    [SerializeField] protected Rigidbody rb;

    protected void init()
    {
        resistance = resistance.normalized;
        angularVelocity = angularResistance.normalized;
    }

    public virtual void PhysicsTick()
    {
        rb.linearVelocity = velocityVector;
        rb.angularVelocity = angularVelocity;
        //reduce velocityVector by resistance
        Vector3 resisAmt = new Vector3( (1 - resistance.x) * velocityVector.x, (1 - resistance.y) * velocityVector.y, (1 - resistance.z) * velocityVector.z);
        velocityVector -= resisAmt * Time.deltaTime;
        Vector3 AngresisAmt = new Vector3((1 - angularResistance.x) * angularVelocity.x, (1 - angularResistance.y) * angularVelocity.y, (1 - angularResistance.z) * angularVelocity.z);
        angularResistance -= AngresisAmt * Time.deltaTime;
    }
    public virtual void AddForcePoint(Vector3 contactpoint,float magnitude , Vector3 forceDir)
    {
        //the added force is converted into angular velocity and airspeed velocity based on distance to center, intertia and resistance
    }
    public virtual void AddForceWhole(Vector3 forceDir, float magnitude)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
