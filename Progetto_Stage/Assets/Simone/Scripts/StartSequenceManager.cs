using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequenceManager : MonoBehaviour
{
    [SerializeField] GameObject cassetto;
    [SerializeField] GameObject pannelli;
    [SerializeField] float dissolveTimeGabbia;
    [SerializeField] float dissolveTimeSfere;
    [SerializeField] GameObject ponte;
    [SerializeField] GameObject tastiera;
    [SerializeField] GameObject canvasFinale;
    [SerializeField] GameObject terra;
    [SerializeField] GameObject luna;
    [SerializeField] GameObject holoEarth;
    [SerializeField] GameObject serranda;
    [SerializeField] List<GameObject> sferette;
    [SerializeField] List<GameObject> tastiModalita;
    [SerializeField] List<GameObject> tastiOpzioniFreeMode;
    [SerializeField] List<GameObject> tastiPlanetarioSubmode;
    [SerializeField] Istanzia_istogrammi ii;


    Mode actualMode;

    private void Start()
    {
        actualMode = Mode.Initial;
    }

    public void StartFreeMode()
    {
        //disattivare i tasti modalit� dopo averli scalati, aggiungere un delay ad ugnuno per farlo pi� carino
        DisattivaTastiMode();
        actualMode = Mode.FreeMode;

        //apri serranda
        serranda.GetComponent<Animator>().SetTrigger("Apri");

        ponte.SetActive(false);
        var solve = pannelli.GetComponent<MeshRenderer>().material;
        LeanTween.value(-0.2f, 1f, dissolveTimeGabbia).setOnUpdate((float value) =>
        {
            solve.SetFloat("_Dissolvenza_animazione", value);

        }).setOnComplete(() =>
        {

            //lasciare attivo solo il tasto per tornare alla fase iniziale


            //Animazione entrata palline con dissolve
            foreach (var obj in sferette)
            {
                obj.SetActive(true);

                var solveSfera = obj.GetComponentsInChildren<MeshRenderer>()[0].material;
                LeanTween.value(1f, -0.2f, dissolveTimeSfere).setOnUpdate((float value) =>
                {
                    solveSfera.SetFloat("_Dissolvenza_animazione", value);
                });
            }

            //animazione terra che si ingrandisce, luna che scompare
            holoEarth.LeanScale(Vector3.zero, 2f).setOnComplete(() =>
            {
                terra.SetActive(true);
                terra.transform.localScale = Vector3.zero;
                holoEarth.SetActive(false);
                terra.transform.LeanScale(new Vector3(10, 10, 10), 5f).setEaseInOutQuart().setOnComplete(() =>
                {
                    luna.SetActive(false);

                    //attivare tastiera
                    tastiera.SetActive(true);

                    //attivare e scalare canvas
                    Vector3 canvasScale = canvasFinale.transform.localScale;
                    canvasFinale.transform.localScale = Vector3.zero;
                    canvasFinale.SetActive(true);
                    canvasFinale.transform.LeanScale(canvasScale, 1.5f).setEaseOutBack();

                    //istanziare cubetti capitali
                    ii.IstanziaBaseIstogrammi();
                    //attivare i tasti opzione
                    foreach (var t in tastiOpzioniFreeMode)
                    {
                        t.gameObject.SetActive(true);
                    }

                }).delay = 0.1f;


            }
                );
        });

    }
    public void StartPlanetariumMode()
    {
        DisattivaTastiMode();
        foreach (var t in tastiPlanetarioSubmode)
        {
            var initialButtonScale= t.transform.localScale;
            t.transform.localScale = Vector3.zero;
            t.SetActive(true);
            t.LeanScale(initialButtonScale, 0.5f).setEaseOutBack().setOnComplete(() => { });
        }

    }

    private void DisattivaTastiMode()
    {
        foreach (var t in tastiModalita)
        {
            t.LeanScale(Vector3.zero, 0.5f).setEaseInBack().setOnComplete(() => { t.gameObject.SetActive(false); });
        }
    }
}

public enum Mode
{
    Initial,
    FreeMode,
    StoryMode
}
