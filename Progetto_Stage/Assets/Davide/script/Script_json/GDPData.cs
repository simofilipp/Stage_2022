using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GDPData 
{
    public string Entity = "";
    public int Year;
    public double GDP;
}

[System.Serializable]
public class GDPDatas
{
    public GDPData[] dati;
}
