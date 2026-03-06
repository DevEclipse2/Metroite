using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;

public class Spark : MonoBehaviour
{
    public string name = string.Empty;
    Build buildobj;
    List<GameObject>    LosExtractors;
    List<GameObject>    LosMiners;
    List<GameObject>    LosAssemblers;
    GameObject          LosDrill;
    List<GameObject>    LosFuelCells;
    GameObject          LosPort;
    public double PowerAvailable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void LoadProducers(List<GameObject> objects)
    {

    }
    public void Init(string newname , Build build)
    {
        name = newname;
        buildobj = build;
        ScanLineOfSight();
    }
    public void CheckNewObject(GameObject gameObject)
    {
        Ray ray = new Ray(this.transform.position, (gameObject.transform.position - this.transform.position).normalized);
        RaycastHit hit;
        // Perform the raycast
        if (Physics.Raycast(ray, out hit, 600, 6))
        {
            switch (hit.collider.gameObject.GetComponent<node>().GetName())
            {
                case "Port":
                    LosPort = hit.collider.gameObject;
                    break;
                case "Assembler":
                    LosAssemblers.Add(hit.collider.gameObject);
                    break;
                case "Miner":
                    LosMiners.Add(hit.collider.gameObject);
                    break;
                case "Drill":
                    LosDrill = hit.collider.gameObject;
                    break;
                case "Node":
                    //LosNode.Add(hit.collider.gameObject);
                    break;
                case "FuelCell":
                    LosFuelCells.Add(hit.collider.gameObject);
                    break;
            }
        }
    }
    public void ScanLineOfSight()
    {
        LosExtractors = new List<GameObject>();
        LosAssemblers = new List<GameObject>();
        LosExtractors = new List<GameObject>();
        LosFuelCells = new List<GameObject>();
        if (buildobj == null)
        {
            foreach(GameObject building in buildobj.Buildings)
            {
                CheckNewObject(building);
            }
        }
    }
    public void UpdatePower()
    {
        PowerAvailable = 0;
        foreach(GameObject fCell in LosFuelCells)
        {
            float power;
            fCell.GetComponent<node>().SubtractPower(out power);
            PowerAvailable += power;
        }
        foreach
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
