using UnityEngine;
using UnityEngine.Windows;

public class Drill : node
{
    Element explosive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public override bool AddElement(Element elementin)
    {
        switch (elementin.element)
        {
            case production.explosive: explosive.amount += elementin.amount; return true; break;
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
