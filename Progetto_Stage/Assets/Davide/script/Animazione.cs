using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animazione : MonoBehaviour
{
    [SerializeField]
    GameObject console;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Console_animazione_apertura()
    {
        console.GetComponent<Animator>().SetTrigger("apertura");
    }
}
