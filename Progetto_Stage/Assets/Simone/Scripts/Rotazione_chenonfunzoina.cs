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
        //transform.LeanRotateAroundLocal(transform.up, -360, 10).setLoopCount(-1).setOnStart(() => { transform.LeanRotateAroundLocal(transform.right, -360, 10).setLoopCount(-1); });
        //transform.LeanRotate(new Vector3(360, 360, 360), 5);
        luna.LeanRotateAround(luna.transform.up, 360f, 6).setRepeat(-1);
        //parent_luna.LeanRotateAround(parent_luna.transform.forward, 360f, 10).setRepeat(-1);
        this.transform.LeanRotateAround(transform.up, -360f, 10).setRepeat(-1);
    }

    // Update is called once per frame
}
