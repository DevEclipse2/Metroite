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
    public float baseOxyConsumRate;
    public float damageDepressMultiplier;

    public bool disasterTrigger;

    //say check for these values to display on screens
    public bool ifBreathableAtmo = true;
    public bool ifLethalAtmo = false;
    public float breathablePressLimit;
    public float lethalPressLimit;
    public float breathableOxyLimit;

    public float scbaRemaining;
    public float scbaUseTime;
    public float scbaRefillRate;
    public bool scbaInUse;

    //check for these to trigger game-overs
    public bool gameOverOxy;
    public bool gameOverDepress;



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

        breathablePressLimit = 200f;//hPa
        lethalPressLimit = 50f;//hPa
        breathableOxyLimit = 0.05f;//percentage 0-1

        scbaUseTime = 60f;//seconds
        scbaRefillRate = 2f;//ratio against use rate 





        disasterTimer = disasterCheckInterval;
    }

    // Update is called once per frame
    void Update()
    {
        //base depress and O2 consum.
        pressure -= (baseDepressRate * Time.deltaTime);
        oxygen -= (baseOxyConsumRate * Time.deltaTime);

        //damage-caused depress.
        pressure -= (damageDepressMultiplier * (hullIntegrity - 1) * Time.deltaTime);

        
        if (disasterTimer > 0 ) {
            disasterTimer -= Time.deltaTime;
        }
        if (disasterTimer <= 0) {
            float disasterRand = Random.Range(0, 1);
            if (disasterRand <= disasterChance) {
                disasterTrigger = true;
            }
            disasterTimer = disasterCheckInterval;
        }

        if (disasterTrigger = true) {
            //call for a random disaster to happen here
        }

        pressure -= (baseDepressRate * Time.deltaTime);

        
        //checks for atmosphere survivability
        if (pressure <= lethalPressLimit) {
            gameOverDepress = true;
        }
        else if ((oxygen <= breathableOxyLimit) || (pressure <= breathablePressLimit)) {
            ifBreathableAtmo = false;
        }
        else {
            ifBreathableAtmo = true;
        }

        //SCBA (Self-contained Breathing Apperatus) (basically emergency air tank) behaviour
        if (scbaRemaining <= 0) {
            gameOverOxy = true;
        }
        if (ifBreathableAtmo == true) {
            scbaInUse = true;
            scbaRemaining -= (Time.deltaTime / scbaUseTime);
        }
        else {
            scbaRemaining += (scbaRefillRate * (Time.deltaTime * scbaRefillRate));
        }

    }
}
 