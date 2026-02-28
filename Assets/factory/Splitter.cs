using UnityEngine;

public class Splitter : node
{
    public Element element;
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
        element.amount -= amount;
        Element elementout = element;
        elementout.amount = amount;
        return elementout;
    }
}
