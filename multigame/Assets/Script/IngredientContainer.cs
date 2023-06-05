using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using Photon.Pun;

public class IngredientContainer : MonoBehaviour
{
    public IngredientType ingredientType;
    public Ingredient[] ingredients;
    public int[] counts;

    public GameObject uiContainer;
    public GameObject[] ingredientObject;
    public Transform[] pos;

    private bool isActive;
    MainController mainController;

    void Start()
    {
        isActive = false;
        uiContainer.SetActive(false);
        mainController = FindObjectOfType<MainController>();
    }

    public void enter()
    {
        isActive = !isActive;
        uiContainer.SetActive(isActive);
        if (isActive && mainController != null)
        {
            mainController.freeze();
        }
        else if (!isActive && mainController != null)
        {
            mainController.unfreeze();
        }
    }
    public void exit()
    {
        isActive = false;
        uiContainer.SetActive(false);
        if (mainController != null)
        {
            mainController.unfreeze();
        }
    }
    public void getIngredient(int index) // 재료오브젝트 생성
    {
        Vector3 newPosition = pos[index].position + new Vector3(0f, 2f, 0f);
        //Instantiate(ingredientObject[index], newPosition, pos[index].rotation);
        PhotonNetwork.Instantiate("Prefabs/재료/" + ingredientObject[index].name, newPosition, pos[index].rotation);
        exit();
    }
}