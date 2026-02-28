using UnityEngine;

public class Processor : node
{
    public Element product;
    public Element element;
    public Element input1;
    public Element input2;
    public Element input3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        product = new Element();
        product.element = production.explosive;
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
