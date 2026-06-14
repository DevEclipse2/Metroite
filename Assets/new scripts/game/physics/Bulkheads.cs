using NUnit.Framework;
using System;
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
    public byte playerCount = 0;
    public byte vehicleCount = 0;
    private bool active;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        id = SceneLoader.Init(this.gameObject, connections);
    }
    public void setGravFieldActive(bool value)
    {
        active = value;
        gravityFields.GetComponent<gravityField>().enabled = value;
        if (value)
        {
            foreach (GameObject presentobjects in ObjectsPresent)
            {
                try
                {
                    if (presentobjects.CompareTag("AlwaysOn"))
                    {
                        presentobjects.GetComponent<Collider>().enabled = true;
                    
                    }
                    else
                    {
                        presentobjects.GetComponent<Collider>().enabled = true;
                        presentobjects.SetActive(true);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
        }
        else
        {
            foreach (GameObject presentobjects in ObjectsPresent)
            {
                try
                {
                    if (presentobjects.CompareTag("AlwaysOn"))
                    {
                    
                        presentobjects.GetComponent<Rigidbody>().Sleep();
                        presentobjects.GetComponent<Collider>().enabled = false;
                    
                    }
                    else
                    {
                    
                        presentobjects.GetComponent<Rigidbody>().Sleep();
                        presentobjects.GetComponent<Collider>().enabled = false;
                        presentobjects.SetActive(false);
                    
                    }
                }
                catch(Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
        }
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
            playerCount++;
            SceneLoader.playerMoved(id, connectionIndexes);
        }
        else if (other.CompareTag("Vehicle"))
        {
            vehicleCount++;
            SceneLoader.vehicleMoved(id, connectionIndexes);
        }
        if (active)
        {
            if (other.CompareTag("AlwaysOn"))
            {
                other.enabled = true;

            }
            else
            {
                other.enabled = true;
                other.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ObjectsPresent.Remove(other.gameObject);
        if (other.CompareTag("Player"))
        {
            playerCount--;
            SceneLoader.playerMoved(id, connectionIndexes);
        }
        else if(other.CompareTag("Vehicle"))
        {
            vehicleCount--;
            SceneLoader.vehicleMoved(id, connectionIndexes);

        }
    }
    // Update is called once per frame
}
