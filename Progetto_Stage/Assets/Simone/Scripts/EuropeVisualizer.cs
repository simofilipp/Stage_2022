using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuropeVisualizer : MonoBehaviour
{
    [SerializeField] GameObject earth;
    [SerializeField] List<GameObject> optionButtons;
    bool mapShown=false;
    Vector3 initialScaleEarth;

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
            earth.transform.LeanScale(Vector3.zero, 3f).setEaseInBack().setEaseOutQuad();
            mapShown=true;
        }
        else
        {
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
