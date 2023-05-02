using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public enum Type { StapleFood, Meat, Vegetable };
    public string ingredientName;
    public int price;
    public Type ingredientType;
}