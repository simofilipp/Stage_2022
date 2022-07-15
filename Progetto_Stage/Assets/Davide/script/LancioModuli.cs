using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancioModuli : MonoBehaviour
{
    public bool moduloIsColliding = false;
    public bool moduloInViaggio = false;

    public static float activeOrbitScale = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {

        if(other.gameObject.tag == "Modulo" && !other.GetComponent<Modulo>().afferrato)
        {
            CheckForTripTOrbit(other);
        }
        else if(other.gameObject.tag == "Modulo")
        {
            moduloIsColliding = true;
        }
    }

    public void CheckForTripTOrbit(Collider other)
    {
        if (!other.GetComponent<Modulo>().isAsteroid)
        {

            GameObject parent_modulo = other.transform.parent.parent.gameObject;
            var module = other.GetComponent<Modulo>();
            TripToOrbit(other, parent_modulo, module);
        }
        else
        {
            other.GetComponent<Modulo>().posizioneSuOrbita.gameObject.SetActive(true);
            other.transform.parent.parent.gameObject.SetActive(false);
            other.GetComponent<Modulo>().isInOrbit = true;
            SpegniTrigger();
            OptionsPlanetarium.moduloAttivo = false;
        }
    }

    private void TripToOrbit(Collider other, GameObject parent_modulo, Modulo module)
    {
        if (parent_modulo.transform.localScale.x > 1f)
        {
            parent_modulo.transform.localScale = Vector3.one;
        }
        other.transform.parent = null;
        //leggere posizione su orbita e fare un lean in quella posizione 

        moduloInViaggio = true;
        LeanTween.move(other.gameObject, module.posizioneSuOrbita, module.tempoAdOrbita).setEaseInQuart().setOnStart(SpegniTrigger).setOnComplete(() =>
        {
            module.isInOrbit = true;
            LeanTween.scale(other.gameObject, other.transform.localScale *= (module.scalaFinale*activeOrbitScale), 0.6f).setOnComplete(() =>
            {
                if (module.hasTrail)
                {
                    other.transform.GetComponentInChildren<TrailRenderer>().enabled = true;
                }
                other.transform.parent = module.posizioneSuOrbita;
                module.scalaBase = other.transform.localScale / activeOrbitScale;
                other.transform.parent.gameObject.SetActive(true);
                if (module.pianeta2D != null)
                {
                    module.pianeta2D.SetActive(true);
                }

                other.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                if (module.isSatellite)
                {
                    if (!module.isGPS)
                    {
                        module.posizioneSuOrbita.parent.LeanRotateAroundLocal(Vector3.up, module.direzioneOrbita, other.GetComponent<Modulo>().velRotazioneOrbita).setRepeat(-1);
                    }
                    else
                    {
                        module.posizioneSuOrbita.parent.LeanRotateAroundLocal(Vector3.forward, module.direzioneOrbita, other.GetComponent<Modulo>().velRotazioneOrbita).setRepeat(-1);
                    }
                    
                }
                if (module.velRotazioneSelf != 0)
                {
                    other.transform.LeanRotateAroundLocal(Vector3.forward, -360, module.velRotazioneSelf).setRepeat(-1);
                }
                parent_modulo.SetActive(false);

                moduloInViaggio = false;
                OptionsPlanetarium.moduloAttivo = false;
            });
        });
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Modulo")
        {
            moduloIsColliding = false;
        }
        
    }

    private void SpegniTrigger()
    {
        this.gameObject.SetActive(false);
    }
}
