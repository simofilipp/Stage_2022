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
    [SerializeField] GameObject freccia;
    [SerializeField] GameObject freccia1;
    [SerializeField] GameObject freccia2;




    public void StartFreeMode()
    {
        ponte.SetActive(false);
        var solve = pannelli.GetComponent<MeshRenderer>().materials;
        LeanTween.value(-0.2f, 1f, dissolveTimeGabbia).setOnUpdate((float value) =>
        {
            solve[1].SetFloat("_Dissolvenza_animazione", value);
            solve[2].SetFloat("_Dissolvenza_animazione", value);
        }).setOnComplete(() =>
        {
            LeanTween.value(-0.2f, 1f, dissolveTimeGabbia).setOnUpdate((float value) =>
            {
                solve[0].SetFloat("_Dissolvenza_animazione", value);
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

                freccia.SetActive(true);
                freccia1.SetActive(true);
                freccia2.SetActive(true);
            });
        }
            );

    }
}
