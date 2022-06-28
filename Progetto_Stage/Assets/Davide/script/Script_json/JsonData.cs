using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonData : MonoBehaviour
{
 
    public TextAsset dataFile;
    public TextAsset dataGDP;
    public GameDatas gameData = new GameDatas();
    public GDPDatas gameDataGDP = new GDPDatas();

    private void Awake()
    {
        ReadData();
        //AggiungiGDP();
        //WriteData();
        //Debug.Log(Application.dataPath);
    
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ReadData()
    {
        gameData = JsonUtility.FromJson<GameDatas>(dataFile.text);
        //gameDataGDP = JsonUtility.FromJson<GDPDatas>(dataGDP.text);

    }

    public void WriteData()
    {
        string output=JsonUtility.ToJson(gameData);

        File.WriteAllText(Application.dataPath + "/newJSON.json", output);
    }

    //void AggiungiGDP()
    //{
    //    foreach(var dato in gameDataGDP.dati)
    //    {
    //        if(dato.Year == 2019)
    //        {
    //            foreach(var datoWC in gameData.dati)
    //            {
    //                if(datoWC.capital=="primary" && datoWC.country == dato.Entity)
    //                {
    //                    datoWC.gdp = dato.GDP;
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //}
}
