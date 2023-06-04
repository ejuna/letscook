using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class WasteCanController : MonoBehaviour
{

    public bool isPlayerEnter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerEnter = true;
        }
        if (other.gameObject.tag == "Pickup" && (other.gameObject.transform.parent == null || other.gameObject.transform.parent.name != "GameObject"))
        {
            Destroy(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerEnter = false;
        }
    }
}
