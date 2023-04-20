using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public List<IngredientContainer> ingredientContainers;

    public void display()
    {

    }

    public void ingredientIncrease(string name, Type IngredeintType, IngredientContainer ingredientContainer)
    {
        if (ingredientContainer.GetType() == IngredeintType)
        {
            
        }
    }
    public void ingredientDecrease(string name, Type IngredeintType, IngredientContainer ingredientContainer)
    {
        if (ingredientContainer.GetType() == IngredeintType)
        {

        }
    }
}