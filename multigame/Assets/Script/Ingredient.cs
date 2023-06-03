using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Ingredient : MonoBehaviour
{
  public IngredientData ingredientData;

    [SerializeField]
    public string ingredientName { get; set; }
    [SerializeField]
    public int Price { get; set; }
    [SerializeField]
    public IngredientType Type { get; set; }
    [SerializeField]
    public GameObject PrepIngredient { get; set; }

    public void Start()
  {
    ingredientName = ingredientData.IngredientName;
    Price = ingredientData.Price;
    Type = ingredientData.IngredientType;
    PrepIngredient = ingredientData.PrepIngredient;
  }


  public void ingredientInfo()
  {
    Debug.Log("재료 이름 :: " + ingredientData.name);
    Debug.Log("재료 가격 :: " + ingredientData.Price);
    Debug.Log("재료 종류 :: " + ingredientData.IngredientType);
  }

}
