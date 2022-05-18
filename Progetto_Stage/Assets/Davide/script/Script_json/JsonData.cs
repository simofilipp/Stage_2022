using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonData : MonoBehaviour
{
    // Start is called before the first frame update

    //string filename = "WorldCities";
    //string percorso;
    public TextAsset dataFile;
    public TextAsset dataHealth;
    public GameDatas gameData = new GameDatas();
    //public HealthDatas healthDatas = new HealthDatas();

    private void Awake()
    {
        ReadData();
    }
    void Start()
    {
        //percorso = Application.persistentDataPath + "/" + filename;
        //Debug.Log(percorso);
        

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ReadData()
    {
        gameData = JsonUtility.FromJson<GameDatas>(dataFile.text);
        //healthDatas = JsonUtility.FromJson<HealthDatas>(dataHealth.text);

        //foreach (var dato in healthDatas.dati)
        //{
        //    Debug.Log(dato.PrintAllData());
        //}

    }
}
