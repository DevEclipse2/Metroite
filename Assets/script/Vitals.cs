using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Vitals : MonoBehaviour
{

    public float R = 8.31f;
    public float Pressure;
    public float DepressurizationRate = 8;
    public float AdditionalDepressurizationRate = 12;
    public float Temperature;
    public float OxygenC;
    public float NitrogenMols = 2234;
    public float OxygenMols = 660;
    public float HullInt;
    const float PodVolume = 65000; // in litres
    const float pressureSTP = 120; // in kPa
    const float temperatureSTP = 293.15f; // in k
    public float BreathVol = 0.350f;
    public float[] OxyCLevels = new float[5]
    {
        0.1f,
        0.15f,
        0.21f,
        0.235f,
        0.5f
    };
    public float[] OxyMLevels = new float[5]
    {
        0,
        0,
        0,
        0,
        0,
    };
    public float[] Plevels = new float[5]
    {
        45f,
        60f,
        100f,
        160f,
        220f,

    };
    public int oxystat;
    public int pstat;
    public bool Fire;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pressure = pressureSTP;
        Temperature = temperatureSTP;
        //pv = nrt , n = pv/rt
        NitrogenMols = Pressure * PodVolume / R / Temperature * 0.79f;
        OxygenMols = Pressure * PodVolume / R / Temperature * 0.21f;
        for(int i = 0; i < 5; i++)
        {
            OxyMLevels[i] = CalculateMols(OxyCLevels[i], pressureSTP , temperatureSTP);
        }
    }
    float CalculateDepressRate()
    {
        return DepressurizationRate + AdditionalDepressurizationRate * (Pressure / pressureSTP) * (1 - HullInt);
            //pressure ^ = rate ^ 
            // hull integrity v = rate ^
        return 12;
    }
    public void OpenAirlock()
    {
        float DeltaP = CalculateDepressRate() * Time.deltaTime * 8; // change in pressure
    }
    void UpdatePressure()
    {
        // P = nRt/V
        Pressure = (NitrogenMols + OxygenMols) * R * Temperature / PodVolume;
        float DeltaP = CalculateDepressRate() * Time.deltaTime; // change in pressure
        //pv = nrt
        float MolarLoss = DeltaP * PodVolume / Temperature / R;
        OxygenMols -= MolarLoss * (OxygenC);
        NitrogenMols -= MolarLoss * (1 - OxygenC);
        Pressure -= DeltaP;
        // calculate loss of oxygen and nitrogen

        OxygenC = CalculateOxyPercent(NitrogenMols, OxygenMols);
        switch (oxystat = CheckOxy(OxygenC))
        {
            case -2:
                // very low , blackout in 12seconds
                break;
            case -1:
                //low , fast movement results in blackout
                break;
            case 0:
                break;
            case 1:
                //fire chance ++
                break;
            case 2:
                //explosion chance ++
                break;
            default:
                break;
        
        }
        switch (pstat = CheckP(Pressure))
        {
            case -2:
                // very low , last warning
                break;
            case -1:
                //low , fast movement results in blackout
                break;
            case 0:
                break;
            case 1:
                //fire intensity ++
                break;
            case 2:
                //fire chance ++
                break;
            default:
                break;

        }
    }

    public void AddPressure( float N , float O)
    {
        //Debug.Log("Add" + N + "oxy"+ O);
        NitrogenMols += N * 60;
        OxygenMols += O   * 40;
    }
    void UpdateFire()
    {
        if (Fire)
        {
            //fire consumes oxygen
            // if pressure drops below 70 or oxy below 16 , fire extinguishes automatically
            //fire increases temperature
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdatePressure();
        UpdateFire();
    }
    int CheckP(float pres)
    {
        int value = -2;
        for (int i = 0; i < 5; i++)
        {
            if (pres > Plevels[i])
            {
                value++;
            }
            else
            {
                break;
            }
        }
        return value;
    }
    int CheckOxy(float oxyC)
    {
        // 2 v high  (instant game over once fire starts)
        // 1 high
        // 0 normal
        // -1 low 
        // -2 v low 
        float Ovol = BreathVol * oxyC;
        float Omol = CalculateMols(oxyC, Pressure, Temperature) * Ovol;
        int value = -2;
        for(int i = 0; i < 5; i++)
        {
            if(Omol > OxyMLevels[i])
            {
                value++;
            }
            else
            {
                break;
            }
        }
        return value;
    }
    float CalculateOxyPercent(float NitroM , float OxyM )
    {
        return OxyM / (NitroM + OxyM);
    }
    float CalculateMols( float concentration, float pressure, float temp)
    {
        float mols = 0;
        mols = pressure / (R * temp);
        mols *= concentration;
        return mols;
        //pv = nrt // solve for n
    }
}
