using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotationManager : MonoBehaviour
{
    [SerializeField] Transform terra;
    [SerializeField] Transform terraPerRicerca;
    [SerializeField] Transform cameraMain;
    Transform stato;

    int id;
    Vector3 terra_initial_scale;
    Quaternion terraRicerca_initial_rotation;

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

        // Start is called before the first frame update
        void Start()
    {
        terraRicerca_initial_rotation = terraPerRicerca.rotation;
        terra_initial_scale = terra.localScale;
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
        terraRicerca_initial_rotation = terraPerRicerca.rotation;
    }

    public void RiprendiTweenTerra()
    {
        terra.LeanScale(terra_initial_scale, 2f);
        terraPerRicerca.LeanRotate(terraRicerca_initial_rotation.eulerAngles, 2f).setOnComplete(() => { LeanTween.resume(id); });
        
    }
}