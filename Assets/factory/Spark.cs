using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;

public class Spark : MonoBehaviour
{
    public string name = string.Empty;
    Build buildobj;
    GameObject[] LosExtractors;
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
                    break;
                case "Assembler":
                    break;
                case "Miner":
                    break;
            }
        }
    }
    public void ScanLineOfSight()
    {
        if (buildobj == null)
        {

        }
    }
    public void UpdatePower()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
