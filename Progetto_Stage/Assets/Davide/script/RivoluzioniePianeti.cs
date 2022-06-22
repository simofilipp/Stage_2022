using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivoluzioniePianeti : MonoBehaviour
{
    public float time;
   
    float amplitude;

    float posX, posY, posZ;

    int id;


    private void Start()
    {
        amplitude = Mathf.Sqrt(((this.transform.localPosition.x) * (this.transform.localPosition.x)) + ((this.transform.localPosition.y) * (this.transform.localPosition.y)));
        MovimentoCircolare();
    }

    public void MovimentoCircolare()
    {
        id=LeanTween.value(0f, 2f*Mathf.PI, time).setOnUpdate((float value) => 
        {
            posX = 0 + Mathf.Cos(value) * amplitude;
            posY = this.transform.localPosition.y;
            posZ = 0 + Mathf.Sin(value) * amplitude;
            transform.localPosition = new Vector3(posX, posY, posZ);
        }).setRepeat(-1).id;
    }

    private void OnDestroy()
    {
        LeanTween.cancel(id);
    }
}
