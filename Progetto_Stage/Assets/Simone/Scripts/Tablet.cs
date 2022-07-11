using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablet : MonoBehaviour
{

    [SerializeField]
    Animator gambaSingola;

    bool alreadyDown;

    // Start is called before the first frame update
    void Start()
    {
        //vetro.GetComponent<MeshRenderer>().material.renderQueue = 3001;
        //immagineASchermo.GetComponent<MeshRenderer>().material.renderQueue = 3002;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AbbassaGamba()
    {
        if (!alreadyDown)
        {
            alreadyDown = true;
            gambaSingola.SetTrigger("ScomparsaGamba");
        }
    }
}
