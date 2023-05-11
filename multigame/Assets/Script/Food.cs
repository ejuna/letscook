using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
  public FoodData foodData;
  private int price { get; set; }


  private void Start()
  {
    int ingredient_size = foodData.Ingredients.Count;
    for(int i=0;i<ingredient_size;i++){
      price += foodData.Ingredients[i].Price;
    }
  }


}
