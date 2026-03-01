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
    void Start()
    {
        audioSources[index].Play();
        playing = true;
    }
    public void Progress()
    {

    }
    // Update is called once per frame
    void Update()
    {
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
            if (auto[index])
            {
                playing = true;
                audioSources[index].Play();

            }
        }
    }
}
