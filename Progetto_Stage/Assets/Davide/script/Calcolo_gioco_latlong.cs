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
    [SerializeField] Transform coordinateZero;

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
            var puntoIstanziato= Instantiate(puntoPrefab, cursore.transform.position, coordinateZero.rotation);
            puntoIstanziato.transform.parent = terra.transform;
            float proiezioneRaggio = Mathf.Sqrt(((puntoIstanziato.transform.position.x - terra.transform.position.x) * (puntoIstanziato.transform.position.x - terra.transform.position.x))
                + ((puntoIstanziato.transform.position.z - terra.transform.position.z) * (puntoIstanziato.transform.position.z - terra.transform.position.z)));
            float raggio = Mathf.Sqrt((proiezioneRaggio * proiezioneRaggio) + (puntoIstanziato.transform.position.y * puntoIstanziato.transform.position.y));

            lng = Mathf.Acos((((coordinateZero.position.z- puntoIstanziato.transform.position.z)* (coordinateZero.position.z - puntoIstanziato.transform.position.z))+ ((coordinateZero.position.x - puntoIstanziato.transform.position.x) * (coordinateZero.position.x - puntoIstanziato.transform.position.x))
                - (raggio * raggio) - ((puntoIstanziato.transform.position.x* puntoIstanziato.transform.position.x) + (puntoIstanziato.transform.position.z* puntoIstanziato.transform.position.z))) / (-2 * raggio) * Mathf.Sqrt((puntoIstanziato.transform.position.x * puntoIstanziato.transform.position.x) + (puntoIstanziato.transform.position.z * puntoIstanziato.transform.position.z)))*Mathf.Rad2Deg;




            //lng = Mathf.Asin((coordinateZero.position.z/Mathf.Sqrt((coordinateZero.position.z* coordinateZero.position.z)+(coordinateZero.position.x * coordinateZero.position.x))))*Mathf.Rad2Deg + Mathf.Asin((puntoIstanziato.transform.position.z / Mathf.Sqrt((puntoIstanziato.transform.position.z * puntoIstanziato.transform.position.z) + (puntoIstanziato.transform.position.x * puntoIstanziato.transform.position.x))))*Mathf.Rad2Deg;
            lat = Mathf.Asin((coordinateZero.position.y) / raggio) * Mathf.Rad2Deg + Mathf.Asin((puntoIstanziato.transform.position.y) / raggio)*Mathf.Rad2Deg;
            Debug.LogWarning("lat: "+lat+"\nlong: "+lng);
        }
    }
}
