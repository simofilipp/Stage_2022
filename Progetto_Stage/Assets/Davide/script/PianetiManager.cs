using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianetiManager : MonoBehaviour
{
    public List<Modulo> pianeti_modulo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scala1X()
    {
        foreach(Modulo modulo in pianeti_modulo)
        {
            if (modulo.isInOrbit)
            {
                LeanTween.scale(modulo.gameObject, modulo.scalaBase, 3).setEaseOutQuart();
            }
        }
    }
    public void Scala2X()
    {
        foreach (Modulo modulo in pianeti_modulo)
        {
            if (modulo.isInOrbit)
            {
                LeanTween.scale(modulo.gameObject, modulo.scalaBase*3, 3).setEaseOutQuart();
            }
        }
    }
    public void Scala3X()
    {
        foreach (Modulo modulo in pianeti_modulo)
        {
            if (modulo.isInOrbit)
            {
                LeanTween.scale(modulo.gameObject, modulo.scalaBase*5, 3).setEaseOutQuart();
            }
        }
    }
}
