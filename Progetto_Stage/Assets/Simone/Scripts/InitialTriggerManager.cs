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
    [SerializeField] List<Material> tavoloMats;
    [SerializeField] List<Material> matSpecials;
    [SerializeField] GameObject tablet;
    [SerializeField] Transform padre;
    public SoundFXRef testSound1;
    //public SoundFXRef cassettoSuono;
    Vector3 earthScale;
    Vector3 moonScale;
    // Start is called before the first frame update
    private void Awake()
    {
        foreach (var renderer in tavoloMats)
        {
                renderer.SetFloat("_Dissolvenza_animazione", 1);
        }
        foreach (var renderer in matSpecials)
        {
            renderer.SetFloat("_Dissolvenza_animazione", -0.2f);
        }
    }
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
        var tabletModel= tablet.transform.GetChild(0).GetChild(0).GetChild(0);
        

        foreach (var renderer in tavoloMats)
        {
            LeanTween.value(1f, -0.2f, 4f).setOnUpdate((float value) =>
            {
                renderer.SetFloat("_Dissolvenza_animazione", value);
            });
        }
        foreach (var renderer in matSpecials)
        {
            LeanTween.value( -0.2f, 1f, 4f).setOnUpdate((float value) =>
            {
                renderer.SetFloat("_Dissolvenza_animazione", value);
            });
        }
        yield return new WaitForSeconds(4.5f);




        tablet.SetActive(true);
        tabletModel.GetChild(2).localScale = Vector3.zero;
        tabletModel.GetChild(0).localScale = new Vector3(0, 0.3762262f, 0.3762262f);
        tabletModel.GetChild(1).LeanMoveLocalZ(0, 1f).setOnComplete(() =>
        {
            tabletModel.GetChild(0).LeanScaleX(0.3762262f, 1).setOnComplete(() =>
            {

                tabletModel.GetChild(2).LeanScale(new Vector3(0.3762262f, 0.3762262f, 0.3762262f), 1).setOnComplete(() =>
                {
                    tablet.LeanMove(new Vector3(0f, 1.02189505f, 1.00300002f), 2f).setEaseInOutQuart();
                    tablet.LeanRotate(new Vector3(46f, 180f, 0), 3f).setEaseOutQuart();
                });
            });
        });
       
       
       
       
    }
}
