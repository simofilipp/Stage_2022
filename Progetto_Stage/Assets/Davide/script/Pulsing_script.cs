using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsing_script : MonoBehaviour
{
    int id;
    // Start is called before the first frame update
    void Start()
    {
        id = LeanTween.value(.8f, 1.5f, 1f).setOnUpdate((float value) =>
        {
            this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white * value);

        }).setLoopPingPong(-1).id;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnDestroy()
    {
        LeanTween.cancel(id);
    }
}
