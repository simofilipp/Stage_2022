using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionsPlanetarium : MonoBehaviour
{
    [SerializeField] GameObject iss;
    [SerializeField] GameObject hubble;
    [SerializeField] LancioModuli lancioModuli;
    [SerializeField] TMP_Text countdown;
    float timer = 15f;

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
            StartCoroutine(AutomaticTransfer(planet));
        }
    }

    IEnumerator AutomaticTransfer(GameObject planet)
    {
        countdown.text = timer.ToString();
        StartCoroutine(Timer());
        yield return new WaitForSeconds(15f);
        if (planet.activeSelf && !lancioModuli.moduloInViaggio)
        {
            //manda in orbita automaticamente
            lancioModuli.CheckForTripTOrbit(planet.GetComponentInChildren<Modulo>().gameObject.GetComponent<Collider>());
        }
        else
        {
            //non fa niente
        }
    }
    IEnumerator Timer()
    {
        while (timer >= 0 && !lancioModuli.moduloInViaggio)
        {
            yield return new WaitForSeconds(1);
            timer -= 1;
            countdown.text = timer.ToString();

        }
        countdown.text = "";
        timer = 15f;
    }
}
