using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
  private FoodData food;
  public FoodData Food{ get { return food; } }

  private float timeLimit;
  public float TimeLimit { get{ return timeLimit; } }

  private GameObject prefab;
  private OrderRecipe recipe;
  public OrderRecipe Recipe{ get { return recipe; } }
  


  public Order(FoodData food,float timeLimit){
    this.food = food;
    this.timeLimit = timeLimit;

    prefab = setPrefab();
    recipe = prefab.AddComponent<OrderRecipe>();
    recipe.setRecipe(this,prefab);
  }


  private GameObject setPrefab(){
    if(prefab == null){
      int recipeSize = Food.Ingredients.Count;

      //프리팹 선택
      string prefabLocation = "";
      switch (recipeSize)
      {
        case 2:
          prefabLocation = "OrderTwo";
          break;
        case 3:
          prefabLocation = "OrderThree";
          break;
        case 4:
          prefabLocation = "OrderFour";
          break;
        case 5:
          prefabLocation = "OrderFive";
          break;
        default: break;
      }
      prefab = Resources.Load<GameObject>("Prefabs/OrderRecipe/" + prefabLocation);
    }

    return prefab;
  }


  public void deleteRecipe(){
    recipe.deleteRecipe();
  }
    

  public void Clear(){

  }
}
