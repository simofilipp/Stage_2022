using Oculus.Interaction;
using Oculus.Interaction.HandPosing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardOpener : MonoBehaviour
{
    [SerializeField]
    GameObject keyboard;

    [SerializeField]
    List<GameObject> sferette;


    Vector3 initialPositionKB;
    Vector3 initialScaleKB;

    bool open= false;
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        initialPositionKB=keyboard.transform.localPosition;
        initialScaleKB=keyboard.transform.localScale;
        keyboard.transform.localPosition=this.transform.localPosition;
        keyboard.transform.LeanScaleY(0, 0.01f);
        keyboard.transform.LeanScaleX(0, 0.01f);
        //OpenCloseKeyboard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCloseKeyboard()
    {
        if (!open)
        {
            open=true;
            EarthRotationManager.instance.CancellaTweenTerra();
            APIManager.instance.turn = true;
            keyboard.transform.LeanMoveLocal(initialPositionKB, 0.5f).setEaseOutQuart();
            keyboard.transform.LeanScaleY(initialScaleKB.y,0.5f);
            keyboard.transform.LeanScaleX(initialScaleKB.x,0.25f);

            foreach(var sferetta in sferette)
            {
                sferetta.GetComponentInChildren<GrabInteractable>().enabled = false;
                sferetta.GetComponentInChildren<HandGrabInteractable>().enabled = false;
            }
        }
        else
        {
            open = false;
            EarthRotationManager.instance.RiprendiTweenTerra();
            APIManager.instance.turn = false;
            keyboard.transform.LeanMoveLocal(transform.localPosition, 0.7f).setEaseInQuad();
            keyboard.transform.LeanScaleY(0, 0.5f);
            keyboard.transform.LeanScaleX(0, 0.3f);

            foreach (var sferetta in sferette)
            {
                sferetta.GetComponentInChildren<GrabInteractable>().enabled = true;
                sferetta.GetComponentInChildren<HandGrabInteractable>().enabled = true;
            }
        }
    }
}
