using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HealthData 
{
    public string City = "";
    public string costOfLivingIndex = "";

    public float LivingIndexClear()
    {
        string[] splittedValue = costOfLivingIndex.Split(".");
        string nuovoValore = splittedValue[0] + "." + splittedValue[1];
        float valore = float.Parse(nuovoValore);
        return valore;
    }
    public string GetCity()
    {
        string[] splittedcity = City.Split(",");
        return splittedcity[0];
    }

    public string PrintAllData()
    {
        string dati = "city: " + GetCity() + "\nindex: " + LivingIndexClear();
        return dati;
    }
}

[System.Serializable]

public class HealthDatas
{
    public HealthData[] dati;
}
