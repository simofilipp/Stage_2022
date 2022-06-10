using OVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialTriggerManager : MonoBehaviour
{
    [SerializeField] Transform earth;
    [SerializeField] Transform moon;
    [SerializeField] GameObject console;
    [SerializeField] GameObject tavolo;
    [SerializeField] GameObject tablet;
    [SerializeField] Transform padre;
    public SoundFXRef testSound1;
    //public SoundFXRef cassettoSuono;
    Vector3 earthScale;
    Vector3 moonScale;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.rotateAround(gameObject, transform.forward, -360f, 30f).setRepeat(-1);
        earthScale = earth.localScale;
        moonScale = moon.localScale;
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
            testSound1.PlaySound();
        }
    }

    void ActivateEarth()
    {
        console.GetComponent<Animator>().SetTrigger("Comparsa_tavolo");
        StartCoroutine(EnableTabletAndTable());
        EnableTabletAndTable();
        LeanTween.scale(padre.gameObject, padre.transform.localScale * 5, 3f).setEaseInOutQuart();
        //cassettoSuono.PlaySoundAt(console.transform.position);
        //earth.gameObject.SetActive(true);
        //earth.localScale = Vector3.zero;
        //LeanTween.scale(earth.gameObject, earthScale, 3f).setEaseInOutQuart().setOnComplete(() => 
        //{
        //    padre.gameObject.SetActive(false);
        //    moon.gameObject.SetActive(true);
        //    moon.localScale = Vector3.zero;
        //    LeanTween.scale(moon.gameObject, moonScale, 3f).setEaseInOutSine();
        //});
    }

    IEnumerator EnableTabletAndTable()
    {
        yield return new WaitForSeconds(4.1f);
        tavolo.SetActive(true);
        tablet.SetActive(true);
        tablet.LeanMove(new Vector3(0f, 1.02189505f, 1.00300002f), 2f).setEaseInOutQuart();
        tablet.LeanRotate(new Vector3(46f, 180f, 0), 3f).setEaseOutQuart();
    }
}
