using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroidi_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.rotateAroundLocal(this.gameObject, Vector3.forward, -360, 1200f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
