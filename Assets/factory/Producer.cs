using UnityEngine;

public class Producer : node
{
    float rate = 4;
    public Element element;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    void Update()
    {
        element.amount += rate * Time.deltaTime;
    }
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
