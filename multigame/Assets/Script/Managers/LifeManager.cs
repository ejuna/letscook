using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LifeManager
{
    
    public int life { get; set; }

    
    public void Init()
    {
        life = Constants.MAX_LIFE;
        
    }

  public bool lifeIncrease(){
    if(life == Constants.MAX_LIFE)
    { //라이프가 꽉 차있으면
      return false;
    }
    life++;
    return true;
  }

  public void lifeDecrease(){
        if (PhotonNetwork.IsMasterClient)
        {
            if (life != 0)
            {
                life--;
                Debug.Log("라이프1 감소");
                Debug.Log("남은 라이프: " + life);
               
            }
            ;
        }
        
    }
}
