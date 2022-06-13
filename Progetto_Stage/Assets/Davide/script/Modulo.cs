using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo : MonoBehaviour
{

    string nome;

    
    public Transform posizioneSuOrbita;

    
    public float velRotazioneOrbita;

    
    public float velRotazioneSelf;

   
    public float scalaFinale;

    public float direzioneOrbita;

    public bool afferrato;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwitchAfferrato()
    {
        afferrato = !afferrato;
    }
}
