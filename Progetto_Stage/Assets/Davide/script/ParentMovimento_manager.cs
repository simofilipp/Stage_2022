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

    [SerializeField]
    GameObject spawnPianeti;

    [SerializeField]
    GameObject triggerOrbita;


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
        spawnPianeti.transform.parent = parent_spostamento.transform;
        triggerOrbita.transform.parent = parent_spostamento.transform;
    }


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "InitialInteractor")
        {
            foreach(var pianetino2d in planetario.GetComponentsInChildren<BoxCollider>())
            {
                pianetino2d.enabled = false;
            }
            SetParentViaggio();
            var module = pianeta.GetComponent<Modulo>();
            LeanTween.resumeAll();
            module.PauseTween();
            
            parent_spostamento.LeanMove(destinazione.transform.position, 5f).setEaseInOutQuart().setOnComplete(() =>
            {
                parent_spostamento.transform.parent= destinazione.transform;
                parent_spostamento.transform.LeanRotate(destinazione.transform.rotation.eulerAngles, 3f).setOnComplete(() => 
                { 
                    parent_spostamento.LeanRotateAroundLocal(Vector3.up, -90, 3f);
                    foreach (var pianetino2d in planetario.GetComponentsInChildren<BoxCollider>())
                    {
                        pianetino2d.enabled = true;
                    }

                });
                
                module.RuotaSole();
                
            });

            //StartCoroutine(Segui());

            //parent_spostamento.LeanMove(destinazione.transform.position,10);
            
        }
       
    }

   
    


}
