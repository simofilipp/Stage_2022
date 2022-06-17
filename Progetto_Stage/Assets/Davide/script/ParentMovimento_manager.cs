using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentMovimento_manager : MonoBehaviour
{

    [SerializeField]
    GameObject noi;

    [SerializeField]
    GameObject piattaforma;

    [SerializeField]
    GameObject scifiConsole;

    [SerializeField]
    GameObject tablet;

    [SerializeField]
    GameObject parent_spostamento;

    [SerializeField]
    GameObject planetario;

    [SerializeField]
    GameObject pianeta;

    

    bool arrivato;

    int id;



    [SerializeField]
    GameObject destinazione;
  
    

    public void SetParentViaggio()
    {
        noi.transform.parent = parent_spostamento.transform;
        piattaforma.transform.parent = parent_spostamento.transform;
        scifiConsole.transform.parent = parent_spostamento.transform;
        tablet.transform.parent = parent_spostamento.transform;
        planetario.transform.parent = parent_spostamento.transform;
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "InitialInteractor")
        {
            SetParentViaggio();
            var module = pianeta.GetComponent<Modulo>();
            LeanTween.resumeAll();
            module.PauseTween();
            
            parent_spostamento.LeanMove(destinazione.transform.position, 5f).setOnComplete(() =>
            {
                parent_spostamento.transform.parent= destinazione.transform;
                module.RuotaSole();
                
            });

            //StartCoroutine(Segui());

            //parent_spostamento.LeanMove(destinazione.transform.position,10);
            
        }
       
    }

   
    


}
