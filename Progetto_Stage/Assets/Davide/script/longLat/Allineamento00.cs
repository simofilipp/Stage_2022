using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allineamento00 : MonoBehaviour
{

    [SerializeField] Transform zeroCoordinates;
    [SerializeField] Transform terra;
    [SerializeField] Transform camera;
    [SerializeField] GameObject Terraobj;
    public float lat;
    public float longi;


    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(RuotaSuZero());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator RuotaSuZero()
    {
        
        yield return new WaitForSeconds(4f);
       
        Vector3 centroterra_a_zero = Vector3.Cross(zeroCoordinates.position - terra.transform.position, Vector3.up);
        Vector3 zero_a_camera = Vector3.Cross(this.transform.position - zeroCoordinates.transform.position, Vector3.up);
        Vector3 centroterra_a_camera = Vector3.Cross(this.transform.position - terra.transform.position, Vector3.up);
        float angolo = Mathf.Acos(((zero_a_camera.magnitude * zero_a_camera.magnitude) - (centroterra_a_zero.magnitude * centroterra_a_zero.magnitude) - (centroterra_a_camera.magnitude * centroterra_a_camera.magnitude)) / (-2 * centroterra_a_zero.magnitude * centroterra_a_camera.magnitude));
        
        float angoloinDegreee = angolo * Mathf.Rad2Deg;
       
       
        Terraobj.transform.LeanRotateAroundLocal(Vector3.forward, angoloinDegreee, 5f).setOnComplete(()=> 
        { 
            Terraobj.transform.LeanRotateAroundLocal(Vector3.forward, longi, 5f).setOnComplete(() =>
            {
                Terraobj.transform.LeanRotateAround(Vector3.right, lat*-1f, 5f);
            }); ; 


        });
        
        

        //var toCamera = Quaternion.LookRotation(camera.position - terra.position);
        //var toSite = Quaternion.LookRotation(zeroCoordinates.localPosition);
        //var fromSite = Quaternion.Inverse(toSite);
        //var rotazioneFinale = toCamera * fromSite;
        //terra.LeanRotate(rotazioneFinale.eulerAngles, 1f);

    }
}
