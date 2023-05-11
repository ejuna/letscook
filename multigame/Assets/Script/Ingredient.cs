using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Ingredient : MonoBehaviour
{
  public IngredientData ingredientData;
  public string ingredientName { get; set; }
  public int Price { get; set; }
  public IngredientType Type { get; set; }


  public void Start()
  {
    ingredientName = ingredientData.IngredientName;
    Price = ingredientData.Price;
    Type = ingredientData.IngredientType;
  }


  public void ingredientInfo()
  {
    Debug.Log("재료 이름 :: " + ingredientData.name);
    Debug.Log("재료 가격 :: " + ingredientData.Price);
    Debug.Log("재료 종류 :: " + ingredientData.IngredientType);
  }

}
