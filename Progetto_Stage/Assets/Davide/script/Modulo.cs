using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo : MonoBehaviour
{

    string nome;

    public GameObject triggerOrbita;
    
    public Transform posizioneSuOrbita;

    
    public float velRotazioneOrbita;

    
    public float velRotazioneSelf;

   
    public float scalaFinale;

    public float direzioneOrbita;

    public int leanID;

    public bool afferrato;
    public bool hasTrail;

    public GameObject sole;

    int countSelezionato=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TrueAfferrato()
    {
        countSelezionato += 1;
        if (countSelezionato == 1)
        {
            afferrato = true;
            triggerOrbita.SetActive(true);
        }
    }
    public void FalseAfferrato()
    {
        countSelezionato -= 1;
        if (countSelezionato == 0)
        {
            afferrato = false;
            if(!triggerOrbita.GetComponent<LancioModuli>().moduloIsColliding)
                triggerOrbita.SetActive(false);
        }
    }

    public void ResumeTween()
    {
        LeanTween.resume(leanID);
    }

    public void PauseTween()
    {
        LeanTween.pause(leanID);
    }

    public void RuotaSole()
    {
        LeanTween.cancel(sole);
        sole.transform.LeanRotateAroundLocal(Vector3.up, 360, velRotazioneOrbita).setRepeat(-1);
    }
}
