using Unity.VisualScripting;
using UnityEngine;

public class conveyer : MonoBehaviour
{
    public GameObject Source;
    public GameObject Target;
    Element bufferelement;
    public float rate = 3.2f;
    public LineRenderer LineRenderer;
    LineRenderer bridgeMaterial;
    Material mat;
    private Color target;
    private Color current;
    public Color[] materialColors;
    public Color ErrorColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bridgeMaterial = GetComponent<LineRenderer>();
        mat = new Material(bridgeMaterial.material);
        bridgeMaterial.material = mat;
    }
    public void Instance(GameObject srce, GameObject trg)
    {
        Source = srce;
        Target = trg;
        current = Color.white;
    }
    void ChangeColor()
    {
        target = materialColors[bufferelement.element];
        mat.SetColor("Color", target);
        mat.SetColor("_Color",target);
        bridgeMaterial.material = mat;
    }
    // Update is called once per frame
    void Update()
    {
        if (Source!= null && Target != null)
        {
            bufferelement = null;
            LineRenderer.positionCount = 2;
            LineRenderer.SetPosition(0, Source.transform.position);
            LineRenderer.SetPosition(1, Target.transform.position);
            bufferelement = Source.GetComponent<node>().PullElement(0);
            if (bufferelement != null )
            {
                //Debug.Log(Target.GetComponent<node>());
                if (Target.GetComponent<node>().AddElement(bufferelement))
                {
                    bufferelement = Source.GetComponent<node>().PullElement(rate * Time.deltaTime);
                    Target.GetComponent<node>().AddElement(bufferelement);
                    ChangeColor();
                }
                else
                {
                    target = ErrorColor;
                }
            }
        }
        
    }
}
