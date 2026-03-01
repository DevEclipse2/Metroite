using UnityEngine;

public class Objectives : Button
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject ObjectiveScreen;
    public Transform location;
    public override void Press()
    {
        Instantiate(ObjectiveScreen,location.position, location.transform.rotation);
    }
}
