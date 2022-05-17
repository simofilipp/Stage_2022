using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_rotazione : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.LeanRotateAround(transform.up,360f, 5f).setLoopCount(1);
        transform.LeanRotateAround(transform.right, 360f, 5f).setLoopCount(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
