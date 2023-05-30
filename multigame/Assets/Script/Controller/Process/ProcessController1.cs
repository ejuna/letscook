using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessController1 : MonoBehaviour
{
    public Transform pos;
    MainController mainController;
    GameObject cooker;
    public bool isPlayerEnter;
    // Start is called before the first frame update
    void Start()
    {
        mainController = FindObjectOfType<MainController>();
        isPlayerEnter = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject interactingObject = mainController.getInteractingObject();
        if (other.CompareTag("Player"))
        {
            bool isPicking = mainController.getIsPicking();
            isPlayerEnter = true;
            if (isPicking && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("in");
                Rigidbody interactingRigidbody = interactingObject.GetComponent<Rigidbody>();
                interactingRigidbody.isKinematic = true;
                interactingObject.transform.SetParent(pos);
                interactingObject.transform.localPosition = new Vector3(0, 1f, 0);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("out");
            isPlayerEnter = false;
        }
    }
}