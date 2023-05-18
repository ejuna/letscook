using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class PickUp : MonoBehaviour
{
    public Transform character;
    public Transform GameObject;

    private GameObject interactingObject;
    private Rigidbody interactingRigidbody;
    private bool isInteracting;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isInteracting)
            {
                interactingObject = FindInteractableObject();
                if (interactingObject != null)
                {
                    interactingRigidbody = interactingObject.GetComponent<Rigidbody>();
                    interactingRigidbody.isKinematic = true;

                    interactingObject.transform.SetParent(GameObject);
                    interactingObject.transform.localPosition = Vector3.zero;
                    isInteracting = true;
                }
            }
            else
            {
                interactingRigidbody.isKinematic = false;

                interactingObject.transform.SetParent(null);
                interactingObject = null;
                isInteracting = false;
            }
        }
    }

    private GameObject FindInteractableObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(character.position, character.forward, out hit))
        {
            GameObject objectHit = hit.collider.gameObject;
            if (objectHit.CompareTag("Pickup"))
            {
                return objectHit;
            }
        }

        return null;
    }
}