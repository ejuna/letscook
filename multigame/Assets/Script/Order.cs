using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
  private FoodData food;
  public FoodData Food{ get { return food; } }

  private float timeLimit;
  public float TimeLimit { get{ return timeLimit; } }


  public Order(FoodData food,float timeLimit){
    this.food = food;
    this.timeLimit = timeLimit;
  }
  
  public void Clear(){

  }
}
