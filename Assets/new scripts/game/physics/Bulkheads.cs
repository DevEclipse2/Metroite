using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulkheads : MonoBehaviour
{
    
    public GameObject gravityFields;
    public int id;
    public GameObject[] connections;
    public List<int> connectionIndexes;
    public HashSet<GameObject> ObjectsPresent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        id = SceneLoader.Init(this.gameObject, connections);
    }
    public void setGravFieldActive(bool value)
    {
        gravityFields.GetComponent<gravityField>().enabled = value;
    }
    public void Unload()
    {
        StartCoroutine(DelayedUnload());
    }
    IEnumerator DelayedUnload()
    {
        Debug.Log("begin unload");

        // Pause execution for 2 seconds
        yield return new WaitForSeconds(4f);
        setGravFieldActive(false);
        Debug.Log("unloaded");
    }
    private void OnTriggerEnter(Collider other)
    {
        // Only track objects with a specific tag or component if needed
        ObjectsPresent.Add(other.gameObject);

        // Notify Manager if the object is the player
        if (other.CompareTag("Player"))
        {
            SceneLoader.playerMoved(id, connectionIndexes);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ObjectsPresent.Remove(other.gameObject);
        if (other.CompareTag("Player"))
        {
            SceneLoader.playerMoved(id, connectionIndexes);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
