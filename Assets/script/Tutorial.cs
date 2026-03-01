using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource[] audioSources;
    public float[] Lengths;
    public bool[] auto;
    public int index = 0;
    float timer;
    bool playing;
    public GameObject lifeSupport;
    disaster disaster;
    void Start()
    {
        audioSources[index].Play();
        playing = true;
        disaster = lifeSupport.GetComponent<disaster>();
        disaster.ifFireHappeningRn = false;

    }
    public void Progress()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(index == -1)
        {
            return;
        }
        if(playing)
        {
            timer += Time.deltaTime;
        }
        if (timer > Lengths[index] && playing)
        {

            audioSources[index].Stop();
            index++;
            timer = 0;
            playing = false;
            if(index >= Lengths.Length)
            {
                disaster.ifFireHappeningRn = true;
                index = -1;
            }
            else
            {
                if (auto[index])
                {
                    playing = true;
                    audioSources[index].Play();

                }
            }
            
        }
    }
}
