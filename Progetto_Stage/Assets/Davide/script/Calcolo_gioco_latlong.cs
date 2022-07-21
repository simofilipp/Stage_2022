using Oculus.Interaction;
using Oculus.Interaction.Surfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calcolo_gioco_latlong : MonoBehaviour
{
    [SerializeField] GameObject cursore;
    [SerializeField] GameObject terra;
    [SerializeField] GameObject puntoPrefab;

    public float latPuntoDaTrovare;
    public float lngPuntoDaTrovare;


    float lat,lng;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RilevaPinch() 
    {
        if (cursore.activeSelf)
        {
            var puntoIstanziato= Instantiate(puntoPrefab, cursore.transform.position, terra.transform.rotation);
            puntoIstanziato.transform.parent = terra.transform;
            float proiezioneRaggio = Mathf.Sqrt(((puntoIstanziato.transform.position.x - terra.transform.position.x) * (puntoIstanziato.transform.position.x - terra.transform.position.x))
                + ((puntoIstanziato.transform.position.z - terra.transform.position.z) * (puntoIstanziato.transform.position.z - terra.transform.position.z)));
            //float raggio = Mathf.Sqrt((proiezioneRaggio * proiezioneRaggio) + (puntoIstanziato.transform.position.y * puntoIstanziato.transform.position.y));
            float raggio=Vector3.Distance(puntoIstanziato.transform.position, terra.transform.position);

            //float distanzaPC = Vector3.Distance(new Vector3(puntoIstanziato.transform.position.x, coordinateZero.position.y, puntoIstanziato.transform.position.z), coordinateZero.position);

            Vector3 position = puntoIstanziato.transform.position;

            Debug.Log(puntoIstanziato.transform.position);

            lat = Mathf.Asin((puntoIstanziato.transform.position.y-terra.transform.position.y) / raggio) * Mathf.Rad2Deg;
            float raggioProiet = Mathf.Cos(lat) * raggio;
            //float proiezionePunto = Vector3.Distance(new Vector3(puntoIstanziato.transform.position.x, terra.transform.position.y, puntoIstanziato.transform.position.z), terra.transform.position);

            lng = Mathf.Atan2(Mathf.Abs(puntoIstanziato.transform.position.z - terra.transform.position.z), Mathf.Abs(puntoIstanziato.transform.position.x - terra.transform.position.x))*Mathf.Rad2Deg;

            //lng=Mathf.Asin(Mathf.Abs(puntoIstanziato.transform.position.z-terra.transform.position.z)/proiezioneRaggio)*Mathf.Rad2Deg;

            //lng = Mathf.Acos(((puntoIstanziato.transform.position.x* puntoIstanziato.transform.position.x)- (puntoIstanziato.transform.position.z * puntoIstanziato.transform.position.z)-(raggioProiet*raggioProiet))
            //    /(-2* puntoIstanziato.transform.position.z * raggioProiet)
            //    ) * Mathf.Rad2Deg; ;

           // Debug.Log(distanzaPC);

            //lng = Mathf.Asin((coordinateZero.position.z/Mathf.Sqrt((coordinateZero.position.z* coordinateZero.position.z)+(coordinateZero.position.x * coordinateZero.position.x))))*Mathf.Rad2Deg + Mathf.Asin((puntoIstanziato.transform.position.z / Mathf.Sqrt((puntoIstanziato.transform.position.z * puntoIstanziato.transform.position.z) + (puntoIstanziato.transform.position.x * puntoIstanziato.transform.position.x))))*Mathf.Rad2Deg;

            Debug.LogWarning("lat: "+lat+"\nlong: "+lng);
        }
    }
    public void RilevaPinchLocal()
    {
        if (cursore.activeSelf)
        {
            var puntoIstanziato = Instantiate(puntoPrefab, cursore.transform.position, terra.transform.rotation);

            Vector3 raggio = puntoIstanziato.transform.position - terra.transform.position;
            float raggioValue = Vector3.Distance(puntoIstanziato.transform.position, terra.transform.position);
            Vector3 nuoveCoordinateP = Vector3.zero;
            
            Debug.Log(raggio.magnitude);
            Debug.Log(raggioValue);
            nuoveCoordinateP.x = Vector3.Dot(raggio, terra.transform.right.normalized);
            nuoveCoordinateP.y = Vector3.Dot(raggio, terra.transform.up.normalized);
            nuoveCoordinateP.z = Vector3.Dot(raggio, terra.transform.forward.normalized);
            Debug.LogWarning(nuoveCoordinateP);

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
            
            Debug.LogWarning("lat: " + lat + "\nlong: " + lng);

            float latInRad= lat * Mathf.Deg2Rad;
            float longInRad = lng * Mathf.Deg2Rad;

            float latNotaInRad =latPuntoDaTrovare * Mathf.Deg2Rad;
            float lngNotaInRad = lngPuntoDaTrovare * Mathf.Deg2Rad;

           
            //Questa é la distanza tra i due punti sulla sfera considerando il suo raggio
            float distanceTwoPoint = raggioValue * Mathf.Acos((Mathf.Sin(lat) * Mathf.Sin(latPuntoDaTrovare)) + (Mathf.Cos(lat) * Mathf.Cos(latPuntoDaTrovare) * Mathf.Cos(lngPuntoDaTrovare - lng)));
            //Questa é la distanza tra i due punti sulla sfera considerando il raggio terrestre
            float distanceTwoPointOnEarth = 3963.0f * Mathf.Acos((Mathf.Sin(lat) * Mathf.Sin(latPuntoDaTrovare)) + (Mathf.Cos(lat) * Mathf.Cos(latPuntoDaTrovare) * Mathf.Cos(lngPuntoDaTrovare - lng)));
            distanceTwoPointOnEarth *= 1.609344f;


            //Calcolo in radianti
            float deltaLat = (latInRad - latNotaInRad);
            float deltalng = (longInRad - lngNotaInRad);
            float a = Mathf.Pow(Mathf.Sin(deltaLat / 2), 2)+(Mathf.Cos(latInRad)*Mathf.Cos(latNotaInRad)*Mathf.Pow(Mathf.Sin(deltalng/2),2));
            float c = 2 * Mathf.Asin(Mathf.Sqrt(a));
            float distanzaCalcolataConIRadianti = c * 6371f;
            Debug.Log("distanza da genova: " + distanceTwoPointOnEarth);
            Debug.Log("distanza da genova: " + distanzaCalcolataConIRadianti);
        }
    }
}
