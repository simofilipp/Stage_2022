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
            if (parent_modulo.transform.localScale.x > 1f)
            {
                parent_modulo.transform.localScale = Vector3.one;
            }
            other.transform.parent = null;
            //leggere posizione su orbita e fare un lean in quella posizione 
            LeanTween.scale(other.gameObject,other.transform.localScale *= other.GetComponent<Modulo>().scalaFinale,2.5f);
           
            LeanTween.move(other.gameObject, other.GetComponent<Modulo>().posizioneSuOrbita, 1.5f).setOnComplete(() =>
            {
                if (other.GetComponent<Modulo>().hasTrail)
                {
                    other.transform.GetComponentInChildren<TrailRenderer>().enabled = true;
                }
                this.gameObject.SetActive(false);
                other.transform.parent = other.GetComponent<Modulo>().posizioneSuOrbita;
                other.gameObject.transform.rotation = new Quaternion(0,0,0,0);
                other.GetComponent<Modulo>().posizioneSuOrbita.parent.LeanRotateAroundLocal(Vector3.up, other.GetComponent<Modulo>().direzioneOrbita, other.GetComponent<Modulo>().velRotazioneOrbita).setRepeat(-1);
                if (other.GetComponent<Modulo>().velRotazioneSelf != 0)
                {
                    //other.transform.LeanRotateAroundLocal
                }
                parent_modulo.SetActive(false);
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


}
