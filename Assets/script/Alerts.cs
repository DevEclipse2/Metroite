using UnityEngine;

public class Alerts : MonoBehaviour
{
    Vitals Vitals;
    disaster disaster;
    public Transform playerFace;

    public GameObject PrefabOxyWarnLow;
    public GameObject PrefabOxyWarnLethal;
    public GameObject PrefabOxyWarnHigh;
    public GameObject PrefabOxyWarnExplosion;
    public GameObject PrefabPressureWarnLowDeath;
    public GameObject PrefabPressureWarnLowBreathe;
    public GameObject PrefabPressureWarnHigh;
    public GameObject PrefabFireWarn;
    public GameObject PrefabSmokeWarn;
    public GameObject PrefabPowerWarn;
    public float AlertTimer = 0.8f;
    public float AlertTimerL = 1.2f;
    float AlertTime;

    bool fireAlert;
    bool pressureAlert;
    bool oxyAlert;
    bool powerAlert;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vitals = GetComponent<Vitals>();
        disaster = GetComponent<disaster>();
        if(Vitals == null || playerFace == null)
        {
            Debug.LogError("No vitals attached");
        }
    }

    // Update is called once per frame
    void Update()
    {
        fireAlert = disaster.ifFireHappeningRn;
        pressureAlert = (Vitals.pstat != 0);
        oxyAlert = (Vitals.oxystat != 0);
        if(fireAlert)
        {
            GameObject fire = Instantiate(PrefabFireWarn, playerFace.position, playerFace.rotation);
        }
        else if (pressureAlert)
        {

        }
        else if (oxyAlert)
        {

        }
    }
}
