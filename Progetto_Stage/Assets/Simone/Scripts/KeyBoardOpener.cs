using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardOpener : MonoBehaviour
{
    [SerializeField]
    GameObject keyboard;

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
            keyboard.transform.LeanMoveLocal(initialPositionKB, 0.5f).setEaseOutQuart();
            keyboard.transform.LeanScaleY(initialScaleKB.y,0.5f);
            keyboard.transform.LeanScaleX(initialScaleKB.x,0.25f);
        }
        else
        {
            open = false;
            keyboard.transform.LeanMoveLocal(transform.localPosition, 0.7f).setEaseInQuad();
            keyboard.transform.LeanScaleY(0, 0.5f);
            keyboard.transform.LeanScaleX(0, 0.3f);
        }
    }
}
