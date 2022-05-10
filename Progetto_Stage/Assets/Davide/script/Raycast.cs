using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject stato_nome;
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StatoSelezinato(string codice)
    {
        codice = stato_nome.name;
        Debug.Log(codice);

    }
}
