using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianetiManager : MonoBehaviour
{
    public List<Modulo> pianeti_modulo;
    [SerializeField] LancioModuli lancio_moduli;
    [SerializeField] GameObject radarPlanetario;
    [SerializeField] GameObject canvasPianeti;
    [SerializeField] GameObject orbite;
    [SerializeField] GameObject luna;
    Vector3 luna_initial_scale;
    bool leanSclaInCorso;
    bool rotazioneRadar;

    // Start is called before the first frame update
    void Start()
    {
        luna_initial_scale = luna.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scala1X()
    {
        if(LancioModuli.activeOrbitScale != 1 && leanSclaInCorso == false)
        {
            
            LancioModuli.activeOrbitScale = 1;
            foreach(Modulo modulo in pianeti_modulo)
            {
                if (modulo.isInOrbit)
                {
                    leanSclaInCorso = true;
                    LeanTween.scale(modulo.gameObject, modulo.scalaBase, .2f).setEaseOutQuart().setOnComplete(()=>
                    {
                        leanSclaInCorso = false;
                    });
                }
            }
        }
    }
    public void Scala2X()
    {
        if (LancioModuli.activeOrbitScale != 3 && leanSclaInCorso == false)
        {
            
            LancioModuli.activeOrbitScale = 3;
            foreach (Modulo modulo in pianeti_modulo)
            {
                if (modulo.isInOrbit)
                {
                    leanSclaInCorso = true;
                    LeanTween.scale(modulo.gameObject, modulo.scalaBase*3, .2f).setEaseOutQuart().setOnComplete(() =>
                    {
                        leanSclaInCorso = false;
                    });
                }
            }
        }
    }
    public void Scala3X()
    {
        if (LancioModuli.activeOrbitScale != 5 && leanSclaInCorso == false)
        {
            
            LancioModuli.activeOrbitScale = 5;
            foreach (Modulo modulo in pianeti_modulo)
            {
                if (modulo.isInOrbit)
                {
                    leanSclaInCorso = true;
                    LeanTween.scale(modulo.gameObject, modulo.scalaBase*5, .2f).setEaseOutQuart().setOnComplete(() =>
                    {
                        leanSclaInCorso = false;
                    });
                }
            }
        }
    }

    public void RuotaPlanetario()
    {
        if (!rotazioneRadar)
        {
            rotazioneRadar = true;
            LeanTween.rotateAroundLocal(radarPlanetario, Vector3.forward, 180, 2f).setOnComplete(() => { rotazioneRadar = false; });
        }
    }

    public void AccendiSpegniCanvas()
    {
        canvasPianeti.SetActive(!canvasPianeti.activeSelf);
    }

    public void AccendiSpegniOrbite()
    {
        orbite.SetActive(!orbite.activeSelf);
    }

    public void ScalaLuna(int scala)
    {
        luna.transform.localScale = luna_initial_scale *scala;
    }
}
