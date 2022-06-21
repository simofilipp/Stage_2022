using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> planetInteractables;
    [SerializeField] GameObject table;
    bool orbiteAttive = true;

    [SerializeField]
    Istanzia_istogrammi istanziaEu;

   
    // Start is called before the first frame update
    void Start()
    {
        istanziaEu.IstanziaIstoEuropa();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CenterPose()
    {
        OVRManager.display.RecenterPose();
    }

    public void DisableTable()
    {
        table.SetActive(!table.activeSelf);
    }

    //public void SwitchOrbits()
    //{
    //    foreach (var planet in planetInteractables)
    //    {
    //        if(planet.GetComponentInChildren<TrailRenderer>() != null)
    //        {
    //            planet.GetComponentInChildren<TrailRenderer>().enabled = orbiteAttive;
    //            orbiteAttive = !orbiteAttive;
    //        }
    //    }
    //}
}
