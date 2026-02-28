using UnityEngine;
using System.Collections.Generic;
public class productionVessel : node
{
    public Element elements;
    public float MaxproductionTime = 0.25f;
    float UnitAmt = 4f;
    public float productionTime = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public override Element PullElement(Element element, float amount)
    {
        elements.amount -= amount;
        return elements;
    }
    // Update is called once per frame
    void Update()
    {
        productionTime += Time.deltaTime;
        if(productionTime > MaxproductionTime)
        {
            elements.amount += UnitAmt;
            productionTime = 0;
        }
    }
}
