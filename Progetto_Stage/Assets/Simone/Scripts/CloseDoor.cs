using OVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    [SerializeField] Animator porta;
    public SoundFXRef portaSuono;
    bool portaChiusa=false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<MeshRenderer>().isVisible && !portaChiusa)
        {
            portaChiusa = true;
            porta.SetTrigger("Close");
            portaSuono.PlaySoundAt(transform.position);
        }
    }
}
