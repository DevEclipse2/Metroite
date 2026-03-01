using UnityEngine;

public class disaster : MonoBehaviour
{
    public float oxygen;
    public float pressure;
    public float temperature;
    public float hullIntegrity;
    public bool depressValve;
    
    public float disasterCheckInterval;
    public float disasterChance;
    public float disasterTimer;
    public float baseDepressRate;

    public bool disasterTrigger;

    public bool ifBreathableAtmo = true;
    public bool ifLethalAtmo = false;
    public float breathableAtmoLimit;
    public float lethalAtmoLimit;

    public Transform  ScreenSpawner;
    public GameObject OxygenAlert;
    public GameObject PressureAlert;
    public GameObject FireAlert;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oxygen = 0.21f; //in percentage 0-1
        pressure = 1013.25f; //in hectopascals(hPa)
        temperature = 25f; //in degrees Celsius
        hullIntegrity = 1f; //in percentage 0-1 
        depressValve = false;

        //edit below to tweak difficulty
        disasterChance = 0.3f;// 0 to 1
        disasterCheckInterval = 15f;//in seconds
        baseDepressRate = 5f;//In hPa lost per second

        breathableAtmoLimit = 200f;//hPa
        lethalAtmoLimit = 50f;//hPa





        disasterTimer = disasterCheckInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (disasterTimer > 0 ) {
            disasterTimer -= Time.deltaTime;
        }
        if (disasterTimer <= 0) {
            if (Random.RandomRange(0,1) <= disasterChance) {
                disasterTrigger = true;
            }
            disasterTimer = disasterCheckInterval;
        }

        if (disasterTrigger = true) {

        }

        pressure -= (baseDepressRate * Time.deltaTime);

    }
}
 