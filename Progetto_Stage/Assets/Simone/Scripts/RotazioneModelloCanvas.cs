using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotazioneModelloCanvas : MonoBehaviour
{
    int id;
    // Start is called before the first frame update
    void Start()
    {
        id = this.transform.LeanRotateAround(Vector3.up, -360f, 20f).setRepeat(-1).id;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        LeanTween.cancel(id);
    }
}
