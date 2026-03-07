using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Producer : node
{
    float rate = 4;
    public Element element;
    string thingname = "Miner";
    public int possibleOutputs;
    public float[] rates = new float[4]
    {
         0.4f,
         1.6f,
         1.6f,
         3.2f,
    };
    public string[] OutputNames = new string[4]
        {
            "Metal",
            "Oxygen",
            "Nitrogen",
            "Hydrogen",
        };
    List<float> inputs = new List<float>();
    List<float> outputs = new List<float>();
    List<float> pulltimes = new List<float>();
    List<float> pushtimes = new List<float>();
    List<Spark> sparks = new List<Spark>();
    public float powerRunning = 0.25f;
    bool powered;
    float powerRequired;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    public override string GetName()
    {
        return thingname;
    }
    public override void AddSpark(GameObject spark)
    {
        sparks.Add(spark.GetComponent<Spark>());
    }
    public override void SubtractPower(out float power, float availpwr, bool FirstT)
    {
        if (FirstT)
        {
            powerRequired = powerRunning;
        }
        if (availpwr > powerRequired / sparks.Count)
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
    void Update()
    {
        powered = true;
        if(element == null)
        {
            element = new Element();
            element.element = production.nitrogen;
        }
        //Debug.Log(element.amount);
        if (powered)
        {
            element.amount += rate * Time.deltaTime;
            inputs.Add(rate * Time.deltaTime);
            pushtimes.Add(Time.realtimeSinceStartup);
        }
        if(pulltimes.Count > 0)
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
                if (inputs.Count > 0)
                {
                    inputs.RemoveAt(0);
                }
            }
        }

    }
    public override void ChangeProduction(int shift, out int final)
    {
        possibleOutputs += shift;
        if(possibleOutputs < 0) {
            possibleOutputs = 3;
        }
        possibleOutputs %= 4;
        element.element = possibleOutputs;
        final = possibleOutputs;
        rate = rates[possibleOutputs];
    }
    public override void ReadProduction(out string[] alternatives)
    {
        alternatives = OutputNames;
    }
    public override void ForceElement(Element inelement)
    {
        element = inelement;
    }
    public override void GetStats(out string name, out string mat, out float input, out float output)
    {
        name = thingname;
        mat = production.GetMat(element.element);
        float sumi = 0;
        foreach(float i in inputs)
        {
            sumi += i;
        }
        float sumo = 0;
        foreach (float i in outputs)
        {
            sumo += i;
        }
        input = sumi;
        output = sumo;
    }
    public override bool AddElement(Element elementin)
    {
        if (element.element == elementin.element)
        {
            inputs.Add(rate * Time.deltaTime);
            pushtimes.Add(Time.realtimeSinceStartup);
            element.amount += elementin.amount;
            return true;
        }
        return false;
    }
    public override Element PullElement(float amount)
    {
        pulltimes.Add(Time.realtimeSinceStartup);
        Element elementout = new Element();
        elementout.element = element.element;
        if (element.amount > amount)
        {
            element.amount -= amount;
            elementout.amount = amount;
            outputs.Add(amount);

        }
        else
        {
            elementout.amount = element.amount;
            element.amount = 0;
            outputs.Add(elementout.amount);

        }
        return elementout;
    }
}
