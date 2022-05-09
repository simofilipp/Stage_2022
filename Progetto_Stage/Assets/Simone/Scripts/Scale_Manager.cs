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


    float limiteMassimo = 1.4f;
    float limiteMinimo = 0.5f;

    int counterSelezionato=0;
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

        var twograbTransf = this.GetComponent<TwoGrabFreeTransformer>();
        percentuale = twograbTransf.scalePercentage;
        //Debug.Log(percentuale);
        Debug.Log(scala_ini_value_Pianeta);

        //this.transform.rotation = target_Follow.transform.rotation;

        //this.transform.localScale *= percentuale;
        if (percentuale != 0f)
        {
            scala_Corrente_Pianeta = (scala_ini_value_Pianeta/20) * percentuale;
            if (scala_Corrente_Pianeta > limiteMinimo && scala_Corrente_Pianeta < limiteMassimo)
            {
                pianeta.transform.localScale = scala_iniziale_Pianeta * scala_Corrente_Pianeta;

            }
        }
    }

    public void OnSelectedScale()
    {
        counterSelezionato += 1;
    }

    public void BackToNormalScale()
    {
        counterSelezionato -= 1;
        if (counterSelezionato == 0)
        {
            this.transform.localScale = scala_iniziale_Sferetta;
        }
    }
}
