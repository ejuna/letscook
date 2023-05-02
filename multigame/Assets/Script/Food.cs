using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food
{
  private string foodName;
  private int foodTier;
  private List<Ingredient> ingredients;
  private int price;

  public Food(string name,int tier,List<Ingredient> ing,int price){
    foodName = name;
    foodTier = tier;
    ingredients = ing;
    this.price = price;
  }

  public Food(string name, int tier, string ing, int price)
  {
    foodName = name;
    foodTier = tier;
    ingredients = new List<Ingredient>();
    this.price = price;
  }

  public List<Ingredient> getIngredients(){
    return ingredients;
  }


}
