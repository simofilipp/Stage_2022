using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivoluzioniePianeti : MonoBehaviour
{
    public float time;
   
    float amplitude;

    float posX, posY, posZ;


    private void Start()
    {
        amplitude = Mathf.Sqrt(((this.transform.localPosition.x) * (this.transform.localPosition.x)) + ((this.transform.localPosition.y) * (this.transform.localPosition.y)));
        MovimentoCircolare();
    }

    public void MovimentoCircolare()
    {
        LeanTween.value(0f, 2f*Mathf.PI, time).setOnUpdate((float value) => 
        {
            posX = 0 + Mathf.Cos(value) * amplitude;
            posY = this.transform.localPosition.y;
            posZ = 0 + Mathf.Sin(value) * amplitude;
            transform.localPosition = new Vector3(posX, posY, posZ);
        }).setRepeat(-1);
    }
   
}
