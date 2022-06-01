using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sferette")
        {
            Debug.Log("Sono dentro");
            Rigidbody my_rigidbody = other.GetComponent<Rigidbody>();
            Debug.Log(my_rigidbody.name);
            my_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            my_rigidbody.useGravity = false;
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sferette")
        {
            Debug.Log("Sono fuori");
            Rigidbody my_rigidbody = other.GetComponent<Rigidbody>();
            my_rigidbody.constraints = RigidbodyConstraints.None;
            my_rigidbody.useGravity = true;
        }
           
        
    }
    //public void FreezSferetta()
    //{
    //    Debug.Log("Sono dentro");
    //    Rigidbody my_rigidbody = GameObject.Find("Sferetta").GetComponent<Rigidbody>();
    //    Debug.Log(my_rigidbody.name);
       
        
    //    my_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    //    my_rigidbody.useGravity = false;
    //}

}
