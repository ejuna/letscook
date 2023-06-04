using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using Photon.Pun;


public class Ingredient : MonoBehaviourPunCallbacks
{
  public IngredientData ingredientData;

    [SerializeField]
    public string ingredientName { get; set; }
    [SerializeField]
    public int Price { get; set; }
    [SerializeField]
    public IngredientType Type { get; set; }
    [SerializeField]
    public GameObject PrepIngredient { get; set; }

    public Vector3 remotePos;
    

    public void Start()
  {
    ingredientName = ingredientData.IngredientName;
    Price = ingredientData.Price;
    Type = ingredientData.IngredientType;
    PrepIngredient = ingredientData.PrepIngredient;
  }

    void Update()
    {
        if (false == photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, remotePos, 10 * Time.deltaTime);
            return;
        }
    }


    public void ingredientInfo()
  {
    Debug.Log("재료 이름 :: " + ingredientData.name);
    Debug.Log("재료 가격 :: " + ingredientData.Price);
    Debug.Log("재료 종류 :: " + ingredientData.IngredientType);
  }


    /*[PunRPC]
    void UpdateInteractingObjectPosition(Vector3 position, int id)
    {
        Debug.Log("찾아보자");
        PhotonView targetView = PhotonView.Find(id);
        Debug.Log("찾는거실행했음");
        if (targetView != null)
        {
            Debug.Log("찾았음");
            GameObject targetObject = targetView.gameObject;
            targetView.RequestOwnership();
            targetObject.transform.position = position;
            Debug.Log(targetObject.name);
        }

        Debug.Log("끝");
    }*/

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 내가 데이터를 보내는 중이라면
        if (stream.IsWriting) // 내꺼 보내는 거
        {
            // 이 방안에 있는 모든 사용자에게 브로드캐스트 
            // - 내 포지션 값을 보내보자
            stream.SendNext(transform.position);
          
        }
        // 내가 데이터를 받는 중이라면 
        else // 원격에 있는 나 
        {
            // 순서대로 보내면 순서대로 들어옴. 근데 타입캐스팅 해주어야 함
            remotePos = (Vector3)stream.ReceiveNext();
            
        }
    }

}
