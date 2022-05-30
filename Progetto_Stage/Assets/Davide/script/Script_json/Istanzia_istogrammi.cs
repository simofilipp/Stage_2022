using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Istanzia_istogrammi : MonoBehaviour
{

    public JsonData jdata;
    public GameObject cubo_scala;
    [SerializeField]
    GameObject parent_punti;
    [SerializeField] float raggio;

    bool datiAttivati = false;
    // Start is called before the first frame update
    private void Awake()
    {
    
    }
    void Start()
    {
       //Invoke("IstanziaPunto", 2.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 CalcolaPunto(float lat, float lng)
    {
        Vector3 pos;
        pos.x = raggio * Mathf.Cos((lng) * Mathf.Deg2Rad) * Mathf.Cos(lat * Mathf.Deg2Rad);
        pos.y =  raggio * Mathf.Sin(lat * Mathf.Deg2Rad);
        pos.z = raggio * Mathf.Sin((lng) * Mathf.Deg2Rad) * Mathf.Cos(lat * Mathf.Deg2Rad);

        return pos;
    }

    public void IstanziaPunto()
    {
        if (parent_punti.gameObject.activeSelf)
        {
            parent_punti.gameObject.SetActive(false);
        }
        else
        {
            parent_punti.gameObject.SetActive(true);
        }
        if (!datiAttivati)
        {
            datiAttivati = true;
            parent_punti.SetActive(true);
            foreach (var dato in jdata.gameData.dati)
            {
                //float valore_scala=1 ;
                //foreach(var citta in jdata.healthDatas.dati)
                //{
                //    if(citta.GetCity() == dato.city)
                //    {
                //        valore_scala = citta.LivingIndexClear();
                //    }
                //}

                if (dato.capital == "primary")
                {
                    var punto = Instantiate(cubo_scala, CalcolaPunto(dato.lat, dato.lng), Quaternion.identity);
                    punto.transform.parent = parent_punti.transform;
                    punto.transform.LookAt(punto.transform.position * 2);
                    punto.transform.localScale = new Vector3(punto.transform.localScale.x, punto.transform.localScale.y, punto.transform.localScale.z * dato.population / 100000);
                    punto.name = dato.city;
                    punto.GetComponent<MeshRenderer>().material.color = Color.green;
                    punto.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.green);
                    if (dato.population > 1000000)
                    {

                        punto.GetComponent<MeshRenderer>().material.color = Color.yellow;
                        punto.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.yellow);
                    }
                    if (dato.population > 5000000)
                    {

                        punto.GetComponent<MeshRenderer>().material.color = Color.red;
                        punto.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);
                    }
                    if (dato.population > 25000000)
                    {

                        punto.GetComponent<MeshRenderer>().material.color = Color.white;
                        punto.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white);
                    }
                }


            }

            parent_punti.transform.position = this.transform.position;
           
            parent_punti.transform.parent = this.transform;
            Debug.Log(parent_punti.transform.localRotation);
            parent_punti.transform.localRotation = Quaternion.Euler(0.09f, -90f, -90);
        }
    }
}

