using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequenceManager : MonoBehaviour
{
    [SerializeField] GameObject cassetto;
    [SerializeField] GameObject gabbia;
    [SerializeField] float dissolveTimeGabbia;
    [SerializeField] float dissolveTimeSfere;
    [SerializeField] GameObject ponte;
    [SerializeField] GameObject tastiera;
    [SerializeField] GameObject canvasFinale;
    [SerializeField] GameObject terra;
    [SerializeField] List<GameObject> sferette;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.RawButton.A) || OVRInput.GetDown(OVRInput.RawButton.X))
        {
            cassetto.GetComponent<Animator>().SetTrigger("scomparsa");
        }
    }

    public void StartFreeMode()
    {
        ponte.SetActive(false);
        var solve = gabbia.GetComponent<MeshRenderer>().material;
        LeanTween.value(-0.2f, 1f, dissolveTimeGabbia).setOnUpdate((float value) =>
        {
            solve.SetFloat("_Dissolvenza_animazione", value);
        }).setOnComplete(() =>
        {
            //attivare tastiera e canvas
            tastiera.SetActive(true);
            canvasFinale.SetActive(true);
            //animazione terra che sale
            terra.transform.LeanMoveY(0, 5f);
            

            //Animazione entrata palline con dissolve
            foreach (var obj in sferette)
            {
                obj.SetActive(true);
                var solveSfera = obj.GetComponentInChildren<MeshRenderer>().material;
                LeanTween.value(1f, -0.2f, dissolveTimeSfere).setOnUpdate((float value) =>
                {
                    solveSfera.SetFloat("_Dissolvenza_animazione", value);
                });
            }
        });

    }
}
