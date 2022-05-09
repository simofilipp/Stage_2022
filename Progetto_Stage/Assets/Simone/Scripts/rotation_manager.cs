using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation_manager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> altreSfere;
    bool selezionato;
    [SerializeField]
    GameObject terra;

    int counterSelezionato=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AvviaCoroutine()
    {
        counterSelezionato += 1;
        selezionato = true;
        terra.GetComponent<Animator>().enabled = false;
        StartCoroutine(Rotazione());
    }

    IEnumerator Rotazione()
    {
        //terra.GetComponent<Animator>().SetTrigger("rotazione");
        while (selezionato ==true)
        {
            terra.transform.rotation = this.transform.rotation;
            yield return null;
        }
        //terra.GetComponent<Animator>().SetTrigger("rotazione");
    }

    public void StoppaCoroutine()
    {
        counterSelezionato -= 1;
        if(counterSelezionato == 0)
        {
            selezionato = false;
            terra.GetComponent<Animator>().enabled = true;
        }
    }

}
