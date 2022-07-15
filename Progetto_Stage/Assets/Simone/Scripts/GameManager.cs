using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> bottoniScalaPianeti;
    [SerializeField] List<GameObject> bottoniScalaLuna;
    [SerializeField] GameObject table;
    [SerializeField] Material materialSelected;
    [SerializeField] Material materialNormal;
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

    public void BottoneSelezionato(GameObject bottoneDaCambiare)
    {
        if (!OptionsPlanetarium.moduloAttivo)
        {
            bottoneDaCambiare.GetComponent<MeshRenderer>().material = materialSelected;
        }
    }

    public void SelezionaBottoneScalaPianeti(GameObject bottoneSelezionato)
    {
        foreach(GameObject bottoneDaCambiare in bottoniScalaPianeti)
        {
            bottoneDaCambiare.GetComponent<MeshRenderer>().material = materialNormal;
        }
        bottoneSelezionato.GetComponent<MeshRenderer>().material = materialSelected;
    }
    public void SelezionaBottoneScalaLuna(GameObject bottoneSelezionato)
    {
        foreach (GameObject bottoneDaCambiare in bottoniScalaLuna)
        {
            bottoneDaCambiare.GetComponent<MeshRenderer>().material = materialNormal;
        }
        bottoneSelezionato.GetComponent<MeshRenderer>().material = materialSelected;
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
