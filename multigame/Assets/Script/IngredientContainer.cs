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

    public void Enter()
    {
        uiContainer.SetActive(true);
    }
    public void Exit()
    {
        uiContainer.SetActive(false);
    }
    public void getIngredient(int index)
    {
        Vector3 newPosition = pos[index].position + new Vector3(0f, 1f, 0f); // y축 값을 1만큼 올림
        Instantiate(ingredientObject[index], newPosition, pos[index].rotation);
        Exit();
    }
}