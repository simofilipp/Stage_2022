using Oculus.Interaction;
using Oculus.Interaction.HandPosing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation_manager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> altreSfere;
    bool selezionato;
    [SerializeField]
    GameObject terra;
    [SerializeField] GameObject freccia;
  

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

    // Update is called once per frame
    void Update()
    {
        //controllo che la terra si sia rimessa in asse prima di poterci interagire nuovamente
        if (EarthRotationManager.instance.sferaLasciata)
        {
            GetComponentInChildren<GrabInteractable>().enabled = false;
            GetComponentInChildren<HandGrabInteractable>().enabled = false;
        }
        else
        {
            GetComponentInChildren<GrabInteractable>().enabled = true;
            GetComponentInChildren<HandGrabInteractable>().enabled = true;
        }
    }
    public void AvviaCoroutine()
    {
        counterSelezionato += 1;
        if (counterSelezionato == 1)
        {
            for(int i = 0; i < altreSfere.Count; i++)
            {
                //faccio in modo che le altre sferette non siano interagibili
                altreSfere[i].GetComponentInChildren<GrabInteractable>().enabled = false;
                altreSfere[i].GetComponentInChildren<HandGrabInteractable>().enabled = false;
            }
            selezionato = true;
            //resetto rotazione sferetta per evitare dei movimenti bruschi della terra
            this.transform.rotation= new Quaternion(0,0,0,0);
            freccia.SetActive(true);
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

            id = terra.LeanRotateAroundLocal(terra.transform.up, -1, 0.05f).id;

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
                    id=terra.transform.LeanRotate(transform.rotation.eulerAngles, 0.5f).id;
                    Debug.LogWarning(id);
                }
                rot_inizialeX = rot_sucessivaX;
                rot_inizialeY = rot_sucessivaY;
                rot_inizialeZ = rot_sucessivaY;
                yield return null;
            }
            LeanTween.cancel(id);
        }
        //terra.GetComponent<Animator>().SetTrigger("rotazione");
    }

    public void StoppaCoroutine()
    {
        counterSelezionato -= 1;
        if(counterSelezionato == 0)
        {
            selezionato = false;
            for (int i = 0; i < altreSfere.Count; i++)
            {
                //rendo nuovamente interagibili le sferette
                altreSfere[i].GetComponentInChildren<GrabInteractable>().enabled = true;
                altreSfere[i].GetComponentInChildren<HandGrabInteractable>().enabled = true;
            }
            freccia.SetActive(false);
        }
    }

}
