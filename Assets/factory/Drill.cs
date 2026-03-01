using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class Drill : node
{
    Element explosive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int Progress = 60;
    public float Cd;
    bool canpress;
    public GameObject lifeSupport;
    disaster disaster;
    private void Update()
    {
        Cd += Time.deltaTime;
        if(Cd > 30)
        {
            canpress = true;
        }
    }
    void Start()
    {
        disaster = lifeSupport.GetComponent<disaster>();

    }


    public override bool AddElement(Element elementin)
    {
        switch (elementin.element)
        {
            case production.explosive: explosive.amount += elementin.amount; return true; break;
        }
        return false;
    }
    // Update is called once per frame
    public void Blast()
    {
        disaster.ifFireHappeningRn = true;
        disaster.hullIntegrity -= 0.15f;
        if (explosive.amount >= 0)
        {
            canpress = false;
            if (explosive.amount > 15f)
            {
                explosive.amount -= 15;
                Progress -= Mathf.FloorToInt(15);
            }
            else
            {
                Progress -= Mathf.FloorToInt(explosive.amount);
                explosive.amount = 0;
            }
            if (Progress <= 0)
            {
                SceneManager.LoadScene("Win");
            }
        }
        else
        {
            Debug.Log("NoExplosives");
        }
    }
}
