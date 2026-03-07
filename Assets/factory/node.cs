using UnityEngine;

public class node : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public virtual void AddSpark(GameObject spark)
    {

    }
    public virtual void SubtractPower(out float finalpower , float availpower , bool firstT)
    {
        finalpower = availpower;
    }
    public virtual void ChangeActive( bool active)
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
    public virtual string GetName()
    {
       return "expedition 33";
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
