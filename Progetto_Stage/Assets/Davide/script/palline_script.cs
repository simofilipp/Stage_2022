using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class palline_script : MonoBehaviour
{
    public WorldMapManager.State stato;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Cambiastato_Terra()
    {
        WorldMapManager._instance.CurrentState = stato;
    }
}
