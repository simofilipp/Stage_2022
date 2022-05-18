using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Istanzia_istogrammi : MonoBehaviour
{

    public JsonData jdata;
    public GameObject cubo_scala;
    [SerializeField]
    GameObject parent_punti;
    // Start is called before the first frame update
    void Start()
    {
        IstanziaPunto();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 CalcolaPunto(float lat, float lng)
    {
        Vector3 pos;
        pos.x = 5f * Mathf.Cos((lng) * Mathf.Deg2Rad) * Mathf.Cos(lat * Mathf.Deg2Rad);
        pos.y = 5f * Mathf.Sin(lat * Mathf.Deg2Rad);
        pos.z = 5f * Mathf.Sin((lng) * Mathf.Deg2Rad) * Mathf.Cos(lat * Mathf.Deg2Rad);

        return pos;
    }

    void IstanziaPunto()
    {
        foreach (var dato in jdata.gameData.dati)
        {

           var punto =  Instantiate(cubo_scala, CalcolaPunto(dato.lat, dato.lng), Quaternion.identity);
            punto.transform.parent = parent_punti.transform;
        }
        parent_punti.transform.position = this.transform.position;
        parent_punti.transform.parent = this.transform;
    }
}
