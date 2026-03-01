using TMPro;
using UnityEngine;

public class updateProduction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject gnode;
    string name;
    string mat;
    float input;
    float output;
    public TextMeshPro namey;
    public TextMeshPro maty;
    public TextMeshPro iny;
    public TextMeshPro outy;
    void Start()
    {
        
    }
    public void productionStats(GameObject node)
    {
        gnode = node;
    }
    // Update is called once per frame
    void Update()
    {
        if (gnode!= null)
        {
            gnode.GetComponent<node>().GetStats(out name, out mat, out input, out output);
            namey.text = name;
            maty.text = mat;
            iny.text = "input :" + input.ToString() + "/second";
            outy.text = "output :" + output.ToString() + "/second";
        }
    }
}
