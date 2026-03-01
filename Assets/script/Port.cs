using UnityEngine;
using UnityEngine.Windows;

public class Port : node
{
    Element Oxy;
    Element Nitro;
    Element Metal;
    public GameObject build;
    Build buildclass;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Oxy = new Element();
        Nitro = new Element();
        Metal = new Element();
        Oxy.element = production.oxygen;
        Nitro.element = production.nitrogen;
        Metal.element = production.metal;
        buildclass = build.GetComponent<Build>();
    }
    public override bool AddElement(Element elementin)
    {
        switch (elementin.element)
        {
            case production.nitrogen: Nitro.amount += elementin.amount; return true; break;
            case production.oxygen: Oxy.amount += elementin.amount; return true; break;
            case production.metal: Metal.amount += elementin.amount; return true; break;
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        buildclass.Metal += Metal.amount;
        Metal.amount = 0;
        buildclass.Oxygen += Oxy.amount;
        Oxy.amount = 0;
        buildclass.Nitrogen += Nitro.amount;
        Nitro.amount = 0;
    }
}
