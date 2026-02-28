using UnityEngine;

public class Processor : node
{
    public Element product;
    public Element input1;
    public Element input2;
    public Element input3;
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
    public override Element PullElement(float amount)
    {
        element.amount -= amount;
        Element elementout = element;
        elementout.amount = amount;
        return elementout;
    }
}
