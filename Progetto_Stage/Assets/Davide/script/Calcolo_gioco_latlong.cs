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
            float raggio = Mathf.Sqrt((proiezioneRaggio * proiezioneRaggio) + (puntoIstanziato.transform.position.y * puntoIstanziato.transform.position.y));

            //float distanzaPC = Vector3.Distance(new Vector3(puntoIstanziato.transform.position.x, coordinateZero.position.y, puntoIstanziato.transform.position.z), coordinateZero.position);

            lat = Mathf.Asin((puntoIstanziato.transform.position.y) / raggio) * Mathf.Rad2Deg;
            float raggioProiet = Mathf.Cos(lat) * raggio;
            float proiezionePunto = Vector3.Distance(new Vector3(puntoIstanziato.transform.position.x, terra.transform.position.y, puntoIstanziato.transform.position.z), terra.transform.position);
            lng = Mathf.Acos(((puntoIstanziato.transform.position.x* puntoIstanziato.transform.position.x)- (puntoIstanziato.transform.position.z * puntoIstanziato.transform.position.z)-(raggioProiet*raggioProiet))
                /(-2*raggio*raggioProiet)
                ) * Mathf.Rad2Deg; ;

           // Debug.Log(distanzaPC);
            Debug.Log(proiezionePunto);


            //lng = Mathf.Asin((coordinateZero.position.z/Mathf.Sqrt((coordinateZero.position.z* coordinateZero.position.z)+(coordinateZero.position.x * coordinateZero.position.x))))*Mathf.Rad2Deg + Mathf.Asin((puntoIstanziato.transform.position.z / Mathf.Sqrt((puntoIstanziato.transform.position.z * puntoIstanziato.transform.position.z) + (puntoIstanziato.transform.position.x * puntoIstanziato.transform.position.x))))*Mathf.Rad2Deg;

            Debug.LogWarning("lat: "+lat+"\nlong: "+lng);
        }
    }
}
