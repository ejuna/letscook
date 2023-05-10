using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
  private Food food;
  public Food Food{ get { return food; } }

  private int timeLimit;
  public int TimeLimit{ get{ return timeLimit; } }


  public Order(Food food,int timeLimit){
    this.food = food;
    this.timeLimit = timeLimit;
  }
  
  public void Clear(){

  }
}
