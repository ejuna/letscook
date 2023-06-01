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
//이건 프리팹에서 불러온 스크립트. 
    recipe = recipe.GetRecipe(this,prefab,id);
//여기선 OrderUI를 설정하고 있고, 나는 OrderUI 스크립트를 바꿔야지,,
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
