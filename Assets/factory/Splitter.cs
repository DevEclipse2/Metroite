using System.Collections.Generic;
using UnityEngine;

public class Splitter : node
{
    public Element element;
    List<float> inputs = new List<float>();
    List<float> outputs = new List<float>();
    List<float> pulltimes = new List<float>();
    List<float> pushtimes = new List<float>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override bool AddElement(Element elementin)
    {
        if(element == null)
        {
            element = elementin;
            return true;
        }
        if (element.element == elementin.element)
        {
            element.amount += elementin.amount;
            return true;
        }
        else if (element.amount < 0.01)
        {
            element = elementin;
            return true;
        }
        return false;
    }
    public void Update()
    {
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
                if(inputs.Count > 0)
                {
                    inputs.RemoveAt(0);

                }

            }
        }
    }

    public override string GetName()
    {
        return "Node";
    }
    public override void GetStats(out string name, out string mat, out float input, out float output)
    {
        name = "Node";
        mat = production.GetMat(element.element);
        float sumi = 0;
        foreach (float i in inputs)
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
    public override void ReadProduction(out string[] alternatives)
    {
        alternatives = new string[1];
        alternatives[0] = "No Production Options";
    }
    public override Element PullElement(float amount)
    {
        if(element == null)
        {
            element = new Element();
            element.element = production.Hydrogen;
            element.amount = 0;
        }
        outputs.Add(amount);
        pulltimes.Add(Time.realtimeSinceStartup);
        Element elementout = new Element();
        elementout.element = element.element;
        if (element.amount > amount)
        {
            element.amount -= amount;
            elementout.amount = amount;
        }
        else
        {
            elementout.amount = element.amount;
            element.amount = 0;
        }
        return elementout;
    }
}
