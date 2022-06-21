using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuropeVisualizer : MonoBehaviour
{
    [SerializeField] GameObject earth;
    [SerializeField] GameObject europa;
    [SerializeField] List<GameObject> optionButtons;
    bool mapShown=false;
    Vector3 initialScaleEarth;
    [SerializeField]
    Istanzia_istogrammi istanziaIsto;
    bool istoIstanziati;
    [SerializeField] List<GameObject>europeButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCloseEuropeMap()
    {
        if (!mapShown)
        {
            foreach (GameObject go in optionButtons)
            {
                if (go != null)
                {
                    go.SetActive(false);
                }
            }
            initialScaleEarth = earth.transform.localScale;
            earth.transform.LeanScale(Vector3.zero, 3f).setEaseInBack().setEaseOutQuad().setOnComplete(()=> 
            {
                europa.SetActive(true);
                foreach(var bottone in europeButtons)
                {
                    bottone.SetActive(true);
                }
                //istanziaIsto.ColoraStatiEU();
                //if (!istoIstanziati)
                //{
                //    istanziaIsto.IstanziaIstoEuropa();
                //    istoIstanziati = true;
                //}
               

            });
            mapShown=true;
        }
        else
        {
            foreach (var bottone in europeButtons)
            {
                bottone.SetActive(false);
            }
            europa.SetActive(false);
            earth.transform.LeanScale(initialScaleEarth, 3f).setEaseOutQuad().setOnComplete(() => 
            { 
                foreach(GameObject go in optionButtons)
                {
                    if(go != null)
                    {
                        go.SetActive(true);
                    }
                }
            });
            mapShown = false;
        }
    }
}
