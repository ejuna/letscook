using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;

public class ProcessController3 : MonoBehaviour
{
    public Transform pos;
    MainController mainController;
    GameObject cooker;
    GameObject player;
    bool isPlayerEnter;
    public GameObject effect;

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
        Ingredient ingredient = interactingObject.GetComponent<Ingredient>();
        IngredientData ingredientData = ingredient.ingredientData;
        GameObject prepIngredient = ingredientData.PrepIngredient;

        Vector3 position = pos.position + new Vector3(0f, 3f, 0f);
        GameObject fxObject = PhotonNetwork.Instantiate("Effect/" + effect.name, position, Quaternion.identity);
        //fxObject.transform.parent = effect.transform;

        mainController.drop();
        PhotonNetwork.Destroy(interactingObject);
        yield return new WaitForSeconds(4f); // 5초 대기

        GameObject prepObject = PhotonNetwork.Instantiate("Prefabs/재료/" + prepIngredient.name, position, Quaternion.identity);

        throwObject(prepObject);
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
        Vector3 thorwAngle = new Vector3(0f, 2f, -1f);
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