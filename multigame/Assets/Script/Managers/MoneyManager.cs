using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager
{
    public int money;

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