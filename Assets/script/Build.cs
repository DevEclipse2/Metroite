using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Build : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float rayDistance = 10000f;
    public LayerMask objectLayer;
    public GameObject target;
    public GameObject asteroid;
    public GameObject UIText;
    public GameObject UIStuff;
    public GameObject UIName;
    public GameObject lightbridge;
    public GameObject[] Buildings;
    public GameObject lifeSupport;
    disaster disaster;
    public string[] BuildingsName = new string[6]
    {
     "Extractor",
     "Assembler",
     "Node",
     "Blast Rig",
     "Fuel Cell",
     " "
    };
    public Vector3 hitpt;
    public Vector3 normal;
    public int targetIndex = 0;
    Vector2 scroll;
    public bool Factorysel;
    public string[] alternatives;

    public GameObject ProductionStat;
    public GameObject Menu;
    public GameObject Poor;
    List<GameObject> displayingscreens = new List<GameObject>();
    List<GameObject> productionscreens = new List<GameObject>();
    public GameObject[] excludeDestroy;
    public Transform SpawnScreen;
    GameObject conveyersource;
    GameObject conveyertarget;
    bool source;
    public GameObject Drill;


    public float Metal = 30;
    public float Oxygen = 0;
    public float Nitrogen = 2;

    public GameObject Port;
    public int extractorC;
    public int AssemblerC;
    public int NodeC    ;
    public int BlastRigC;
    public int FuelCellC;
    List<GameObject> Producers;
    List<GameObject> Conveyers = new List<GameObject>();

    void checkConveyer(GameObject Lightbridge)
    {
        if(Conveyers.Count == 0)
        {
            Conveyers.Add(Lightbridge);
            Lightbridge.GetComponent<conveyer>().Instance(conveyersource, conveyertarget);
            conveyersource = null;
            conveyertarget = null;
            return;
        }
        foreach(GameObject game in Conveyers)
        {
            conveyer ltb = game.GetComponent<conveyer>();
            if(ltb != null)
            {
                if(ltb.Source == conveyersource && ltb.Target == conveyertarget)
                {
                    //destroy lightbridge
                    int index = Conveyers.IndexOf(game);
                    Conveyers.RemoveAt(index);
                    conveyersource = null;
                    conveyertarget = null;
                    Destroy(game);
                    Destroy(Lightbridge);
                    return;
                }
                else if (ltb.Source == conveyertarget && ltb.Target == conveyersource)
                {
                    int index = Conveyers.IndexOf(game);
                    Lightbridge.GetComponent<conveyer>().Instance(conveyersource, conveyertarget);
                    conveyersource = null;
                    conveyertarget = null;
                    Conveyers[index] = Lightbridge;
                    Destroy(game);
                    return;
                }
            }
        }
        Lightbridge.GetComponent<conveyer>().Instance(conveyersource, conveyertarget);
        conveyersource = null;
        conveyertarget = null;

    }

    bool SubtractCost(int index)
    {
        switch (index)
        {
            case 0:
                if(Metal > extractorC)
                {
                    Metal -= extractorC;
                    return true;
                }
                return false;

            case 1:
                if (Metal > AssemblerC)
                {
                    Metal -= AssemblerC;
                    return true;
                }
                return false;
            case 2:
                if (Metal > NodeC)
                {
                    Metal -= NodeC;
                    return true;
                }
                return false;
            case 3:
                if (Metal > BlastRigC)
                {
                    Metal -= BlastRigC;
                    BlastRigC = 100000000;
                    return true;
                }
                return false;
            case 4:
                if(Metal > FuelCellC)
                {
                    Metal -= FuelCellC;
                    return true;
                }
                return false;

        }
        return false;
    }
    void Start()
    {
        disaster = lifeSupport.GetComponent<disaster>();
    }
    public void checkray(out GameObject target)
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, rayDistance, objectLayer))
        {
            // If the ray hits an object, output its name
            //Debug.Log("Hit object: " + hit.collider.gameObject.name);
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
                if (SubtractCost(targetIndex))
                {
                    GameObject factory = Instantiate(Buildings[targetIndex], hitpt + normal * 0.1f, Quaternion.LookRotation(normal));
                    factory.transform.parent = asteroid.transform;
                    if(BlastRigC > 1000 && Drill == null)
                    {
                        Drill = factory;
                    }
                }
                else
                {
                    Poor.transform.position = SpawnScreen.transform.position;
                    Poor.transform.rotation = SpawnScreen.transform.rotation;
                }
            }
            else if (target.layer == 6)
            {
                Factorysel = true;
                target.GetComponent<node>().ReadProduction(out alternatives);
            }
            else if(target.layer == 10) {
                target.gameObject.GetComponent<Button>().Press();

            }

        }
        
    }
    private void OnPrevious()
    {
        checkray(out target);
        if (target.layer == 9 )
        {
            if(!excludeDestroy.Contains(target.gameObject))
            {
                Destroy(target);

            }
            else
            {
                target.gameObject.transform.position = new Vector3(100,100, 100);
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
            else if(target != conveyersource)
            {
                conveyertarget = target;
            }
        }
        if(conveyersource != null && conveyertarget != null) 
        {
            GameObject Lightbridge = Instantiate(lightbridge);
            checkConveyer(Lightbridge);
        }
    }
    public void OnScrollWheel(InputValue value)
    {
        checkray(out target);
        if(target == asteroid)
        {
            Factorysel = false;
        }
        scroll = value.Get<Vector2>();
        if (!Factorysel)
        {
            targetIndex += (int)Mathf.Clamp(scroll.y, -1, 1);
            if(targetIndex < 0)
            {
                targetIndex += Buildings.Length;
            }
            if(targetIndex > Buildings.Length) {

                targetIndex = 0;
            }
        }
        else
        {
            int finalindex = 0;
            target.GetComponent<node>().ReadProduction(out alternatives);
            target.GetComponent<node>().ChangeProduction((int)Mathf.Clamp(scroll.y, -1, 1), out finalindex);
            UIText.GetComponent<displayProduction>().displaytext = alternatives;
            UIStuff.GetComponent<TextMeshProUGUI>().text = alternatives[(int)Mathf.Clamp(finalindex, 0, alternatives.Length)];

        }


    }
    public void OnJump()
    {
        checkray(out target);
        if(target.layer == 6)
        {
            /*
            if(displayingscreens.Count > 0 )
            {
                if(displayingscreens.Contains(target.gameObject))
                {
                    int index = displayingscreens.IndexOf(target.gameObject);
                    if(productionscreens[index] == null)
                    {
                        productionscreens[index] = Instantiate(ProductionStat, SpawnScreen.transform.position, SpawnScreen.transform.rotation);
                        productionscreens[index].GetComponent<updateProduction>().productionStats(target.gameObject);

                    }
                    else
                    {
                        productionscreens[index].transform.position = SpawnScreen.position;
                        productionscreens[index].transform.rotation = SpawnScreen.transform.rotation;

                    }
                }
                
            }
            else
            {
               */
                displayingscreens.Add(target.gameObject);
                productionscreens.Add(Instantiate(ProductionStat, SpawnScreen.transform.position, SpawnScreen.transform.rotation));
                productionscreens[productionscreens.Count - 1].GetComponent<updateProduction>().productionStats(target.gameObject);

            //}
        }
    }

    public void OnSprint(InputValue value)
    {
        Debug.Log("interact");
        Menu.transform.position = SpawnScreen.transform.position;
        Menu.transform.rotation = SpawnScreen.transform.rotation;
    }
    // Update is called once per frame
    void Update()
    {
        disaster.Increment(Nitrogen, Oxygen);
        Nitrogen = 0;
        Oxygen = 0;
        UIName.GetComponent<TextMeshProUGUI>().text = BuildingsName[targetIndex];
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
