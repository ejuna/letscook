using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SubmitController : MonoBehaviour
{

  bool isPlayerEnter;
  // Start is called before the first frame update
  void Start()
    {
    isPlayerEnter = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void submit(GameObject submitObject){
        Food food = submitObject.GetComponent<Food>();
        if(food == null || !Managers.Orders.checkOrder(food))
        {
          //동기화필요
          Managers.Life.lifeDecrease();
          return;
        }
        else if(Managers.Orders.checkOrder(food)){
          //동기화필요
            Managers.Money.moneyIncrease(food.Price);
            Managers.Fame.fameIncrease(1);
        }
        //맨 앞 주문서 삭제(요리제출로 간주하기때문에)
         Managers.Orders.deleteOrder();
    }


    void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.tag == "Player")
      {
        isPlayerEnter = true;
      }
      if (other.gameObject.tag == "Pickup" && (other.gameObject.transform.parent == null || other.gameObject.transform.parent.name != "GameObject"))
      {
            submit(other.gameObject);
            PhotonNetwork.Destroy(other.gameObject);
      
      }
    }

    void OnTriggerExit(Collider other)
    {
      if (other.gameObject.tag == "Player")
      {
        isPlayerEnter = false;
      }
    }
}
