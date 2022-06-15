using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotazioni_planetarium : MonoBehaviour
{
    public float frequency;
    public Transform centro;
   
    float amplitude;

    float posX, poY, posZ, angle = 0;


    private void Start()
    {
        amplitude = Mathf.Sqrt(((this.transform.localPosition.x) * (this.transform.localPosition.x)) + ((this.transform.localPosition.y) * (this.transform.localPosition.y)));
    }

    private void Update()
    {
        posX = 0 + Mathf.Cos(angle) * amplitude;
        posZ = this.transform.localPosition.z;
        poY = 0 + Mathf.Sin(angle) * amplitude;
        transform.localPosition = new Vector3(posX, poY, posZ);
        angle = angle + Time.deltaTime * frequency;
        if (angle >= 360f)
        {
            angle = 0f;
        }
    }
}
