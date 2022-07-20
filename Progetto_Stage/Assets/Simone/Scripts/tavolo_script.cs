using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tavolo_script : MonoBehaviour
{
   
    [SerializeField] GameObject console;
    [SerializeField] GameObject table;

    [SerializeField] List<Material> tavoloMats;
    [SerializeField] List<Material> matSpecials;
    [SerializeField] GameObject planetario2D;
    [SerializeField] StartSequenceManager startSequenceManager;




    [SerializeField] StartSequenceManager startSequence;

    Vector3 tavoloInitialScale;
    // Start is called before the first frame update
    private void Awake()
    {
        //foreach (var renderer in tavoloMats)
        //{
        //    renderer.SetFloat("_Dissolvenza_animazione", 1);
        //}
        //foreach (var renderer in matSpecials)
        //{
        //    renderer.SetFloat("_Dissolvenza_animazione", -0.2f);
        //}

        tavoloInitialScale = table.transform.localScale;
        table.transform.localScale = Vector3.zero;
        table.SetActive(true);
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


        table.transform.LeanScale(tavoloInitialScale,1.5f).setEaseOutQuart();

        yield return new WaitForSeconds(4.5f);
        if(startSequenceManager.actualMode == Mode.SolarSystemMode)
        {
            planetario2D.transform.parent.gameObject.SetActive(true);

        }




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
