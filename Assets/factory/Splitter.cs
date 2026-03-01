using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.CameraUI;

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
        if (element.element == elementin.element)
        {
            element.amount += elementin.amount;
            return true;
        }
        return false;
    }
    public override Element PullElement(float amount)
    {
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
