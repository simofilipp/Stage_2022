using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class RayRotationManager : MonoBehaviour
{

    [SerializeField] MeshRenderer cursorDX;
    [SerializeField] MeshRenderer cursorSX;

    bool grabbedDX=false;
    bool grabbedSX=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger) && cursorSX.enabled && !grabbedDX)
        {
            grabbedSX = true;
            Debug.Log("Grab sinstro premuto");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger) && cursorDX.enabled && !grabbedSX)
        {
            grabbedDX = true;
            Debug.Log("Grab destro premuto");
        }
        if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
        {
            grabbedSX = false;
            Debug.Log("Grab sinistro rilasciato");
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
        {
            grabbedDX = false;
            Debug.Log("Grab destro rilasciato");
        }
    }

    public void StartRayRotation()
    {
        StartCoroutine(RayRotation());
    }

    IEnumerator RayRotation()
    {
        
        while(grabbedSX || grabbedDX)
        {
            Debug.Log("Sta ruotando");
            //if (grabbedDX)
            //    Debug.Log("Sto ruotando con la DX");
            //if (grabbedSX)
            //    Debug.Log("Sto ruotando con la SX");
            yield return null;
        }
    }
}
