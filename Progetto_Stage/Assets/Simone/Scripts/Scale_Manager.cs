using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale_Manager : MonoBehaviour
{
    [SerializeField] GameObject pianeta;

    float percentuale;
    float percentualeRilascio;
    float scala_Corrente_Pianeta;
    float scala_ini_value_Pianeta;
    Vector3 scala_iniziale_Pianeta;
    Vector3 scala_iniziale_Sferetta;
    TwoGrabFreeTransformer twograbTransf;


    float limiteMassimo = 1.4f;
    float limiteMinimo = 1f;

    int counterSelezionato=0;
    bool selezionato = false;
    // Start is called before the first frame update
    void Start()
    {
        scala_iniziale_Sferetta = transform.localScale;
        //cambiare in base a quanto si cambia la scala iniziale della sfera
        scala_iniziale_Pianeta = pianeta.transform.localScale*10;
        scala_Corrente_Pianeta = pianeta.transform.localScale.x*10;
        scala_ini_value_Pianeta = scala_Corrente_Pianeta;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSelectedScale()
    {
        counterSelezionato += 1;
        twograbTransf = this.GetComponent<TwoGrabFreeTransformer>();
        if(counterSelezionato == 2)
        {
            selezionato = true;
            StartCoroutine(Scalatura());
        }
    }

    public void BackToNormalScale()
    {
        counterSelezionato -= 1;
        if (counterSelezionato == 0)
        {
            selezionato = false;
            //percentuale = 0;
            this.transform.localScale = scala_iniziale_Sferetta;
            twograbTransf.MarkAsBaseScale();
        }
    }

    IEnumerator Scalatura()
    {
        while (selezionato)
        { 
            pianeta.transform.localScale = scala_iniziale_Pianeta * twograbTransf._activeScale;
            yield return null;
        }
    }
}
