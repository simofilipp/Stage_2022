using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancioModuli : MonoBehaviour
{
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

            //leggere posizione su orbita e fare un lean in quella posizione 
            LeanTween.scale(other.gameObject,other.transform.localScale *= other.GetComponent<Modulo>().scalaFinale,2.5f);
            LeanTween.move(other.gameObject, other.GetComponent<Modulo>().posizioneSuOrbita, 3f).setOnComplete(() =>
            {
                other.transform.parent = other.GetComponent<Modulo>().posizioneSuOrbita;
                other.GetComponent<Modulo>().posizioneSuOrbita.parent.LeanRotateAroundLocal(Vector3.up, other.GetComponent<Modulo>().direzioneOrbita, other.GetComponent<Modulo>().velRotazioneOrbita).setRepeat(-1);
                if (other.GetComponent<Modulo>().velRotazioneSelf != 0)
                {
                    //other.transform.LeanRotateAroundLocal
                }
                if (other.GetComponent<Modulo>().hasTrail)
                {
                    other.transform.GetComponentInChildren<TrailRenderer>().enabled = true;
                }
                parent_modulo.SetActive(false);
            });

            
            
        }
    }


}
