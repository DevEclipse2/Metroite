using UnityEngine;
using UnityEngine.Rendering;

public class BigRedButton : Button
{
    public GameObject Player;
    public override void Press()
    {
        if(Player.GetComponent<Build>().Drill != null)
        {
            Player.GetComponent<Build>().Drill.GetComponentInChildren<Drill>().Blast();
        }
        else
        {
            Debug.Log("no drill rig");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
