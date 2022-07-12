using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tavolo_script : MonoBehaviour
{
   
    [SerializeField] GameObject console;
    
    [SerializeField] List<Material> tavoloMats;
    [SerializeField] List<Material> matSpecials;
    
   
    
    [SerializeField] StartSequenceManager startSequence;
    
    
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CompariTavolo()
    {
        console.GetComponent<Animator>().SetTrigger("Comparsa_tavolo");
        StartCoroutine(EnableTabletAndTable());
        EnableTabletAndTable();
      
    }
    IEnumerator EnableTabletAndTable()
    {
        yield return new WaitForSeconds(4.1f);
       


        foreach (var renderer in tavoloMats)
        {
            LeanTween.value(1f, -0.2f, 4f).setOnUpdate((float value) =>
            {
                renderer.SetFloat("_Dissolvenza_animazione", value);
            });
        }
        foreach (var renderer in matSpecials)
        {
            LeanTween.value(-0.2f, 1f, 4f).setOnUpdate((float value) =>
            {
                renderer.SetFloat("_Dissolvenza_animazione", value);
            });
        }
        yield return new WaitForSeconds(4.5f);




        //tablet.SetActive(true);
        //tabletModel.GetChild(2).localScale = Vector3.zero;
        //tabletModel.GetChild(0).localScale = new Vector3(0, 0.3762262f, 0.3762262f);
        //tabletModel.GetChild(1).LeanMoveLocalZ(0, 1.2f).setEaseOutQuart().setOnComplete(() =>
        //{
        //    tabletModel.GetChild(0).LeanScaleX(0.3762262f, 1).setEaseOutQuint().setOnComplete(() =>
        //    {

        //        tabletModel.GetChild(2).LeanScale(new Vector3(0.3762262f, 0.3762262f, 0.3762262f), 0.8f).setOnComplete(() =>
        //        {
        //            tablet.LeanMove(new Vector3(0f, 1.206f, 0.75f), 2f).setEaseInOutQuart();
        //            tablet.LeanRotate(new Vector3(58f, 180f, 0), 3f).setEaseOutQuart();

        //            startSequence.GeneraBottone(tabletModel.GetChild(3).GetChild(0).gameObject);
        //            startSequence.GeneraBottone(tabletModel.GetChild(4).GetChild(0).gameObject);
        //        });
        //    });
        //});




    }
}
