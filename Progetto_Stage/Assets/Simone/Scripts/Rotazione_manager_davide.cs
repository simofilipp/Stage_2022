using Oculus.Interaction;
using Oculus.Interaction.HandPosing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotazione_manager_davide : MonoBehaviour
{
    [SerializeField]
    List<GameObject> altreSfere;
    bool selezionato;
    [SerializeField]
    GameObject terra;
    float rotazioneX;
    float rotazioneY;
    float rotazioneZ;

    int counterSelezionato = 0;
    // Start is called before the first frame update
    void Start()
    {
        rotazioneX = 1f;
        rotazioneY = 1f;
        rotazioneZ = 1f;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void AvviaCoroutine()
    {
        counterSelezionato += 1;
        if (counterSelezionato == 1)
        {
            for (int i = 0; i < altreSfere.Count; i++)
            {
                altreSfere[i].GetComponentInChildren<GrabInteractable>().enabled = false;
                altreSfere[i].GetComponentInChildren<HandGrabInteractable>().enabled = false;
            }
            selezionato = true;
            StartCoroutine(Rotazione());
        }
    }

    IEnumerator Rotazione()
    {
        float rot_inizialeX = 0;   
        float rot_inizialeY = 0;   
        float rot_inizialeZ = 0;
        float rot_sucessivaX;
        float rot_sucessivaY;
        float rot_sucessivaZ;

        while (selezionato == true)
        {
            rot_sucessivaX = transform.rotation.eulerAngles.x;
            rot_sucessivaY = transform.rotation.eulerAngles.y;
            rot_sucessivaZ = transform.rotation.eulerAngles.z;
            Debug.LogWarning(rot_sucessivaX - rot_inizialeX);
            if (Mathf.Abs(rot_sucessivaX - rot_inizialeX) > rotazioneX || Mathf.Abs(rot_sucessivaY - rot_inizialeY) > rotazioneY || Mathf.Abs(rot_sucessivaZ - rot_inizialeZ) > rotazioneZ)
            {
                //terra.transform.rotation = this.transform.rotation;
                LeanTween.cancel(terra);
                terra.transform.LeanRotate(transform.rotation.eulerAngles, 0.5f);
            }
            rot_inizialeX = rot_sucessivaX;
            rot_inizialeY = rot_sucessivaY;
            rot_inizialeZ = rot_sucessivaY;
            yield return null;
        }
    }

    public void StoppaCoroutine()
    {
        counterSelezionato -= 1;
        if (counterSelezionato == 0)
        {
            selezionato = false;
            for (int i = 0; i < altreSfere.Count; i++)
            {
                altreSfere[i].GetComponentInChildren<GrabInteractable>().enabled = true;
                altreSfere[i].GetComponentInChildren<HandGrabInteractable>().enabled = true;
            }
        }
    }

}