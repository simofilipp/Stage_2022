using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostraCanvaPianeti : MonoBehaviour
{
    [SerializeField] GameObject canvasPianeti;
    [SerializeField] GameObject pianeti_da_accendere;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AccendiCanvas(GameObject pianeta)
    {
        if (!canvasPianeti.activeSelf)
        {
            canvasPianeti.SetActive(true);
        }
        //Spawnare il pianeta cliccato, al centro del canvas
        for(int i=0;  i<canvasPianeti.transform.GetChild(0).childCount; i++)
        {
            canvasPianeti.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }
        pianeta.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "InitialInteractor")
        {
            AccendiCanvas(pianeti_da_accendere);
        }
    }
}
