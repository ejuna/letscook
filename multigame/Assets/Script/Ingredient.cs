using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[System.Serializable]
public class Ingredient
{
    //Type : StapleFood, Meat, Vegetable
    public string ingredientName;
    public int price;
    public Sprite ingredientImage;
    //public Mesh ingredientMesh;
    public IngredientType ingredientType;
}