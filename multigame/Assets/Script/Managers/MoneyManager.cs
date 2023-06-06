using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager
{
    public int money = 754620;
    public int tempMoney = 754620;

   

    public void moneyIncrease(int amount)
    {
        money += amount;
    }
    public bool moneyDecrease(int amount)
    {
        if(money < amount){
          return false;
        }
        money -= amount;
        return true;
    }
    public void Clear() { }
}