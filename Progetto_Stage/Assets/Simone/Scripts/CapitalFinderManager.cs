using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitalFinderManager : MonoBehaviour
{
    [SerializeField] GameObject terraInterazioni;
    [SerializeField] GameObject terraGioco;
    [SerializeField] GameObject sferaGioco;
    [SerializeField] GameObject sferaInterazioni1;
    [SerializeField] GameObject sferaInterazioni2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpGioco()
    {
        terraInterazioni.SetActive(false);
        sferaInterazioni1.SetActive(false);
        sferaInterazioni2.SetActive(false);


        sferaGioco.SetActive(true);
        terraGioco.SetActive(true);
    }

    public void SpegniGioco()
    {
        terraInterazioni.SetActive(true);
        sferaInterazioni1.SetActive(true);
        sferaInterazioni2.SetActive(true);


        sferaGioco.SetActive(false);
        terraGioco.SetActive(false);

        terraGioco.GetComponent<RayInteractable>().enabled = false;
    }
}
