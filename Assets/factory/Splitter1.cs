using System.Collections.Generic;
using UnityEngine;


public class Processor : node
{
    public Element product;
    public Element input1;
    public Element input2;
    public Element input3;
    public float powerRunning = 20f;
    public float powerIdle = 0.8f;
    List<float> inputs = new List<float>();
    List<float> outputs = new List<float>();
    List<float> pulltimes = new List<float>();
    List<float> pushtimes = new List<float>();
    List<Spark> sparks = new List<Spark>();
    bool work;
    bool powered;
    float powerRequired;


    string[] alternativeProd = new string[1]{
    "No production Options"
    };
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
    public override void AddSpark(GameObject spark)
    {
        sparks.Add(spark.GetComponent<Spark>());
    }
    public override void SubtractPower(out float power, float availpwr , bool FirstT)
    {
        if (FirstT)
        {
            powerRequired = powerIdle;
            if (work)
            {
                powerRequired = powerRunning;
                work = false;
            }
        }
        if(availpwr > powerRequired / sparks.Count)
        {
            power = availpwr - powerRequired / sparks.Count;
            powerRequired -= powerRequired / sparks.Count;
        }
        else
        {
            power = 0;
            powerRequired -= availpwr;
        }
    }

    public override void ChangeActive(bool active)
    {
        powered = active;
    }
    public override void GetStats(out string name, out string mat, out float input, out float output)
    {
        name = "Assembler";
        mat = production.GetMat(product.element);
        input = 0;
        foreach(float f in inputs)
        {
            input += f;
        }
        output = 0;
        foreach (float f in outputs)
        {
            output += f;
        }

    }
    public override void ReadProduction(out string[] alternatives)
    {
        alternatives = alternativeProd;
    }
    public override string GetName()
    {
        return "Assembler";
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
            if (powered)
            {
                input1.amount -= 3; input2.amount -= 6; input3.amount -= 5;
                product.amount++;
                work = true;
                inputs.Add(1);
                pushtimes.Add(Time.realtimeSinceStartup);
            }
        }
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
