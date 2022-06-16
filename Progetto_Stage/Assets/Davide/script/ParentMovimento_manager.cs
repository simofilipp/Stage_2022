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

    bool arrivato;

    int id;



    [SerializeField]
    GameObject destinazione;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

            StartCoroutine(Segui());

            //parent_spostamento.LeanMove(destinazione.transform.position,10);
            
        }
        if (other.gameObject.tag == "arrivato")
        {
            parent_spostamento.transform.parent = destinazione.transform;
            arrivato = true;
        }
    }

   
    IEnumerator Segui()
    {
        Debug.Log("sono in coroutines");
        id = parent_spostamento.LeanMove(destinazione.transform.position, 0.1f).id;
        yield return null;
        do
        {
            LeanTween.cancel(id);
            id= parent_spostamento.LeanMove(destinazione.transform.position,0.1f).id;
            if(parent_spostamento.transform.position== destinazione.transform.position)
            {
                arrivato = true;
                
            }
            yield return null;
        }
        while (!arrivato);
        
      
        
    }


}
