using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ProcessController1 : MonoBehaviour
{
    public Transform pos;
    MainController mainController;
    GameObject cooker;
    GameObject player;
    bool isPlayerEnter;

    void Start()
    {
        isPlayerEnter = false;
        player = GameObject.FindGameObjectWithTag("Player");
        mainController = FindObjectOfType<MainController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            cook();
        }
    }

    IEnumerator CookAndThrowObject(GameObject interactingObject)
    {
        // 재료를 요리하는 동작 수행
        Rigidbody interactingRigidbody = interactingObject.GetComponent<Rigidbody>();
        interactingRigidbody.isKinematic = true;
        interactingObject.transform.SetParent(pos);
        interactingObject.transform.localPosition = new Vector3(0, 1f, 0);
        interactingObject.GetComponent<Collider>().isTrigger = true;
        mainController.drop();

        yield return new WaitForSeconds(2f); // 2초 대기

        // 재료를 던지는 동작 수행
        throwObject(interactingObject);
        mainController.unfreeze();
    }

    void cook()
    {
        if (isPlayerEnter)
        {
            GameObject interactingObject = mainController.getInteractingObject();
            bool isPicking = mainController.getIsPicking();
            if (isPicking)
            {
                mainController.freeze();
                StartCoroutine(CookAndThrowObject(interactingObject));
            }
        }
    }

    public void throwObject(GameObject go)
    {
        //go의 Rigidbody를 가져온다.
        Rigidbody rigidbody = go.GetComponent<Rigidbody>();

        //던질 각도,및 힘 선언 및 조절
        Vector3 thorwAngle = new Vector3(3f, 2f, 0f);
        float thorwForce = 2f;
        //
        rigidbody.AddForce(thorwAngle * thorwForce, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (player == other.gameObject)
        {
            isPlayerEnter = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (player == other.gameObject)
        {
            isPlayerEnter = false;
        }
    }
}