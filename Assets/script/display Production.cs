using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class displayProduction : MonoBehaviour
{
    public string[] displaytext = new string[2]{
    "",
    ""
    };
    TextMeshProUGUI tmp;
    string supertext;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        supertext = "";
        if (displaytext.Length > 0)
        {
            foreach (string s in displaytext)
            {
                supertext += s + "<br>";
            }
            tmp.text = supertext;
        }
    }
}
