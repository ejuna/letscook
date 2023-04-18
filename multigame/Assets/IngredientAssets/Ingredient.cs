using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string name;
    public int price;
    public Type ingredientType;
    public Ingredient(string name, int price, Type ingredientType)
    {
        this.name = name;
        this.price = price;
        this.ingredientType = ingredientType;
    }
}