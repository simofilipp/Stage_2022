using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAt : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject camera;

    [SerializeField]
    GameObject ref_line1;

    [SerializeField]
    GameObject ref_line2;

    [SerializeField]
    GameObject stato;

    LineRenderer _lineRenderer;
   


    private void Awake()
    {
        
        _lineRenderer = GetComponent<LineRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(camera.transform.position);
        _lineRenderer.SetPosition(0, ref_line1.transform.position);
        _lineRenderer.SetPosition(1, ref_line2.transform.position);
        _lineRenderer.SetPosition(2, stato.transform.position);


    }
}
