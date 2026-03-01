using UnityEngine;

public class valve : Button
{
    public GameObject lifeSupport;
    disaster disaster;
    public AudioSource source;
    public AudioSource source2;
    float timer;
    public override void Press()
    {
        if (disaster.depressValveOpen)
        {
            if (!source.isPlaying) { 
                source.Play();
                source2.Stop();
            }
        }
        else
        {
            if (!source2.isPlaying)
            {
                source2.Play();
                source.Stop();
            }
        }
        
        
        disaster.depressValveOpen = !disaster.depressValveOpen;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        disaster = lifeSupport.GetComponent<disaster>();

    }

    // Update is called once per frame
    void Update()
    {
    }
}
