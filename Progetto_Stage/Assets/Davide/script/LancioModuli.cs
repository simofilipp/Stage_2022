using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancioModuli : MonoBehaviour
{
    public bool moduloIsColliding = false;


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

            GameObject parent_modulo = other.transform.parent.parent.gameObject;
            var module = other.GetComponent<Modulo>();
            if (parent_modulo.transform.localScale.x > 1f)
            {
                parent_modulo.transform.localScale = Vector3.one;
            }
            other.transform.parent = null;
            //leggere posizione su orbita e fare un lean in quella posizione 
            
           
            LeanTween.move(other.gameObject, module.posizioneSuOrbita, module.tempoAdOrbita).setEaseInQuart().setOnStart(SpegniTrigger).setOnComplete(() =>
            {
                module.isInOrbit = true;
                LeanTween.scale(other.gameObject, other.transform.localScale *= module.scalaFinale, 0.6f).setOnComplete(() =>
                {
                    if (module.hasTrail)
                    {
                        other.transform.GetComponentInChildren<TrailRenderer>().enabled = true;
                    }
                    other.transform.parent = module.posizioneSuOrbita;
                    module.scalaBase = other.transform.localScale;
                    other.transform.parent.gameObject.SetActive(true);
                    if(module.pianeta2D != null)
                    {
                        module.pianeta2D.SetActive(true);
                    }

                    other.gameObject.transform.rotation = new Quaternion(0,0,0,0);
                    if (module.isSatellite)
                    {
                        module.posizioneSuOrbita.parent.LeanRotateAroundLocal(Vector3.up,module.direzioneOrbita, other.GetComponent<Modulo>().velRotazioneOrbita).setRepeat(-1);
                    }
                    if (module.velRotazioneSelf != 0)
                    {
                        other.transform.LeanRotateAroundLocal(Vector3.forward,-360,module.velRotazioneSelf).setRepeat(-1); 
                    }
                    parent_modulo.SetActive(false);

                    OptionsPlanetarium.moduloAttivo = false;
                });
            });
        }
        else if(other.gameObject.tag == "Modulo")
        {
            moduloIsColliding = true;
        }
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
