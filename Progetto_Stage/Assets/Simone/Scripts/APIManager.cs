using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RestSharp;
using UnityEngine.Networking;
//using SimpleJSON;
using System.Text;
using System;
using TMPro;

public class APIManager : MonoBehaviour
{
    [SerializeField] TMP_Text textCovid;
    [SerializeField] TMP_Text countryInput;
    [SerializeField] EarthRotationManager earthRotationManager;

    string countryName = "";
    string _URL;

    CountryScript _countryScript;
    DatiGiornalieri yesterdayData;
    DatiGiornalieri todayData;
    List<DatiGiornalieri> dataList;

    int listCounter = 0;
    bool validData = false;
    public bool turn = false;

    DateTime today;
    DateTime yesterday;
    DateTime twoDaysAgo;

    public static APIManager _instance;
    public static APIManager instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<APIManager>();
            }
            return _instance;
        }
        set { _instance = value; }
    }

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) { Destroy(gameObject); return; };
        dataList = new List<DatiGiornalieri>();
        dataList.Add(yesterdayData);
        dataList.Add(todayData);
    }

    private void Start()
    {
        today = DateTime.Now;
        yesterday = today.AddDays(-2);
        twoDaysAgo = today.AddDays(-3);

        WorldMapManager.instance.CurrentState = WorldMapManager.State.Politic;
    }

    void Update()
    {

    }

    IEnumerator GetData()
    {

        var client = new RestClient(_URL);
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        IRestResponse response = client.Execute(request);
        Debug.Log(response.Content);
        if (response.Content != "{\"message\":\"Not Found\"}")
        {
            //Pulizia messaggio in arrivo
            response.Content = response.Content.Replace("{", "").Replace("}", "").Replace("\"", "").Trim('[', ']');
            string[] arrayRisposta = response.Content.Split(",");

            string nameValue = "";
            string codeValue = "";
            int casesValue;
            //Filtro per i valori che mi servono, salvo i valori in base alle giornate
            for (int i = 0; i < arrayRisposta.Length; i++)
            {
                string _field = arrayRisposta[i].Split(':')[0].ToLower();
                string _value = arrayRisposta[i].Split(':')[1];

                switch (_field)
                {
                    case "country":
                        nameValue = _value;
                        break;
                    case "countrycode":
                        codeValue = _value;
                        break;
                    case "province":
                        if (_value == "")
                        {
                            validData = true;
                        }
                        break;
                    case "cases":
                        //lo stato è valido solo se non presenta alcun valore nel field Province
                        if (validData)
                        {
                            dataList[listCounter] = new DatiGiornalieri(nameValue);
                            dataList[listCounter].SetCode(codeValue);
                            casesValue = int.Parse(_value);
                            dataList[listCounter].SetDailyCases(casesValue);
                            validData = false;
                            listCounter += 1;
                        }
                        break;
                }

            }
            //salvo i valori utili dello stato in questione
            _countryScript = new CountryScript(
            dataList[0].GetName(),
            dataList[0].GetCode(),
            dataList[0].GetDailyCases(),
            dataList[1].GetDailyCases(),
            dataList[1].GetDailyCases() - dataList[0].GetDailyCases()
            );



            Debug.Log(_countryScript.PrintAllData());
            textCovid.text = _countryScript.PrintAllData();

            if (turn)
            {
                earthRotationManager.RuotaTerra(_countryScript.GetCode());
            }

        }
        else
        {
            Debug.Log("Incomprensione");
        }
        yield return null;
    }

    public void GetCountryData()
    {
        //prendo ciò che è stato digitato e sostituisco gli spazi con un trattino
        countryName = countryInput.text;
        countryName = countryName.Trim().Replace(' ', '-');


        //inserisco lo stato e le date degli ultimi 2 giorni
        if (countryName != "")
        {
            //_URL = "https://api.covid19api.com/country/" + countryName.ToLower() + "/status/confirmed?from=" + yesterday.Year + "-" + yesterday.Month + "-" + yesterday.Day + "T00:00:00Z&to=" + today.Year + "-" + today.Month + "-" + today.Day + "T" + today.Hour + ":" + today.Minute + ":" + today.Second + "Z";
            _URL = "https://api.covid19api.com/country/" + countryName.ToLower() + "/status/confirmed?from=" + twoDaysAgo.Year + "-" + twoDaysAgo.Month + "-" + twoDaysAgo.Day + "T00:00:00Z&to=" + yesterday.Year + "-" + yesterday.Month + "-" + yesterday.Day + "T00:00:00Z";
        }
        listCounter = 0;
        StartCoroutine(GetData());
    }

    public void GetCountryData(string name)
    {
        if (name != "")
        {
            //_URL = "https://api.covid19api.com/country/" + countryName.ToLower() + "/status/confirmed?from=" + yesterday.Year + "-" + yesterday.Month + "-" + yesterday.Day + "T00:00:00Z&to=" + today.Year + "-" + today.Month + "-" + today.Day + "T" + today.Hour + ":" + today.Minute + ":" + today.Second + "Z";
            _URL = "https://api.covid19api.com/country/" + name.ToLower() + "/status/confirmed?from=" + twoDaysAgo.Year + "-" + twoDaysAgo.Month + "-" + twoDaysAgo.Day + "T00:00:00Z&to=" + yesterday.Year + "-" + yesterday.Month + "-" + yesterday.Day + "T00:00:00Z";
        }
        listCounter = 0;
        StartCoroutine(GetData());
    }

    //string url= "https://api.covid19api.com/country/italy/status/confirmed?from=2022-5-3T00:00:00Z&to=2022-5-4T00:00:00Z"
}
