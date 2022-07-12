using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPlanetarium : MonoBehaviour
{
    [SerializeField] GameObject iss;
    [SerializeField] GameObject hubble;

    public static bool moduloAttivo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableISS()
    {
        iss.SetActive(true);
    }
    public void EnableHubble()
    {
        hubble.SetActive(true);
    }

    public void EnablePlanet(GameObject planet)
    {
        if(planet.GetComponentInChildren<MeshRenderer>() != null && !moduloAttivo)
        {
            moduloAttivo = true;
            planet.SetActive(true);
        }
    }
}
