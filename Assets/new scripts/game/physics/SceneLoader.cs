using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneLoader
{
    public static List<GameObject> bulkheads;
    public static List<int>     ActiveIndexes;
    public static void playerMoved(int selfId, List<int> connectedIndexes)
    {
        foreach (int index in ActiveIndexes) {
            if (!connectedIndexes.Contains(index))
            {
                bulkheads[index].GetComponent<Bulkheads>().Unload();
            }
        }
        foreach (int index in connectedIndexes)
        {
            bulkheads[index].GetComponent<Bulkheads>().setGravFieldActive(true);
        }
        //when a player moves into a bulkhead, adjacent rooms are also loaded, while non adjacent rooms that were loaded before unload after 3 seconds
    }
    public static int Init(GameObject gameobj,GameObject[] connections)
    {
        foreach (GameObject connection in connections) 
        {
            if(bulkheads.Contains(connection))
            {
                gameobj.GetComponent<Bulkheads>().connectionIndexes.Add(bulkheads.IndexOf(connection));
            }
            else
            {
                bulkheads.Add(connection);
                gameobj.GetComponent<Bulkheads>().connectionIndexes.Add(bulkheads.Count - 1);
            }
        }
        if(bulkheads.Contains(gameobj))
        {
            return (bulkheads.IndexOf(gameobj));
        }
        else
        {
            bulkheads.Add(gameobj);
            return bulkheads.Count - 1;
        }
    }
}
