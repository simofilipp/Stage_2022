using Oculus.Interaction;
using Oculus.Interaction.HandPosing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_manager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> altreSfere;
    bool selezionato;
    
    [SerializeField]
    GameObject terra;
    [SerializeField] GameObject freccia;
    [SerializeField] GameObject ghostSfera;
    [SerializeField] GameObject keyboard;


    float rotazioneX;
    float rotazioneY;
    float rotazioneZ;

    int counterSelezionato=0;
    int id=100;
    // Start is called before the first frame update
    void Start()
    {
        rotazioneX = 1f;
        rotazioneY = 1f;
        rotazioneZ = 1f;
    }


    public void AvviaCoroutine()
    {
        counterSelezionato += 1;
        if (counterSelezionato == 1)
        {
            ghostSfera.SetActive(false);
            for (int i = 0; i < altreSfere.Count; i++)
            {
                //faccio in modo che le altre sferette non siano interagibili
                altreSfere[i].GetComponentInChildren<GrabInteractable>().enabled = false;
                altreSfere[i].GetComponentInChildren<HandGrabInteractable>().enabled=false;
            }
            selezionato = true;
            //resetto rotazione sferetta per evitare dei movimenti bruschi della terra se non è bloccata
            if (!EarthRotationManager.instance.bloccata)
            {
                this.transform.rotation= new Quaternion(0,0,0,0);
            }

            freccia.SetActive(true);
            EarthRotationManager.instance.bloccata = false;

            StartCoroutine(Rotazione());
        }
    }

    IEnumerator Rotazione()
    {
        //terra.GetComponent<Animator>().SetTrigger("rotazione");
        while (selezionato ==true)
        {
            float rot_inizialeX = 0;
            float rot_inizialeY = 0;
            float rot_inizialeZ = 0;
            float rot_sucessivaX;
            float rot_sucessivaY;
            float rot_sucessivaZ;

            id = terra.LeanRotateAroundLocal(terra.transform.up, -1, 0.01f).id;

            //la terra segue tramite un tween le sferette, aggiunto un threshold per limitare la vibrazione sugli assi
            while (selezionato == true)
            {
                rot_sucessivaX = transform.rotation.eulerAngles.x;
                rot_sucessivaY = transform.rotation.eulerAngles.y;
                rot_sucessivaZ = transform.rotation.eulerAngles.z;
                if (Mathf.Abs(rot_sucessivaX - rot_inizialeX) > rotazioneX || Mathf.Abs(rot_sucessivaY - rot_inizialeY) > rotazioneY || Mathf.Abs(rot_sucessivaZ - rot_inizialeZ) > rotazioneZ)
                {
                    //terra.transform.rotation = this.transform.rotation;
                    LeanTween.cancel(id);
                    id=terra.transform.LeanRotate(transform.rotation.eulerAngles, 0.4f).id;
                    
                }
                rot_inizialeX = rot_sucessivaX;
                rot_inizialeY = rot_sucessivaY;
                rot_inizialeZ = rot_sucessivaY;
                yield return null;
            }
            LeanTween.cancel(id);
        }
    }

    public void StoppaCoroutine()
    {
            counterSelezionato -= 1;
            if (counterSelezionato == 0)
            {
                var my_rigidbody = GetComponent<Rigidbody>();
                selezionato = false;
                if (!EarthRotationManager.instance.bloccata)
                {
                    for (int i = 0; i < altreSfere.Count; i++)
                    {
                        //rendo nuovamente interagibili le sferette
                        altreSfere[i].GetComponentInChildren<GrabInteractable>().enabled = true;
                        altreSfere[i].GetComponentInChildren<HandGrabInteractable>().enabled = true;
                    }
                    freccia.SetActive(false);
                    my_rigidbody.constraints = RigidbodyConstraints.None;
                    my_rigidbody.useGravity = true;
                }
                else
                {
                    my_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                    my_rigidbody.useGravity = false;
                }
            }
    }

    public void BloccaSfera()
    {
        EarthRotationManager.instance.bloccata = true;
        ghostSfera.SetActive(true);
    }

    public void SpegniTastiera()
    {
        if(counterSelezionato==1)
            keyboard.SetActive(false);
    }

    public void AccendiTastiera()
    {
        if (!EarthRotationManager.instance.bloccata && counterSelezionato==0)
        {
            keyboard.SetActive(true);
        }
    }
}

