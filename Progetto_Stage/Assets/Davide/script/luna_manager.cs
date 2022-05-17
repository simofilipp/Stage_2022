using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luna_manager : MonoBehaviour
{
    [SerializeField]
    GameObject luna;
    
    // Start is called before the first frame update
    void Start()
    {
        luna.transform.LeanRotateAround(luna.transform.up, 360,7).setLoopCount(-1);
        transform.LeanRotateAround(transform.right, 360, 10).setLoopCount(-1);
        transform.LeanRotateAround(transform.forward, 360, 20).setLoopCount(-1);

    }

   
}
