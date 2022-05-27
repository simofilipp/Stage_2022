using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialTriggerManager : MonoBehaviour
{
    [SerializeField] Transform earth;
    [SerializeField] Transform moon;
    [SerializeField] GameObject cassetto;
    [SerializeField] Transform padre;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.rotateAround(gameObject, transform.forward, -360f, 30f).setRepeat(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "InitialInteractor")
        {
            LeanTween.move(padre.gameObject, earth.position, 5f).setEaseInOutQuart().setOnComplete(ActivateEarth);
        }
    }

    void ActivateEarth()
    {
        cassetto.GetComponent<Animator>().SetTrigger("scomparsa");
        earth.gameObject.SetActive(true);
        Vector3 earthScale= earth.localScale;
        Vector3 moonScale= moon.localScale;
        earth.localScale = Vector3.zero;
        earth.localScale = Vector3.zero;
        LeanTween.scale(earth.gameObject, earthScale, 3f).setEaseInOutQuart().setOnComplete(() => 
        {
            padre.gameObject.SetActive(false);
            moon.localScale = Vector3.zero;
            moon.gameObject.SetActive(true);
            LeanTween.scale(moon.gameObject, moonScale, 3f).setEaseInOutSine();
        });
    }
}
