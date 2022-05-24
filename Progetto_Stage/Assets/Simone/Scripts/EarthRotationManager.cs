using Oculus.Interaction;
using Oculus.Interaction.HandPosing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotationManager : MonoBehaviour
{
    [SerializeField] Transform terra;
    [SerializeField] Transform terraPerRicerca;
    [SerializeField] Transform cameraMain;
    [SerializeField] List<GameObject> sferette;


    Transform stato;

    int id;
    int counterSelezionato=0;
    Vector3 terra_initial_scale;
    Quaternion terraRicerca_initial_rotation;
    Quaternion terra_initial_rotation;


    public static EarthRotationManager _instance;
    public static EarthRotationManager instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<EarthRotationManager>();
            }
            return _instance;
        }
        set { _instance = value; }
    }

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) { Destroy(gameObject); return; };
    }

        void Start()
    {
        terraRicerca_initial_rotation = terraPerRicerca.rotation;
        //modificare in base a quanto si scala inizialmente la terra
        terra_initial_scale = terra.localScale*10;
        id=terra.LeanRotateAroundLocal(terra.up, -360, 60f).setLoopCount(-1).id;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RuotaTerra(string code)
    {
        StartCoroutine(RotazioneTerra(code));

    }

    IEnumerator RotazioneTerra(string code)
    {
        yield return new WaitForSeconds(0.1f);
        //alcuni stati hanno un codice diverso, filtrarli
        switch (code)
        {
            case "GB":
                code = "UK";
                break;
        }
        //Find non ottimizzato, tenere in considerazione di usare un loop per cercare nella lista del WorldMapManager
        stato = GameObject.Find(code).transform;
        var toCamera = Quaternion.LookRotation(cameraMain.position - terraPerRicerca.position);
        var toSite = Quaternion.LookRotation(stato.localPosition);
        var fromSite = Quaternion.Inverse(toSite);
        var rotazioneFinale = toCamera * fromSite;
        terraPerRicerca.LeanRotate(rotazioneFinale.eulerAngles, 1f);
        terra.LeanScale(terra_initial_scale * 0.8f, 0.5f).setOnComplete(() => { terra.LeanScale(terra_initial_scale * 1.2f, 0.5f); });

        //terra.rotation = rotazioneFinale;

    }

    public void CancellaTweenTerra()
    {
        LeanTween.pause(id);
        //salvo l'ultimo valore della terra per ricerca
        terraRicerca_initial_rotation = terraPerRicerca.rotation;
    }

    public void RiprendiTweenTerra()
    {
        terra.LeanScale(terra_initial_scale, 2f);
        //riporto in asse la terra per ricerca
        terraPerRicerca.LeanRotate(terraRicerca_initial_rotation.eulerAngles, 2f).setOnComplete(() => { LeanTween.resume(id); });
        
    }
    public void PausaTweenTerraSferette()
    {
        counterSelezionato += 1;
        if (counterSelezionato == 1)
        {
            LeanTween.pause(id);
            //salvo l'ultimo valore della terra
            terra_initial_rotation = terra.rotation;
        }
    }
    public void RiprendiTweenTerraSferette()
    {
        counterSelezionato -= 1;
        if(counterSelezionato == 0)
        {
            foreach(var sfera in sferette)
            {
                sfera.GetComponentInChildren<HandGrabInteractable>().enabled = false;
                sfera.GetComponentInChildren<GrabInteractable>().enabled = false;
            }
            terra.LeanScale(terra_initial_scale, 0.7f);
            //riporto in asse la terra
            terra.LeanRotate(terra_initial_rotation.eulerAngles, 0.7f).setOnComplete(() => { 
                LeanTween.resume(id);
                foreach (var sfera in sferette)
                {
                    sfera.GetComponentInChildren<HandGrabInteractable>().enabled = true;
                    sfera.GetComponentInChildren<GrabInteractable>().enabled = true;
                }
            });
        }
    }
}
