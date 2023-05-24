using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FameManager
{
  public int fame;

  public void moneyIncrease(int amount)
  {
    fame += amount;
  }
  public void moneyDecrease(int amount)
  {
    fame -= amount;
  }
  public void Clear() {
        fame = 0;
    }
}
