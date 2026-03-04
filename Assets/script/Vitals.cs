using UnityEngine;

public class Vitals : MonoBehaviour
{

    public float R = 8.31f;
    public float Pressure;
    public float DepressurizationRate = 8;
    public float AdditionalDepressurizationRate = 12;
    public float Temperature;
    public float OxygenC;
    public float NitrogenMols;
    public float OxygenMols;
    public float HullInt;
    const float PodVolume = 65000; // in litres
    const float pressureSTP = 100; // in kPa
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pressure = pressureSTP;
        Temperature = temperatureSTP;
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
    void UpdatePressure()
    {
        // P = nRt/V
        Pressure = (NitrogenMols + OxygenMols) * R * Temperature / PodVolume;
        float DeltaP = CalculateDepressRate() * Time.deltaTime; // change in pressure

        // calculate loss of oxygen and nitrogen

        OxygenC = CalculateOxyPercent(NitrogenMols, OxygenMols);
        switch (CheckOxy(OxygenC))
        {
        
        
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdatePressure();
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
