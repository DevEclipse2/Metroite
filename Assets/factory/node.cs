using System.Runtime.CompilerServices;
using UnityEngine;

public class node : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public virtual Element PullElement(Element element)
    {
        return null;
    }
    public virtual void AddElement( Element element)
    {

    }
    public virtual void GetName(out string name)
    {
        name = "expedition 33";
    }
    public virtual void ReadProduction(out string[] alternatives)
    {
        alternatives = null;
    }
    public virtual void ChangeProduction( int shift , out int final) 
    {
        final = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
