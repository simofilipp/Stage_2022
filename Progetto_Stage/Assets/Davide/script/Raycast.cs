using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Oculus.Interaction;
using System;

public class Raycast : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject canvasFinale;
    [SerializeField]
    GameObject _APIManager;
    private IInteractableView iiv;

    private void Awake()
    {
        //canvasFinale = GameObject.Find("CanvasFinale");
        iiv = GetComponent<IInteractableView>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void StatoSelezinato(InteractableStateChangeArgs obj)
    {
        //codice = stato_nome.name;
        //Debug.Log(codice);
        if(iiv.State == InteractableState.Select)
        {
            TMP_Text testo = canvasFinale.GetComponentInChildren<TMP_Text>();
            testo.text = this.GetComponent<Country>().Name;
            Debug.Log(GetComponent<Country>().Name);
            _APIManager.GetComponent<APIManager>().GetCountryData(GetComponent<Country>().Name);
        }

    }
    void OnEnable()
    {
        iiv.WhenStateChanged += StatoSelezinato;
    }

   
}
