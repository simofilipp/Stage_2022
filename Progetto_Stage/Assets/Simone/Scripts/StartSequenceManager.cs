using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequenceManager : MonoBehaviour
{
    [SerializeField] GameObject cassetto;
    [SerializeField] GameObject pannelli;
    [SerializeField] float dissolveTimeGabbia;
    [SerializeField] float dissolveTimeSfere;
    [SerializeField] GameObject ponte;
    [SerializeField] GameObject tastiera;
    [SerializeField] GameObject canvasFinale;
    [SerializeField] GameObject terra;
    [SerializeField] GameObject luna;
    [SerializeField] List<GameObject> sferette;





    public void StartFreeMode()
    {
        ponte.SetActive(false);
        var solve = pannelli.GetComponent<MeshRenderer>().material;
        LeanTween.value(-0.2f, 1f, dissolveTimeGabbia).setOnUpdate((float value) =>
        {
            solve.SetFloat("_Dissolvenza_animazione", value);
            
        }).setOnComplete(() =>
        {
           
            //attivare tastiera e canvas
            tastiera.SetActive(true);
                canvasFinale.SetActive(true);
            //animazione terra che si ingrandisce, luna che scompare
            terra.transform.LeanScale(new Vector3(10, 10, 10), 5f).setEaseInOutQuart().setOnComplete(() => { luna.SetActive(false); }).delay = 0.5f;


            //Animazione entrata palline con dissolve
            foreach (var obj in sferette)
                {
                    obj.SetActive(true);

                    var solveSfera = obj.GetComponentsInChildren<MeshRenderer>()[0].material;
                    LeanTween.value(1f, -0.2f, dissolveTimeSfere).setOnUpdate((float value) =>
                        {
                            solveSfera.SetFloat("_Dissolvenza_animazione", value);
                        });
                }

               
      
        }
            );

    }
}
