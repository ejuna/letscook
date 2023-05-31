using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager
{
    
    public int life { get; set; }

    public Action minusLifeAction = null;
    public Action resetLifeAction = null;
    public void Init()
    {
        life = Constants.MAX_LIFE;
        resetLifeAction();
    }

  public bool lifeIncrease(){
    if(life == Constants.MAX_LIFE)
    { //라이프가 꽉 차있으면
      return false;
    }
    life++;
    return true;
  }

  public bool lifeDecrease(){
    if(life == 0){
      return false;
    }
    life--;
    minusLifeAction();
    return true;
  }
}
