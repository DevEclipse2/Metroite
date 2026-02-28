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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    void Update()
    {
        if(element == null)
        {
            element = new Element();
            element.element = 2;
        }
        //Debug.Log(element.amount);
        element.amount += rate * Time.deltaTime;
        inputs.Add(rate * Time.deltaTime);
        pushtimes.Add(Time.realtimeSinceStartup);
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
                inputs.RemoveAt(0);

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
        outputs.Add(amount);
        pulltimes.Add(Time.realtimeSinceStartup);
        element.amount -= amount;
        Element elementout = element;
        elementout.amount = amount;
        return elementout;
    }
}
