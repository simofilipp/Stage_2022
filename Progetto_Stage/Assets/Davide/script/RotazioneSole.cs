using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotazioneSole : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.LeanRotateAroundLocal(Vector3.forward, -360, 5f).setRepeat(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
