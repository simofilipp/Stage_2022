using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotationManager : MonoBehaviour
{
    [SerializeField] Transform terra;
    [SerializeField] Transform cameraMain;
    Transform stato;

    int id;
    Vector3 terra_initial_scale;
    Quaternion terra_initial_rotation;
    // Start is called before the first frame update
    void Start()
    {
        terra_initial_rotation = terra.rotation;
        terra_initial_scale = terra.localScale;
        id=LeanTween.rotateZ(terra.gameObject, 360, 60f).id;
        Invoke("CancellaTween", 5f);
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
        var toCamera = Quaternion.LookRotation(cameraMain.position - terra.position);
        var toSite = Quaternion.LookRotation(stato.localPosition);
        var fromSite = Quaternion.Inverse(toSite);
        var rotazioneFinale = toCamera * fromSite;
        terra.LeanRotate(rotazioneFinale.eulerAngles, 1f);
        terra.LeanScale(terra_initial_scale * 0.8f, 0.5f).setOnComplete(() => { terra.LeanScale(terra_initial_scale * 1.2f, 0.5f); });

        //terra.rotation = rotazioneFinale;

    }

    void CancellaTween()
    {
        LeanTween.cancel(id);
    }
}
