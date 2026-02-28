using UnityEngine;

public class conveyer : MonoBehaviour
{
    public GameObject Source;
    public GameObject Target;
    public float rate = 3.2f;
    public LineRenderer LineRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void Instance(GameObject srce, GameObject trg)
    {
        Source = srce;
        Target = trg;
    }
    // Update is called once per frame
    void Update()
    {
        if (Source!= null && Target != null)
        {
            LineRenderer.positionCount = 2;
            LineRenderer.SetPosition(0, Source.transform.position);
            LineRenderer.SetPosition(1, Target.transform.position);
        }
    }
}
