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
    //재료 정보 txt를 불러서 일치하는 이름에 Ingredient를 생성하여 Add하기
    // xxx 걍 OrderManager에서 재료정보 한번만 불러오면 될 일이다. 여기서 처리하지 말자xxx
    this.price = price;
  }

  public List<Ingredient> getIngredients(){
    return ingredients;
  }


}
