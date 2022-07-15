using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruota_GPS : MonoBehaviour
{
    [SerializeField] List<GameObject> altreOrbiteGPS;
    [SerializeField] List<GameObject> fratelliGPS;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var orb in altreOrbiteGPS)
        {
            orb.SetActive(true);
            LeanTween.rotateAroundLocal(orb, Vector3.forward, 360, 45).setRepeat(-1);
        }
        foreach(var fratm in fratelliGPS)
        {
            fratm.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
