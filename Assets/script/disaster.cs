using UnityEngine;
using UnityEngine.SceneManagement;
public class disaster : MonoBehaviour
{
    //the var names should be self-explainatory enough right??

    public float oxygen;
    public float pressure;
    public float temperature;
    public float hullIntegrity;
    public bool depressValveOpen = false;

    public float normOxygen = 0.21f;
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

    public GameObject FireContainer;
    public GameObject screen;
    public Transform screenpos;
    public Vitals vitals;
    float spawn;
    public void Increment(float value, float oxy)
    {
        oxygen += oxy;
        pressure += (value + oxy) * 500;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vitals = GetComponent<Vitals>();
        //norm. and max. values of vital stuff
        oxygen = normOxygen = 0.21f; //in percenge 0-1
        pressure = normPressure = 1013.25f; //in hectopascals(hPa)
        temperature = normTemperature = 25f; //in degrees Celsius
        hullIntegrity = normHullIntegrity = 1f; //in percentage 0-1 

        //edit below to tweak difficulty




        disasterTimer = disasterCheckInterval;
    }
    public void GameOver()
    {

    }
    void Depress()
    {

    }
    void OpenDoor()
    {

    }
    // Update is called once per frame
    void Update()
    {
        /*
        if (gameOverDepress || gameOverOxy)
        {
            SceneManager.LoadScene("title");

        }
        if (ifFireHappeningRn)
        {
            spawn += Time.deltaTime;
            if(spawn > 0.8f)
            {
                Instantiate(screen, screenpos.position , screenpos.rotation);
                spawn = 0;
            }
        }
        if(depressValveOpen)
        {
            pressure -= 400 * Time.deltaTime; 
            ifFireHappeningRn = false;
        }
        //base depress and O2 consum.
        pressure -= (baseDepressRate * Time.deltaTime);
        oxygen -= (baseOxyConsumRate * Time.deltaTime);

        //damage-caused depress.
        pressure -= (damageDepressMultiplier * (hullIntegrity - 1) * Time.deltaTime);

        if (disasterTimer > 0)
        {
            disasterTimer -= Time.deltaTime;
        }
        if (disasterTimer <= 0)
        {
            float disasterRand = Random.Range(0, 1);
            if (disasterRand <= disasterChance)
            {
                disasterTrigger = true;
            }
            disasterTimer = disasterCheckInterval;
        }

        if (disasterTrigger = true)
        {
            //call for a random disaster to happen here

        }



        //checks for atmosphere survivability
        if (pressure <= lethalPressLimit)
        {
            gameOverDepress = true;
        }
        else if ((oxygen <= breathableOxyLimit) || (pressure <= breathablePressLimit))
        {
            ifBreathableAtmo = false;
        }
        else
        {
            ifBreathableAtmo = true;
        }

        //SCBA (Self-contained Breathing Apperatus) (basically emergency air tank) behaviour
        if (scbaRemaining <= 0)
        {
            gameOverOxy = true;
        }
        if (ifBreathableAtmo == true)
        {
            scbaInUse = true;
            scbaRemaining -= (Time.deltaTime / scbaUseTime);
        }
        else
        {
            scbaRemaining += ((Time.deltaTime * scbaRefillRate) / scbaUseTime);
        }



        FireContainer.SetActive(ifFireHappeningRn);

        //fire




        //band-aid max value limiters
        if (oxygen >= normOxygen)
        {
            oxygen = normOxygen;
        }
        if (pressure >= normPressure)
        {
            pressure = normPressure;
        }
        if (hullIntegrity >= normHullIntegrity)
        {
            hullIntegrity = normHullIntegrity;
        }
        Mathf.Clamp01(scbaRemaining);


        */
        FireContainer.SetActive(ifFireHappeningRn);
        vitals.Fire = ifFireHappeningRn;
        if (depressValveOpen)
        {
            vitals.OpenAirlock();
            ifFireHappeningRn = false;
        }

    }
}
