using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPlanetarium : MonoBehaviour
{
    [SerializeField] GameObject iss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableISS()
    {
        iss.SetActive(true);
    }
}
