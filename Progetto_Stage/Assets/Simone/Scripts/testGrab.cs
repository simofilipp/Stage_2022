using Oculus.Interaction;
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
    [SerializeField] GameObject _testObj;

    private IInteractableView InteractableView;

    protected bool _started = false;


    protected virtual void Awake()
    {
        InteractableView = _interactableView as IInteractableView;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.BeginStart(ref _started);
        Assert.IsNotNull(InteractableView);

        UpdateVisual();
        this.EndStart(ref _started);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void OnEnable()
    {
        if (_started)
        {
            InteractableView.WhenStateChanged += UpdateVisualState;
            UpdateVisual();
        }
    }

    protected virtual void OnDisable()
    {
        if (_started)
        {
            InteractableView.WhenStateChanged -= UpdateVisualState;
        }
    }

    private void UpdateVisual()
    {
        switch (InteractableView.State)
        {
            case InteractableState.Normal:
                break;
            case InteractableState.Hover:
                break;
            case InteractableState.Select:
                //WorldMapManager.instance.CurrentState = stato;
                _testObj.GetComponent<MeshRenderer>().material = materialStato;
                break;
            case InteractableState.Disabled:
                break;
        }
    }

    private void UpdateVisualState(InteractableStateChangeArgs args) => UpdateVisual();

}
