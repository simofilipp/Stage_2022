using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonData : MonoBehaviour
{
    // Start is called before the first frame update

    string filename = "WorldCities";
    string percorso;
    public TextAsset dataFile;

    public GameDatas gameData = new GameDatas();

    private void Awake()
    {
        ReadData();
    }
    void Start()
    {
        percorso = Application.persistentDataPath + "/" + filename;
        Debug.Log(percorso);
        

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ReadData()
    {
        gameData = JsonUtility.FromJson<GameDatas>(dataFile.text);

        foreach(var dato in gameData.dati)
        {
            Debug.Log(dato.PrintAllData());
        }
        
    }
}
