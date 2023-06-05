using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
  private static int count = 0;
  private int id;
  public int Id{ get { return id; } }

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
    this.id = count;
    count++;

    prefab = setPrefab();
    recipe = prefab.GetComponent<OrderRecipe>();
    recipe = recipe.GetRecipe(this,prefab,id);
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
