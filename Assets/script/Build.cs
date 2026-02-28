using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class Build : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float rayDistance = 10000f;
    public LayerMask objectLayer;
    public GameObject target;
    public GameObject asteroid;
    public GameObject UIText;
    public GameObject UIName;
    public GameObject lightbridge;
    public GameObject[] Buildings;
    public Vector3 hitpt;
    public Vector3 normal;
    public int targetIndex;
    Vector2 scroll;
    public bool Factorysel;
    public string[] alternatives;


    GameObject conveyersource;
    GameObject conveyertarget;
    bool source;

    void Start()
    {
        
    }
    public void checkray(out GameObject target)
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, rayDistance, objectLayer))
        {
            // If the ray hits an object, output its name
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            target = hit.collider.gameObject;
            hitpt = hit.point;
            normal = hit.normal;
            // You can perform actions based on the hit object here
        }
        else
        {
            target = null;
        }

    }
    void build()
    {

    }
    public void OnClick(InputValue value)
    {
        if(value.isPressed == true)
        {
            checkray(out target);
            if (target == asteroid)
            {
                Factorysel = false;
                GameObject factory = Instantiate(Buildings[targetIndex], hitpt + normal * 0.1f, Quaternion.LookRotation(normal));
                factory.transform.parent = asteroid.transform;
            }
            else if (target.layer == 6)
            {
                Factorysel = true;
                target.GetComponent<node>().ReadProduction(out alternatives);
            }

        }
        
    }
    void OnRightClick(InputValue value)
    {
        target = null;
        checkray(out target);

        if (value.isPressed == true && target != null&&  target.layer == 6)
        {
            source = !source;
            if(source)
            {
                conveyersource = target;
            }
            else
            {
                conveyertarget = target;
            }
        }
        if(conveyersource != null && conveyertarget != null) 
        {
            GameObject Lightbridge = Instantiate(lightbridge);
            Lightbridge.GetComponent<conveyer>().Instance(conveyersource, conveyertarget);
            conveyersource = null;
            conveyertarget = null;
        }
    }
    public void OnScrollWheel(InputValue value)
    {
        scroll = value.Get<Vector2>();
        if (!Factorysel)
        {
            targetIndex += (int)Mathf.Clamp(scroll.y, -1, 1);
            if(targetIndex < 0)
            {
                targetIndex += Buildings.Length;
            }
        }
        else
        {
            int finalindex = 0;
            target.GetComponent<node>().ReadProduction(out alternatives);
            target.GetComponent<node>().ChangeProduction((int)Mathf.Clamp(scroll.y, -1, 1), out finalindex);
            string[] selected = new string[1];
            selected[0] = alternatives[finalindex];
            UIText.GetComponent<displayProduction>().displaytext = alternatives;
            UIName.GetComponent<displayProduction>().displaytext = selected;

        }


    }
    public void OnJump()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //if ( target.layer == 6) {
            /*string[] name = new string[1];
            string name2 = " ";
            target.GetComponent<node>().GetName(out name2);
            name[0] = name2;
            UIName.GetComponent<displayProduction>().displaytext = name;
            */        
        //}
    }
}
