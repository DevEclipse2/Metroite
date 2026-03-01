using UnityEngine;

public class disaster : MonoBehaviour
{
    //the var names should be self-explainatory enough right??

    public float oxygen;
    public float pressure;
    public float temperature;
    public float hullIntegrity;
    public bool depressValveOpen = false;

    public float normOxygen;
    public float normPressure;
    public float normTemperature;
    public float normHullIntegrity;

    public float depressValveRelRate;

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

    public bool ifFireHappeningRn = false;

    //check for these to trigger game-overs
    public bool gameOverOxy;
    public bool gameOverDepress;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //norm. and max. values of vital stuff
        oxygen = normOxygen;// = 0.21f; //in percenge 0-1
        pressure = normPressure;// = 1013.25f; //in hectopascals(hPa)
        temperature = normTemperature;// = 25f; //in degrees Celsius
        hullIntegrity = normHullIntegrity;// = 1f; //in percentage 0-1 

        //edit below to tweak difficulty
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        //disasterChance;// = 0.3f;// 0 to 1
        //disasterCheckInterval;// = 15f;//in seconds
        //baseDepressRate;// = 5f;//In hPa lost per second

        //breathablePressLimit;// = 200f;//hPa
        //lethalPressLimit;// = 50f;//hPa
        //breathableOxyLimit;// = 0.05f;//percentage 0-1

        //scbaUseTime;// = 60f;//seconds
        //scbaRefillRate;// = 2f;//ratio against use rate 
=======
        disasterChance;// = 0.3f;// 0 to 1
        disasterCheckInterval;// = 15f;//in seconds
        baseDepressRate;// = 5f;//In hPa lost per second

        breathablePressLimit;// = 200f;//hPa
        lethalPressLimit;// = 50f;//hPa
        breathableOxyLimit;// = 0.05f;//percentage 0-1

        scbaUseTime;// = 60f;//seconds
        scbaRefillRate;// = 2f;//ratio against use rate 
>>>>>>> Stashed changes
=======
        disasterChance;// = 0.3f;// 0 to 1
        disasterCheckInterval;// = 15f;//in seconds
        baseDepressRate;// = 5f;//In hPa lost per second

        breathablePressLimit;// = 200f;//hPa
        lethalPressLimit;// = 50f;//hPa
        breathableOxyLimit;// = 0.05f;//percentage 0-1

        scbaUseTime;// = 60f;//seconds
        scbaRefillRate;// = 2f;//ratio against use rate 
>>>>>>> Stashed changes





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
            scbaRemaining += ((Time.deltaTime * scbaRefillRate) / scbaUseTime);
        }




        //fire
        if (ifFireHappeningRn = true) {
            
        }
        if (ifFireHappeningRn = true) {
            object.DisasterEventFires.Enable = true;
        }
        else
        {
            object.DisasterEventFires.Enable = false;
        }



        //band-aid max value limiters
        if ( oxygen >= normOxygen )
        {
            oxygen = normOxygen;
        }
        if ( pressure >= normPressure )
        {
            pressure = normPressure;
        }
        if ( hullIntegrity >= normHullIntegrity )
        {
            hullIntegrity = normHullIntegrity;
        }
        Mathf.Clamp01(scbaRemaining);



    }
}
 