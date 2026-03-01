using UnityEngine;

public class disaster : MonoBehaviour
{
    public float Oxygen;
    public float Nitrogen;
    public float Temp;
    public float BreathLimitP = 0.6f;
    public float LiveLimitP = 0.25f;
    public bool oAlert;
    public bool pAlert;
    public bool tAlert;
    public float BreatherMaxTime = 36f;
    public float BreatherTimer = 0;
    public float OpenMaxTime = 12f;
    public float OpenTimer = 0;
    public int RemainingRespirators = 8;
    float Integrity = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
