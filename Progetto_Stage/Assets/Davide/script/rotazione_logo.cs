using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotazione_logo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.LeanRotateAroundLocal(Vector3.up, -360, 27f).setRepeat(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
