using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotazioni_planetarium : MonoBehaviour
{
    public float time;
   
    float amplitude;

    float posX, poY, posZ;
    int id;

    private void Start()
    {
        amplitude = Mathf.Sqrt(((this.transform.localPosition.x) * (this.transform.localPosition.x)) + ((this.transform.localPosition.y) * (this.transform.localPosition.y)));
        MovimentoCircolare();
    }

    public void MovimentoCircolare()
    {
        id=LeanTween.value(0f, -2f*Mathf.PI, time).setOnUpdate((float value) => 
        {
            posX = 0 + Mathf.Cos(value) * amplitude;
            posZ = this.transform.localPosition.z;
            poY = 0 + Mathf.Sin(value) * amplitude;
            transform.localPosition = new Vector3(posX, poY, posZ);
        }).setRepeat(-1).id;
    }
    private void OnDestroy()
    {
        LeanTween.cancel(id);
    }
}
