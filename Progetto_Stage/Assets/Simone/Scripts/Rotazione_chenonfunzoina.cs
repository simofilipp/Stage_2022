using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotazione_chenonfunzoina : MonoBehaviour
{
    [SerializeField] GameObject luna;
    //[SerializeField] GameObject parent_luna;

    // Start is called before the first frame update
    void Start()
    {
        //applico una rotazione sia alla luna che ad essa attorno alla terra
        luna.LeanRotateAround(luna.transform.up, -360f, 100f).setRepeat(-1);
        this.transform.LeanRotateAround(transform.up, -360f, 100f).setRepeat(-1);
    }

    // Update is called once per frame
}
