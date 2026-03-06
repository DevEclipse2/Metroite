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
    public AudioSource source;
    private void Update()
    {
        Cd += Time.deltaTime;
        if(Cd > 30)
        {
            canpress = true;
            Cd = 0;
        }
    }
    void Start()
    {
        disaster = lifeSupport.GetComponent<disaster>();
        explosive = new Element();
        explosive.element = production.explosive;
    }


    public override bool AddElement(Element elementin)
    {
        if (explosive == null) { 
            explosive=new Element();
            explosive.element = production.explosive;

        }
        if (elementin == null) { 
            return false;
        }
        if(elementin.element == production.explosive)
        {

            explosive.amount += elementin.amount;
            return true;
        }
        return false;
    }

    public override string GetName()
    {
        return "Drill";
    }


    // Update is called once per frame
    public void Blast()
    {
        
        if (explosive.amount >= 1 && canpress)
        {
            disaster.ifFireHappeningRn = true;
            disaster.hullIntegrity -= 0.15f;
            source.Play();
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
