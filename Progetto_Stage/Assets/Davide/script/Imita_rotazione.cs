using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;


public class Imita_rotazione : MonoBehaviour
{
    [SerializeField]
    GameObject target_Follow;
    float percentuale;
    Vector3 scala_iniziale;
    float scala_Corrente;
    float scala_ini_value;


    float limiteMassimo= 1.2f;
    float limiteMinimo= 0.5f;

   
    public WorldMapManager.State state;


    // Start is called before the first frame update
    void Start()
    {
        scala_iniziale = this.transform.localScale;
        scala_Corrente = this.transform.localScale.x;
        scala_ini_value = scala_Corrente;
    }

    // Update is called once per frame
    void Update()
    {
        
        var twograbTransf= target_Follow.GetComponent<TwoGrabFreeTransformer>();
        percentuale = target_Follow.GetComponent<TwoGrabFreeTransformer>().scalePercentage;
        //Debug.Log(percentuale);
        Debug.Log(scala_ini_value);

        this.transform.rotation = target_Follow.transform.rotation;

        //this.transform.localScale *= percentuale;
        if(percentuale != 0f)
        {
            
            scala_Corrente = (scala_ini_value/40 )* percentuale;
            //if ((twograbTransf.targetVector-twograbTransf.initialVector).magnitude<0f)
            //{
            //scala_Corrente = Mathf.Max(limiteMinimo, scala_Corrente);

            //}
            //else if((twograbTransf.targetVector - twograbTransf.initialVector).magnitude > 0f)
            //{
            //scala_Corrente = Mathf.Min(limiteMassimo, scala_Corrente);

            //}
            if(scala_Corrente> limiteMinimo && scala_Corrente < limiteMassimo)
            {
            this.transform.localScale = scala_iniziale * scala_Corrente;

            }

        }
        

    }

    public void RendiFiglio(Transform nuovo_genitore) 
    {
        target_Follow.transform.SetParent(nuovo_genitore, false);
    }

  

}
