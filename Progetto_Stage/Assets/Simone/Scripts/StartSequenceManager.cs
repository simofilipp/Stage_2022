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
    [SerializeField] List<GameObject> tastiFreeModeGioco;
    [SerializeField] Istanzia_istogrammi ii;
    [SerializeField] GameObject tavolo;
    [SerializeField] GameObject logo;
    Vector3 opzioniinitialScale;
   

    bool bottoniPianetiAccesi;



    public Mode actualMode;
    private Vector3 moonScale;

    private void Start()
    {
        actualMode = Mode.Initial;
        moonScale=luna.transform.localScale;
        opzioniinitialScale = tastiOpzioniFreeMode[0].transform.localScale;
    }

    public void StartExperience()
    {

        NascondiBottone(tastoSTART);
        NascondiBottone(logo);

        ologrammiMode.SetActive(true);
        StartCoroutine(SpawnHolograms());
    
    }

    public void StartFreeMode()
    {
        //disattivare i tasti modalit� dopo averli scalati, aggiungere un delay ad ugnuno per farlo pi� carino
        DisattivaTastiMode();
        actualMode = Mode.FreeMode;

        CheckHologramMode();

        //far scomparire gli ologrammi delle altre modalit� e spostare quello corretto al centro
        //al momento spengiamo tutto

        //dissolve e animazione tavolo
        tavolo.GetComponent<tavolo_script>().CompariTavolo();

        //apri serranda
        serranda.GetComponent<Animator>().SetTrigger("Apri");

        ponte.SetActive(false);
        var solve = pannelli.GetComponent<MeshRenderer>().material;

        //LeanTween.value(-0.2f, 1f, dissolveTimeGabbia).setOnUpdate((float value) =>
        //{
        //    solve.SetFloat("_Dissolvenza_animazione", value);

        //})

        pannelli.LeanColor(Color.black,dissolveTimeGabbia)
            .setOnComplete(() =>
        {
            pannelli.SetActive(false);
            //lasciare attivo solo il tasto per tornare alla fase iniziale

            ologrammiMode.SetActive(false);



            //animazione terra che si ingrandisce, luna che scompare

            //holoEarth.LeanScale(Vector3.zero, 2f).setOnComplete(() =>
            //{
                terra.SetActive(true);
                terra.transform.localScale = Vector3.zero;
                holoEarth.SetActive(false);
                terra.transform.LeanScale(new Vector3(8, 8, 8), 5f).setEaseInOutQuart().setOnComplete(() =>
                {
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


            //}
            //    );
        });

    }

    public void StartGiocoFreeMode()
    {
       foreach(var tasto in tastiOpzioniFreeMode)
        {
            NascondiBottone(tasto);
        }
       foreach(var tastoGioco in tastiFreeModeGioco)
        {
            GeneraBottone(tastoGioco,opzioniinitialScale);
        }

    }
    public void QuitGiocoFreeMode()
    {
        foreach (var tasto in tastiFreeModeGioco)
        {
            NascondiBottone(tasto);
        }
        //da mettere in un set on complete
        foreach (var tastoOpzione in tastiOpzioniFreeMode)
        {
            GeneraBottone(tastoOpzione,opzioniinitialScale);
        }

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

        //far scomparire gli ologrammi delle altre modalit� e spostare quello corretto al centro

    }


    public void StartSolarSystem()
    {
        actualMode = Mode.SolarSystemMode;
        ologrammiMode.SetActive(false);
        DisattivaTastiSubMode();
        bottoneSpegniTavolo.SetActive(false);
        ponte.SetActive(false);
        var solve = pannelli.GetComponent<MeshRenderer>().material;

        //LeanTween.value(-0.2f, 1f, dissolveTimeGabbia).setOnUpdate((float value) =>
        //{
        //    solve.SetFloat("_Dissolvenza_animazione", value);

        //})

        pannelli.LeanColor(Color.black, dissolveTimeGabbia)
    .setOnComplete(() =>
        {
            pannelli.SetActive(false);

            //dissolve e animazione tavolo
            tavolo.GetComponent<tavolo_script>().CompariTavolo();

            opzioniinitialScale = tastiOpzioniSolarSystem[0].transform.localScale;
            foreach (var t in tastiOpzioniSolarSystem)
            {
                GeneraBottone(t);

            }
            //holoEarth.LeanScale(Vector3.zero, 2f).setOnComplete(() =>
            //{
                //attivo sole e planetario 2D
                sole.SetActive(true);
                var solveSole = sole.transform.GetChild(0).GetComponent<MeshRenderer>().material;
                LeanTween.value(-0.2f, 1.5f, 3f).setOnUpdate((float value) =>
                {
                    solveSole.SetFloat("_Dissolvenza_animazione", value);
                    bottoneSpegniTavolo.SetActive(true);
                    tavolo_scifi.SetActive(true);

                });
                
                holoEarth.SetActive(false);
                puntaStatica.SetActive(false);
                //attivo tasti pianeti
               
                bottoniPianetiAccesi = true;
            //});
        });
    }
    public void StartEarthSystem()
    {
        actualMode = Mode.EarthSystemMode;
        ologrammiMode.SetActive(false);
        DisattivaTastiSubMode();
        ponte.SetActive(false);
        var solve = pannelli.GetComponent<MeshRenderer>().material;

        //LeanTween.value(-0.2f, 1f, dissolveTimeGabbia).setOnUpdate((float value) =>
        //{
        //    solve.SetFloat("_Dissolvenza_animazione", value);

        //})

        pannelli.LeanColor(Color.black, dissolveTimeGabbia)
.setOnComplete(() =>
        {
            pannelli.SetActive(false);
            //holoEarth.LeanScale(Vector3.zero, 2f).setOnComplete(() =>
            //{
                terraNoStati.SetActive(true);
                terraNoStati.transform.localScale = Vector3.zero;
                holoEarth.SetActive(false);

                //dissolve e animazione tavolo
                tavolo.GetComponent<tavolo_script>().CompariTavolo();

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
            //});
        });
    }

    private void DisattivaTastiMode()
    {
        foreach (var t in tastiModalita)
        {
            //Spengo il poke, ricordarsi di accenderlo nel caso si voglia tornare alla scelta delle modalit�
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

     public void GeneraBottone(GameObject t,Vector3 scalaFinale)
    {
        
        t.transform.localScale = new Vector3(0,0,t.transform.localScale.z);
        t.SetActive(true);
        t.LeanScale(scalaFinale, 0.5f).setEaseOutBack();//.setOnComplete(() => { t.transform.GetChild(1).GetChild(0).LeanMoveLocalZ(-0.3f, 0.5f); });
        
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
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < ologrammiMode.transform.childCount; i++)
        {
            GeneraBottone(ologrammiMode.transform.GetChild(i).gameObject, .3f);
            yield return null;
            //yield return new WaitForSeconds(1f);
        }
        foreach (var tasto in tastiModalita)
        {
            GeneraBottone(tasto, 1);
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
                //implementare modo per far partire tutto in sequenza di questo leantween
                ologrammiMode.transform.GetChild(1).LeanMoveLocal(Vector3.zero, 1f).setEaseOutQuad();

                break;
            case Mode.PlanetariumMode:
                //spegnere ologrammi diversi e spostare questo al centro
                ologrammiMode.transform.GetChild(0).gameObject.SetActive(false);
                ologrammiMode.transform.GetChild(1).gameObject.SetActive(false);
                ologrammiMode.transform.GetChild(2).LeanMoveLocal(Vector3.zero, 1f).setEaseOutQuad();
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
