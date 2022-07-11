using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsing_script : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.value(.8f, 1.5f, 1f).setOnUpdate((float value) =>
        {
            this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white * value);

        }).setLoopPingPong(-1);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
