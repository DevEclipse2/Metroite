using System.Runtime.CompilerServices;
using UnityEngine;

public class node : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public virtual void GetStats(out string name, out string mat , out float inputs, out float outputs)
    {
        name = "";
        mat = "";
        inputs = 0;
        outputs = 0;
    }
    public virtual Element PullElement(float amount)
    {
        return null;
    }
    public virtual bool AddElement( Element element)
    {
        return false;
    }
    public virtual void ForceElement(Element element)
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
