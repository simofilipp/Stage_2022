using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale_Manager : MonoBehaviour
{
    [SerializeField] GameObject pianeta;

    float percentuale;
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
        scala_iniziale_Pianeta = pianeta.transform.localScale;
        scala_Corrente_Pianeta = pianeta.transform.localScale.x;
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
            percentuale = 0;
            this.transform.localScale = scala_iniziale_Sferetta;
            twograbTransf.MarkAsBaseScale();
        }
    }

    IEnumerator Scalatura()
    {
        yield return null;
        while (selezionato)
        {
            percentuale = twograbTransf.scalePercentage;
            Debug.Log(scala_ini_value_Pianeta);
            if (percentuale != 0f)
            {
                scala_Corrente_Pianeta = (scala_ini_value_Pianeta / 10f) * percentuale;
                if (scala_Corrente_Pianeta > limiteMinimo && scala_Corrente_Pianeta < limiteMassimo)
                {
                    pianeta.transform.localScale = scala_iniziale_Pianeta * scala_Corrente_Pianeta;

                }
            }

            yield return null;
        }
    }
}
