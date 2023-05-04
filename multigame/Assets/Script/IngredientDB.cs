using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDB : MonoBehaviour
{
    public static IngredientDB Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<Ingredient> IngredDB = new List<Ingredient>();
}