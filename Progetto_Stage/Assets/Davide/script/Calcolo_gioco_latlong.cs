using Oculus.Interaction;
using Oculus.Interaction.Surfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Calcolo_gioco_latlong : MonoBehaviour
{
    [SerializeField] GameObject cursore;
    [SerializeField] GameObject terra;
    [SerializeField] GameObject puntoPrefab;
    [SerializeField] JsonData jdata;
    [SerializeField] TMP_Text testoCapitale;
    [SerializeField] TMP_Text testoDistanza;
    [SerializeField] TMP_Text testoTempo;


    float tempo = 0;
    int tentativi=0;
    int tentativiCapitali=0;
    float punteggio;
    float distanzaCalcolataConIRadianti;
    bool stagiocando;

    List<GameData> listaCapitali=new List<GameData>();
    GameData capitaleEstratta;

     float latPuntoDaTrovare;
     float lngPuntoDaTrovare;


    float lat,lng;


    public void RiempiListaCapitali()
    {
        foreach (var dato in jdata.gameData.dati)
        {
            if (dato.capital == "primary")
            {
                listaCapitali.Add(dato);
            }
        }
        capitaleEstratta = EstraiCapitale();
        testoCapitale.text = capitaleEstratta.city;
        this.GetComponent<RayInteractable>().enabled = true;
        testoDistanza.text = "";
        stagiocando = true;
        StartCoroutine(Timer());


    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    public void RilevaPinchLocal()
    {
        if (cursore.activeSelf)
        {
            var puntoIstanziato = Instantiate(puntoPrefab, cursore.transform.position, terra.transform.rotation);
            Vector3 raggio = puntoIstanziato.transform.position - terra.transform.position;
            float raggioValue = Vector3.Distance(puntoIstanziato.transform.position, terra.transform.position);
            Vector3 nuoveCoordinateP = Vector3.zero;

           
            
            nuoveCoordinateP.x = Vector3.Dot(raggio, terra.transform.right.normalized);
            nuoveCoordinateP.y = Vector3.Dot(raggio, terra.transform.up.normalized);
            nuoveCoordinateP.z = Vector3.Dot(raggio, terra.transform.forward.normalized);
           

            lat = Mathf.Asin((nuoveCoordinateP.y) / raggioValue) * Mathf.Rad2Deg;


            lng = Mathf.Atan2(Mathf.Abs(nuoveCoordinateP.z), Mathf.Abs(nuoveCoordinateP.x)) * Mathf.Rad2Deg;

            if (nuoveCoordinateP.x > 0)
            {
                if (nuoveCoordinateP.z > 0)
                {
                    lng = lng - 180;
                }
                else
                {
                    lng = 180 - lng;
                }
            }
            else
            {
                if (nuoveCoordinateP.z > 0)
                {
                    lng = -lng;
                }

            }

            

            puntoIstanziato.transform.parent = terra.transform;
            DistanzaDaCapitaleEstratta();

            tentativi += 1;
            if (tentativi == 3)
            {
                tentativi = 0;
                tentativiCapitali += 1;
                punteggio += distanzaCalcolataConIRadianti;

                if(tentativiCapitali == 3)
                {
                    stagiocando = false;
                    StopCoroutine(Timer());
                    testoCapitale.text = "GAME OVER";
                    testoDistanza.text = "Punteggio: " + punteggio / tempo;
                    this.GetComponent<RayInteractable>().enabled = false;
                    return;
                }
                RiempiListaCapitali();
            }

        }
    }

    private void DistanzaDaCapitaleEstratta()
    {
        

        latPuntoDaTrovare = capitaleEstratta.lat;
        lngPuntoDaTrovare = capitaleEstratta.lng;

        float latInRad = lat * Mathf.Deg2Rad;
        float longInRad = lng * Mathf.Deg2Rad;

        float latNotaInRad = latPuntoDaTrovare * Mathf.Deg2Rad;
        float lngNotaInRad = lngPuntoDaTrovare * Mathf.Deg2Rad;

        //Calcolo in radianti
        float deltaLat = (latInRad - latNotaInRad);
        float deltalng = (longInRad - lngNotaInRad);
        float a = Mathf.Pow(Mathf.Sin(deltaLat / 2), 2) + (Mathf.Cos(latInRad) * Mathf.Cos(latNotaInRad) * Mathf.Pow(Mathf.Sin(deltalng / 2), 2));
        float c = 2 * Mathf.Asin(Mathf.Sqrt(a));
        distanzaCalcolataConIRadianti = c * 6371f;
        Debug.Log("Distanza da " + capitaleEstratta.city + ": " + distanzaCalcolataConIRadianti);
        if (distanzaCalcolataConIRadianti <= 100)
        {
            testoDistanza.text = "VITTORIA!!\nDistanza da " + capitaleEstratta.city + ": " + distanzaCalcolataConIRadianti;
        }
        else
        {
            testoDistanza.text = "Torna a studiare geografia!!\nDistanza da " + capitaleEstratta.city + ": " + distanzaCalcolataConIRadianti;
        }
    }

    GameData EstraiCapitale()
    {
        return listaCapitali[Random.Range(0,listaCapitali.Count)];
    }

    IEnumerator Timer()
    {
        while (stagiocando)
        {
            testoTempo.text = tempo.ToString();
            yield return new WaitForSeconds(1f);
            tempo += 1;
        }
    }
}
