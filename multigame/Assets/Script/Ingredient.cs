using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public enum Type { StapleFood, Meat, Vegetable };
    public new string name;
    public int price;
    public Type ingredientType;
}