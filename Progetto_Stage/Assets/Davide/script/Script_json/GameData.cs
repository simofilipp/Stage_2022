using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData 
{
    public string city = "";
    public float lat;
    public float lng;
    public string country = "";
    public string iso2 = "";
    public string capital = "";
    public long population;
    public double gdp;
    

    public string PrintAllData()
    {
        string dati = "city: " + city + "\ncountry: " + country + "\nlat: " + lat.ToString() + "\nlng: " + lng.ToString() + "\ncode: " + iso2 + "\ncapital: " + capital + "\ngdp: " + gdp;
        return dati;
    }

}

[System.Serializable]

public class GameDatas
{
    public GameData[] dati;
   
}




