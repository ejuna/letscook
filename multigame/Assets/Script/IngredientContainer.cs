using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class IngredientContainer : MonoBehaviour
{
    public IngredientType ingredientType;
    public Ingredient[] ingredients;
    public int[] counts;

    public GameObject uiContainer;
    public GameObject[] ingredientObject;
    public Transform[] pos;
    void Start()
    {
        uiContainer.SetActive(false);
    }

    public void enter()
    {
        uiContainer.SetActive(true);
    }
    public void exit()
    {
        uiContainer.SetActive(false);
    }
    public void getIngredient(int index) // 재료오브젝트 생성
    {
        Vector3 newPosition = pos[index].position + new Vector3(0f, 1f, 0f);
        Instantiate(ingredientObject[index], newPosition, pos[index].rotation);
        exit();
    }
}