using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(fileName = "Ingredient Data", menuName = "Scriptable Object/Ingredient Data", order = int.MaxValue)]
public class IngredientData : ScriptableObject
{
  [SerializeField]
  private string ingredientName;
  public string IngredientName { get { return ingredientName; } }

  [SerializeField]
  private int price;
  public int Price { get { return price; } }

  [SerializeField]
  private IngredientType ingredientType;
  public IngredientType IngredientType { get { return ingredientType; } }
}
