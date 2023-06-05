using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FameManager
{
    public int fame = 0;

  public void fameIncrease(int amount)
  {
    fame += amount;
  }
  public void fameDecrease(int amount)
  {
    fame -= amount;
  }
  public void Clear() {
        fame = 0;
    }
}
