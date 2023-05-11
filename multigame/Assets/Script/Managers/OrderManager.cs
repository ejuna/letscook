using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager
{
  private Queue<Order> orderList;
  private List<Food> AllFoods= getAllFoodList();//게임 프리팹 넣기?
  private List<Food> todayFoods;
  public int complete { get; set; }
  private const int DEFALT_TIME = 20;


  private static List<Food> getAllFoodList() {
    List<Food> foods = new List<Food>();
    
    return foods;
  }


  public void init(int day,int fame){
    orderList = new Queue<Order>();
    createTodaysOrder(day, fame);
    complete = 0;
  }


///명성과 일수 계산하여 tier 몇까지 받을건지
  public void createTodaysOrder(int day, int fame)
  {
    
  }


  public void createOrder(){
    int rand = Random.Range(0, AllFoods.Count);
    Order order = new Order(todayFoods[rand], DEFALT_TIME);
    orderList.Enqueue(order);
  }

  public void createOrder(int time)
  {
    int rand = Random.Range(0, AllFoods.Count);
    Order order = new Order(todayFoods[rand], time);
    orderList.Enqueue(order);
  }



  public void createGourmandOrder(){
    createOrder((int)(DEFALT_TIME - DEFALT_TIME * 0.3));
  }



  public void createGroupGuestOrder(int group_num)
  {
    for (int i=0;i<= group_num;i++){
      createOrder();
    }
  }

  public bool checkOrder(Food food){
    if (orderList.Count == 0)
    {
      return false;
    }

    Food order_food = orderList.Dequeue().Food;
    List<IngredientData> ingredients = food.foodData.Ingredients;
    List<IngredientData> order_ingredients = order_food.foodData.Ingredients;
    int ingredients_size = ingredients.Count;
    int order_ingredients_size = order_ingredients.Count;


    List<bool> check = new List<bool>();
    for(int i=0;i<order_ingredients.Count;i++){
      check.Add(false);
    }


    for(int i=0;i< ingredients_size; i++){
      string ingredient = ingredients[i].IngredientName;
      for(int j=0;j< order_ingredients_size; j++){
        if(check[j] == false && ingredient.Equals(order_ingredients[j].IngredientName)){
          check[j] = true;
          break;
        }

        //일치하는 재료가 없을때
        if (j == order_ingredients_size - 1) return false;
      }
    }

    for(int i = 0; i < order_ingredients_size; i++){
      if (check[i] == false) return false;
    }

    return true;
  }

  public bool deleteOrder(){
    if(orderList.Count == 0){
      return false;
    }
    orderList.Dequeue();
    return true;
  }

  public bool isOrders()
    {
        if (orderList.Count == 0)
        {
            return false;
        }
        return true; ;
    }
  public void Clear(){

  }

}
