using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
  private Food food;
  private int timeLimit;


  public Order(Food food,int timeLimit){
    this.food = food;
    this.timeLimit = timeLimit;
  }
  
  public Food getFood(){
    return food;
  }

  public int getTimeLimit(){
    return timeLimit;
  }

  public void Clear(){

  }
}
