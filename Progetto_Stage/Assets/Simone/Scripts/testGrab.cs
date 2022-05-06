using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class testGrab : MonoBehaviour
{
    OVRGrabbable _ovrg;

    [SerializeField, Interface(typeof(IInteractableView))]
    private MonoBehaviour _interactableView;

    
    [SerializeField] Material materialStato;
    [SerializeField] Shader layerShader;
    [SerializeField] GameObject _testObj;

    private IInteractableView InteractableView;

    protected bool _started = false;


    protected virtual void Awake()
    {
        InteractableView = _interactableView as IInteractableView;
        //WorldMapManager.EventChangeState += OnChangeState;
    }

    private void OnChangeState()
    {
        throw new NotImplementedException();
    }

    // Start is called before the first frame update
    //protected virtual void Start()
    //{
    //    this.BeginStart(ref _started);
    //    Assert.IsNotNull(InteractableView);

    //    UpdateVisual();
    //    this.EndStart(ref _started);
    //}

    // Update is called once per frame
    void Update()
    {

    }

    //protected virtual void OnEnable()
    //{
    //    if (_started)
    //    {
    //        InteractableView.WhenStateChanged += UpdateVisualState;
    //        UpdateVisual();
    //    }
    //}

    //protected virtual void OnDisable()
    //{
    //    if (_started)
    //    {
    //        InteractableView.WhenStateChanged -= UpdateVisualState;
    //    }
    //}

    //private void UpdateVisual()
    //{
    //    switch (InteractableView.State)
    //    {
    //        case InteractableState.Normal:
    //            break;
    //        case InteractableState.Hover:
    //            break;
    //        case InteractableState.Select:
    //            //WorldMapManager.instance.CurrentState = stato;
    //            _testObj.GetComponent<MeshRenderer>().material.shader = layerShader;
    //            break;
    //        case InteractableState.Disabled:
    //            break;
    //    }
    //}

    //private void UpdateVisualState(InteractableStateChangeArgs args) => UpdateVisual();

    public void OnChange(int id)
    {

        if (id == 0) WorldMapManager.instance.CurrentState = WorldMapManager.instance.CurrentState = WorldMapManager.State.Earth;
        if (id == 1) WorldMapManager.instance.CurrentState = WorldMapManager.instance.CurrentState = WorldMapManager.State.Politic;
        if (id == 2) WorldMapManager.instance.CurrentState = WorldMapManager.instance.CurrentState = WorldMapManager.State.Population;
        if (id == 3) WorldMapManager.instance.CurrentState = WorldMapManager.instance.CurrentState = WorldMapManager.State.Science;
        if (id == 4) WorldMapManager.instance.CurrentState = WorldMapManager.instance.CurrentState = WorldMapManager.State.Transport;
        if (id == 5) WorldMapManager.instance.CurrentState = WorldMapManager.instance.CurrentState = WorldMapManager.State.Disaster;
        if (id == 6) WorldMapManager.instance.CurrentState = WorldMapManager.instance.CurrentState = WorldMapManager.State.Climat;
    }

}
