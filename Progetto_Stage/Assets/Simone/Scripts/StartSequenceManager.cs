using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequenceManager : MonoBehaviour
{
    //[SerializeField] GameObject cassetto;
    [SerializeField] GameObject pannelli;
    [SerializeField] float dissolveTimeGabbia;
    [SerializeField] float dissolveTimeSfere;
    [SerializeField] GameObject ologrammiMode;
    [SerializeField] GameObject ponte;
    [SerializeField] GameObject tastiera;
    [SerializeField] GameObject canvasFinale;
    [SerializeField] GameObject terra;
    [SerializeField] GameObject terraNoStati;
    [SerializeField] GameObject luna;
    [SerializeField] GameObject sole;
    [SerializeField] GameObject planetario2D;
    [SerializeField] GameObject holoEarth;
    [SerializeField] GameObject puntoRilascioModuli;
    [SerializeField] GameObject serranda;
    [SerializeField] GameObject puntaStatica;
    [SerializeField] GameObject tavolo_scifi;
    [SerializeField] GameObject bottoneSpegniTavolo;
    [SerializeField] GameObject tastoSTART;
    [SerializeField] List<GameObject> sferette;
    [SerializeField] List<GameObject> tastiModalita;
    [SerializeField] List<GameObject> tastiOpzioniFreeMode;
    [SerializeField] List<GameObject> tastiOpzioniEarthSystem;
    [SerializeField] List<GameObject> tastiOpzioniSolarSystem;
    [SerializeField] List<GameObject> tastiPlanetarioSubmode;
    [SerializeField] Istanzia_istogrammi ii;
    Vector3 opzioniinitialScale;

    bool bottoniPianetiAccesi;



    Mode actualMode;
    private Vector3 moonScale;

    private void Start()
    {
        actualMode = Mode.Initial;
        moonScale=luna.transform.localScale;
    }

    public void StartExperience()
    {
        NascondiBottone(tastoSTART);
        foreach(var tasto in tastiModalita)
        {
            GeneraBottone(tasto);
        }
        ologrammiMode.SetActive(true);
        StartCoroutine(SpawnHolograms());
    
    }

    public void StartFreeMode()
    {
        //disattivare i tasti modalità dopo averli scalati, aggiungere un delay ad ugnuno per farlo più carino
        DisattivaTastiMode();
        actualMode = Mode.FreeMode;

        CheckHologramMode();

        //far scomparire gli ologrammi delle altre modalità e spostare quello corretto al centro
        //al momento spengiamo tutto


        //apri serranda
        serranda.GetComponent<Animator>().SetTrigger("Apri");

        ponte.SetActive(false);
        var solve = pannelli.GetComponent<MeshRenderer>().material;
        LeanTween.value(-0.2f, 1f, dissolveTimeGabbia).setOnUpdate((float value) =>
        {
            solve.SetFloat("_Dissolvenza_animazione", value);

        }).setOnComplete(() =>
        {
            pannelli.SetActive(false);
            //lasciare attivo solo il tasto per tornare alla fase iniziale

            ologrammiMode.SetActive(false);


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
                terra.transform.LeanScale(new Vector3(8, 8, 8), 5f).setEaseInOutQuart().setOnComplete(() =>
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
                        GeneraBottone(t);
                    }

                }).delay = 0.1f;


            }
                );
        });

    }
    public void StartPlanetariumMode()
    {
        actualMode = Mode.PlanetariumMode;
        CheckHologramMode();
        DisattivaTastiMode();
        foreach (var t in tastiPlanetarioSubmode)
        {
            GeneraBottone(t);
        }

        //far scomparire gli ologrammi delle altre modalità e spostare quello corretto al centro

    }


    public void StartSolarSystem()
    {
        actualMode = Mode.SolarSystemMode;
        ologrammiMode.SetActive(false);
        DisattivaTastiSubMode();
        bottoneSpegniTavolo.SetActive(false);
        ponte.SetActive(false);
        var solve = pannelli.GetComponent<MeshRenderer>().material;
        LeanTween.value(-0.2f, 1f, dissolveTimeGabbia).setOnUpdate((float value) =>
        {
            solve.SetFloat("_Dissolvenza_animazione", value);

        }).setOnComplete(() =>
        {
            pannelli.SetActive(false);
            foreach (var t in tastiOpzioniSolarSystem)
            {
                GeneraBottone(t);

            }
            holoEarth.LeanScale(Vector3.zero, 2f).setOnComplete(() =>
            {
                //attivo sole e planetario 2D
                sole.SetActive(true);
                var solveSole = sole.transform.GetChild(0).GetComponent<MeshRenderer>().material;
                LeanTween.value(-0.2f, 1.5f, 3f).setOnUpdate((float value) =>
                {
                    solveSole.SetFloat("_Dissolvenza_animazione", value);
                    bottoneSpegniTavolo.SetActive(true);
                    tavolo_scifi.SetActive(true);

                });
                planetario2D.SetActive(true);
                holoEarth.SetActive(false);
                puntaStatica.SetActive(false);
                //attivo tasti pianeti
                opzioniinitialScale = tastiOpzioniSolarSystem[0].transform.localScale;
               
                bottoniPianetiAccesi = true;
            });
        });
    }
    public void StartEarthSystem()
    {
        actualMode = Mode.EarthSystemMode;
        ologrammiMode.SetActive(false);
        DisattivaTastiSubMode();
        ponte.SetActive(false);
        var solve = pannelli.GetComponent<MeshRenderer>().material;
        LeanTween.value(-0.2f, 1f, dissolveTimeGabbia).setOnUpdate((float value) =>
        {
            solve.SetFloat("_Dissolvenza_animazione", value);

        }).setOnComplete(() =>
        {
            pannelli.SetActive(false);
            holoEarth.LeanScale(Vector3.zero, 2f).setOnComplete(() =>
            {
                terraNoStati.SetActive(true);
                terraNoStati.transform.localScale = Vector3.zero;
                holoEarth.SetActive(false);
                terraNoStati.transform.LeanScale(new Vector3(8, 8, 8), 5f).setEaseInOutQuart().setOnComplete(() =>
                {
                    luna.SetActive(true);
                    luna.transform.localScale = Vector3.zero;
                    LeanTween.scale(luna.gameObject, moonScale, 3f).setEaseInOutSine();
                    //attivare i tasti opzione
                    foreach (var t in tastiOpzioniEarthSystem)
                    {
                        GeneraBottone(t);
                    }
                    
                    //attivare punto rilascio
                    //puntoRilascioModuli.SetActive(true);
                });
            });
        });
    }

    private void DisattivaTastiMode()
    {
        foreach (var t in tastiModalita)
        {
            //Spengo il poke, ricordarsi di accenderlo nel caso si voglia tornare alla scelta delle modalità
            t.GetComponent<PokeInteractable>().enabled = false;
            NascondiBottone(t);
        }
    }


    private void DisattivaTastiSubMode()
    {
        foreach (var t in tastiPlanetarioSubmode)
        {
            t.GetComponent<PokeInteractable>().enabled = false;
            NascondiBottone(t);
        }
    }

    public void GeneraBottone(GameObject t)
    {
        var initialButtonScale = t.transform.localScale;
        t.transform.localScale = new Vector3(0,0,t.transform.localScale.z);
        t.SetActive(true);
        t.LeanScale(initialButtonScale, 0.5f).setEaseOutBack();//.setOnComplete(() => { t.transform.GetChild(1).GetChild(0).LeanMoveLocalZ(-0.3f, 0.5f); });
        
    }
    
    public void GeneraBottone(GameObject t, float time)
    {
        var initialButtonScale = t.transform.localScale;
        t.transform.localScale = new Vector3(0,0,t.transform.localScale.z);
        t.SetActive(true);
        t.LeanScale(initialButtonScale, time).setEaseOutBack();//.setOnComplete(() => { t.transform.GetChild(1).GetChild(0).LeanMoveLocalZ(-0.3f, 0.5f); });
        
    }
    public void NascondiBottone(GameObject t)
    {
        t.LeanScale(new Vector3(0, 0, t.transform.localScale.z), 0.5f).setEaseInBack().setOnComplete(() => { t.gameObject.SetActive(false); });
    }

    public void SwitchPlanetButton()
    {
        if (actualMode == Mode.SolarSystemMode)
        {
            if (bottoniPianetiAccesi)
            {
                foreach(var bot in tastiOpzioniSolarSystem)
                {
                    NascondiBottone(bot);
                }
                bottoniPianetiAccesi = false;
            }
            else
            {
                foreach (var bot in tastiOpzioniSolarSystem)
                {
                   
                   bot.SetActive(true);
                   bot.LeanScale(opzioniinitialScale, 0.5f).setEaseOutBack();
                }
                bottoniPianetiAccesi = true;
            }
        }
    }
    IEnumerator SpawnHolograms()
    {
        for(int i = 0; i < ologrammiMode.transform.childCount; i++)
        {
            GeneraBottone(ologrammiMode.transform.GetChild(i).gameObject, 2);
            yield return new WaitForSeconds(1f);
        }
    }

    void CheckHologramMode()
    {
        switch (actualMode)
        {
            case Mode.FreeMode:
                //spegnere ologrammi diversi e spostare questo al centro
                ologrammiMode.transform.GetChild(0).gameObject.SetActive(false);
                ologrammiMode.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case Mode.PlanetariumMode:
                //spegnere ologrammi diversi e spostare questo al centro
                ologrammiMode.transform.GetChild(0).gameObject.SetActive(false);
                ologrammiMode.transform.GetChild(1).gameObject.SetActive(false);
                break;
        }
    }
}



public enum Mode
{
    Initial,
    FreeMode,
    PlanetariumMode,
    SolarSystemMode,
    EarthSystemMode
}
