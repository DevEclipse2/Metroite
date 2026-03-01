using System.Collections.Generic;
using UnityEngine;


public class Processor : node
{
    public Element product;
    public Element input1;
    public Element input2;
    public Element input3;
    List<float> inputs = new List<float>();
    List<float> outputs = new List<float>();
    List<float> pulltimes = new List<float>();
    List<float> pushtimes = new List<float>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        product = new Element();
        product.element = production.explosive;
        input1 = new Element();
        input1.element = production.nitrogen;
        input2 = new Element();
        input2.element = production.oxygen;
        input3 = new Element();
        input3.element = production.Hydrogen;


    }
    public override bool AddElement(Element elementin)
    {
        switch (elementin.element)
        {
            case production.nitrogen: input1.amount += elementin.amount; return true; break;
            case production.oxygen:   input2.amount += elementin.amount; return true; break;
            case production.Hydrogen:   input3.amount += elementin.amount; return true; break;
        }
        return false;
    }
    public void Update()
    {
        //if()
        if(input1.amount > 3 && input2.amount > 6 && input3.amount > 5)
        {
            input1.amount -= 3; input2.amount -= 6; input3.amount -= 5;
            product.amount++;
        }
        inputs.Add(1);
        pushtimes.Add(Time.realtimeSinceStartup);
        if (pulltimes.Count > 0)
        {
            if (pulltimes[0] < Time.realtimeSinceStartup - 1)
            {
                pulltimes.RemoveAt(0);
                outputs.RemoveAt(0);

            }
        }
        if (pushtimes.Count > 0)
        {
            if (pushtimes[0] < Time.realtimeSinceStartup - 1)
            {
                pushtimes.RemoveAt(0);
                inputs.RemoveAt(0);

            }
        }
    }
    public override Element PullElement(float amount)
    {
        //element.amount -= amount;
        //Element elementout = element;
        //elementout.amount = amount;
        //return elementout;
        outputs.Add(amount);
        pulltimes.Add(Time.realtimeSinceStartup);
        Element elementout = new Element();
        elementout.element = product.element;
        if (product.amount > amount)
        {
            product.amount -= amount;
            elementout.amount = amount;
        }
        else
        {
            elementout.amount = product.amount;
            product.amount = 0;
        }
        return elementout;
    }
}
