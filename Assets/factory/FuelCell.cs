using UnityEngine;
using System.Collections.Generic;
public class FuelCell : node
{
    Element hydrogen;
    Element oxygen;
    List<Spark> sparks = new List<Spark>();
    float poweroutput1 = 0.5f;
    float poweroutput2 = 1.75f;
    bool low;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hydrogen = new Element();
        oxygen = new Element();
        hydrogen.element = production.Hydrogen;
        oxygen.element = production.oxygen;
    }
    public override string GetName()
    {
        return "FuelCell";
    }
    public override void AddSpark(GameObject spark)
    {
        sparks.Add(spark.GetComponent<Spark>());
    }
    public override void SubtractPower(out float power, float availpwr, bool FirstT)
    {
        if (low)
        {
            power = availpwr += poweroutput1 / sparks.Count;

        }
        else
        {
            power = availpwr += poweroutput2 / sparks.Count;
        }
    }
    public override bool AddElement(Element elementin)
    {
        switch (elementin.element)
        {
            case production.oxygen: oxygen.amount += elementin.amount; return true; break;
            case production.Hydrogen: hydrogen.amount += elementin.amount; return true; break;
        }
        return false;
    }
    public override Element PullElement(float amount)
    {
        return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
