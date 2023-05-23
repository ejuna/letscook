using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager
{
    
    private int life;

    public void Init()
    {
        life = Constants.MAX_LIFE;
    }
  public int getLife(){
    return life;
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
    return true;
  }
}
