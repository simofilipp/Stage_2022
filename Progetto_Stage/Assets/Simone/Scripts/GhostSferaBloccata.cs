using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSferaBloccata : MonoBehaviour
{
    bool ghostAttivo=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EarthRotationManager.instance.bloccata)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
