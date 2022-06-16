using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbGunManager : MonoBehaviour
{
    [SerializeField] GameObject orbPrefab;
    [SerializeField] Transform handPositionDX;
    [SerializeField] Transform handPositionSX;
    [SerializeField] float explosionForce;

    public GameObject orbDX;
    public GameObject orbSX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateOrbDX()
    {
        if(orbDX == null)
        {
            orbDX = Instantiate(orbPrefab, handPositionDX);
            orbDX.transform.Rotate(new Vector3(0, 180, 0));
        }

    }
    public void GenerateOrbSX()
    {
        if (orbSX == null)
            orbSX = Instantiate(orbPrefab, handPositionSX);
    }

    public void FireOrbDX()
    {
        if( orbDX != null)
        {
            orbDX.GetComponent<Rigidbody>().AddForce(orbDX.transform.up* -explosionForce, ForceMode.Impulse);
            orbDX.transform.parent = null;
            Destroy(orbDX,1.5f);
        }
    }
    public void FireOrbSX()
    {
        if (orbSX != null)
        {
            orbSX.GetComponent<Rigidbody>().AddForce(orbSX.transform.up * explosionForce, ForceMode.Impulse);
            orbSX.transform.parent = null;
            Destroy (orbSX,1.5f);
        }
    }
}
