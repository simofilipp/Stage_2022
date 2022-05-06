using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale_Manager : MonoBehaviour
{
    [SerializeField] GameObject pianeta;

    float percentuale;
    Vector3 scala_iniziale;
    float scala_Corrente;
    float scala_ini_value;


    float limiteMassimo = 1.2f;
    float limiteMinimo = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        scala_iniziale = pianeta.transform.localScale;
        scala_Corrente = pianeta.transform.localScale.x;
        scala_ini_value = scala_Corrente;
    }

    // Update is called once per frame
    void Update()
    {

        var twograbTransf = this.GetComponent<TwoGrabFreeTransformer>();
        percentuale = twograbTransf.scalePercentage;
        //Debug.Log(percentuale);
        Debug.Log(scala_ini_value);

        //this.transform.rotation = target_Follow.transform.rotation;

        //this.transform.localScale *= percentuale;
        if (percentuale != 0f)
        {

            scala_Corrente = (scala_ini_value) * percentuale;
            if (scala_Corrente > limiteMinimo && scala_Corrente < limiteMassimo)
            {
                this.transform.localScale = scala_iniziale * scala_Corrente;

            }

        }


    }
}
