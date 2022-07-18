using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject foglioGrande;
    float moltiplicatore_scala;

    public Vector3 destinazioneScelta;

    // Start is called before the first frame update
    void Start()
    {
        destinazioneScelta = new Vector3(103.36f, -24.52f, 1);
        moltiplicatore_scala = foglioGrande.transform.localScale.x / this.gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogWarning("Collido");
        Debug.LogWarning(collision.gameObject.name);
        Instantiate(prefab, collision.GetContact(0).point, Quaternion.identity);
        Debug.LogWarning(collision.GetContact(0).point);
        Vector2 coordinateTocco = new Vector2(collision.GetContact(0).point.x, collision.GetContact(0).point.z);
        Vector2 distanzaToccoCentro = new Vector2(collision.GetContact(0).point.x - transform.position.x, collision.GetContact(0).point.z - transform.position.z);
        Vector3 nuovoPunto = new Vector3(foglioGrande.transform.position.x + distanzaToccoCentro.x * moltiplicatore_scala, foglioGrande.transform.position.y, foglioGrande.transform.position.z + distanzaToccoCentro.y * moltiplicatore_scala);
        //Instantiate(prefab, nuovoPunto, Quaternion.identity);
        destinazioneScelta = nuovoPunto;
    }
}
