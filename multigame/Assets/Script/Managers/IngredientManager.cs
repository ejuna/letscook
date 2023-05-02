using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public List<IngredientContainer> ingredientContainers;

    public void display()
    {

    }

    public void ingredientIncrease(string name, Ingredient.Type ingredientType, IngredientContainer ingredientContainer)
    {
        if (ingredientContainer.ingredientType == ingredientType)
        {
            for (int i = 0; i < ingredientContainer.ingredients.Length; i++)
            {
                if (ingredientContainer.ingredients[i].ingredientName == name)
                {
                    ingredientContainer.counts[i]++;
                    return;
                }
            }
        }
    }
    public void ingredientDecrease(string name, Ingredient.Type ingredientType, IngredientContainer ingredientContainer)
    {
        if (ingredientContainer.ingredientType == ingredientType)
        {
            for (int i = 0; i < ingredientContainer.ingredients.Length; i++)
            {
                if (ingredientContainer.ingredients[i].ingredientName == name)
                {
                    if (ingredientContainer.counts[i] > 0)
                    {
                        ingredientContainer.counts[i]--;
                    }
                    return;
                }
            }
        }
    }
}