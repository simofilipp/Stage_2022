using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class Istanzia_istogrammi : MonoBehaviour
{

    public JsonData jdata;
    public GameObject cubo_scala;
    [SerializeField]
    GameObject parent_punti;
    [SerializeField]
    GameObject terraInterazioni;
    
    

    ShowedData datoMostrato;
    float raggio;
    bool scalando=false;

    public List<GameObject> punti;
    List<float> dataValues;

    // Start is called before the first frame update
    private void Awake()
    {
    
    }
    void Start()
    {
        dataValues = new List<float>();
        datoMostrato = ShowedData.None;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(terraInterazioni.transform.localScale.x);
    }

    Vector3 CalcolaPunto(float lat, float lng)
    {
        Vector3 pos;
        pos.x = raggio * Mathf.Cos((lng) * Mathf.Deg2Rad) * Mathf.Cos(lat * Mathf.Deg2Rad);
        pos.y =  raggio * Mathf.Sin(lat * Mathf.Deg2Rad);
        pos.z = raggio * Mathf.Sin((lng) * Mathf.Deg2Rad) * Mathf.Cos(lat * Mathf.Deg2Rad);

        return pos;
    }


    public void IstanziaBaseIstogrammi()
    {
        raggio = terraInterazioni.transform.localScale.x / 2;
        foreach (var dato in jdata.gameData.dati)
        {
            if (dato.capital == "primary")
            {
                Debug.Log("Entrato nel foreach di idtanzia...");
                var punto = Instantiate(cubo_scala, CalcolaPunto(dato.lat, dato.lng), Quaternion.identity);
                punto.transform.parent = parent_punti.transform;
                punto.transform.LookAt(punto.transform.position * 2);
                punti.Add(punto);
                punto.name = dato.city;
                //mettere a 0 la scala se si vedono
                punto.transform.localScale = new Vector3(punto.transform.localScale.x, punto.transform.localScale.y, 0);
            }
        }
        parent_punti.transform.position = this.transform.position;
        parent_punti.transform.parent = this.transform;
        parent_punti.transform.localRotation = Quaternion.Euler(0.09f, -90f, -90);
        parent_punti.SetActive(false);
    }

    public void ScalaConDati(int i)
    {
        if (!scalando)
        {
            switch (i)
            {
                //Popolazione
                case 0:
                    if (datoMostrato != ShowedData.CapitalPopulation)
                    {
                        scalando = true;
                        parent_punti.SetActive(true);
                        datoMostrato = ShowedData.CapitalPopulation;
                        dataValues.Clear();
                        foreach (var dato in jdata.gameData.dati)
                        {
                            if (dato.capital == "primary")
                            {
                                dataValues.Add(dato.population);
                            }
                        }
                        for (int j = 0; j < dataValues.Count; j++)
                        {
                            //scala in base al dato, le liste devono essere lunghe uguali e precise
                            var scalaFinale = new Vector3(punti[j].transform.localScale.x, punti[j].transform.localScale.y, dataValues[j] / 100000);
                            punti[j].transform.LeanScale(scalaFinale, 5f);
                            punti[j].GetComponentInChildren<TMP_Text>().text = dataValues[j].ToString();
                            punti[j].GetComponent<MeshRenderer>().material.color = Color.green;
                            punti[j].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.green);
                            if (dataValues[j] > 1000000)
                            {

                                punti[j].GetComponent<MeshRenderer>().material.color = Color.yellow;
                                punti[j].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.yellow);
                            }
                            if (dataValues[j] > 5000000)
                            {

                                punti[j].GetComponent<MeshRenderer>().material.color = Color.red;
                                punti[j].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);
                            }
                            if (dataValues[j] > 25000000)
                            {
                                punti[j].GetComponent<MeshRenderer>().material.color = Color.white;
                                punti[j].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white);
                            }
                        }
                        StartCoroutine(SetScalandoFalse());
                    }
                    else
                    {
                        scalando = true;
                        //se ho già visualizzato i dati li scalo a 0
                        foreach (var punto in punti)
                        {
                            var scalaZero = new Vector3(punto.transform.localScale.x, punto.transform.localScale.y, 0f);
                            punto.transform.LeanScale(scalaZero, 5f).setOnComplete(() => { parent_punti.SetActive(false); });
                        }
                        datoMostrato = ShowedData.None;
                        StartCoroutine(SetScalandoFalse());
                    }
                    break;
                case 1:
                    if (datoMostrato != ShowedData.Latitude)
                    {
                        scalando = true;
                        parent_punti.SetActive(true);
                        datoMostrato = ShowedData.Latitude;
                        dataValues.Clear();
                        foreach (var dato in jdata.gameData.dati)
                        {
                            if (dato.capital == "primary")
                            {
                                dataValues.Add(Mathf.Abs(dato.lat));
                            }
                        }
                        for (int j = 0; j < dataValues.Count; j++)
                        {
                            //scala in base al dato, le liste devono essere lunghe uguali e precise
                            var scalaFinale = new Vector3(punti[j].transform.localScale.x, punti[j].transform.localScale.y, dataValues[j]);
                            punti[j].transform.LeanScale(scalaFinale, 5f);
                            punti[j].GetComponent<MeshRenderer>().material.color = Color.green;
                            punti[j].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.green);
                            if (dataValues[j] > 30)
                            {

                                punti[j].GetComponent<MeshRenderer>().material.color = Color.yellow;
                                punti[j].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.yellow);
                            }
                            if (dataValues[j] > 45)
                            {

                                punti[j].GetComponent<MeshRenderer>().material.color = Color.red;
                                punti[j].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);
                            }
                            if (dataValues[j] > 70)
                            {

                                punti[j].GetComponent<MeshRenderer>().material.color = Color.white;
                                punti[j].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white);
                            }
                        }
                        StartCoroutine(SetScalandoFalse());
                    }
                    else
                    {
                        scalando = true;
                        //se ho già visualizzato i dati li scalo a 0
                        foreach (var punto in punti)
                        {
                            var scalaZero = new Vector3(punto.transform.localScale.x, punto.transform.localScale.y, 0f);
                            punto.transform.LeanScale(scalaZero, 5f).setOnComplete(() => { parent_punti.SetActive(false); });
                        }
                        datoMostrato = ShowedData.None;
                        StartCoroutine(SetScalandoFalse());
                    }
                    break;
            }
        }
    }
    IEnumerator SetScalandoFalse()
    {
        yield return new WaitForSeconds(5.2f);
        scalando = false;
    }

    public void ColoraStati()
    {
        foreach(var dato in jdata.gameData.dati)
        {
            if (dato.capital == "primary")
            {
                foreach(var stato in WorldMapManager.instance.countries)
                {
                    if (dato.iso2 == stato.gameObject.name)
                    {
                        stato.ColorCountry= new Color(1,1f-(float)Math.Round(dato.population/ 39105000f,2), 1f-(float)Math.Round(dato.population / 39105000f, 2));
                        stato.ChangeColor();
                        //break;
                    }
                }
            }
        }
    }

}


public enum ShowedData{
    None,
    CapitalPopulation,
    Latitude
}

