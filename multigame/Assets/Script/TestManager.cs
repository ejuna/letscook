using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    private OrderManager orderManager;
    private List<FoodData> list;
  float timer = 0f;

  void Start()
    {

    }

    void Update()
    {
    timer += Time.deltaTime;
    if (timer >= 1f){
      orderManager.createOrder(50);
      timer = 0f;
    }
   
    }

    private void printLogFoodList(List<FoodData> list){
    Debug.Log(list.Count);
      foreach(FoodData food in list){
      Debug.Log(food.FoodName);
      Debug.Log(food.FoodTier);
    }
    }

    private void printLogOrder(Order order){
    Debug.Log(order.Food.FoodName);
    Debug.Log(order.Food.FoodTier);
  }
}
