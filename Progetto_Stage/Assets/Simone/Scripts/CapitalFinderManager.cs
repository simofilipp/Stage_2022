using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitalFinderManager : MonoBehaviour
{
    [SerializeField] GameObject terraInterazioni;
    [SerializeField] GameObject terraGioco;

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
        terraGioco.SetActive(true);
    }

    public void SpegniGioco()
    {
        terraInterazioni.SetActive(true);
        terraGioco.SetActive(false);
    }
}
