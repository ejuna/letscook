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

    /*void Update()
    {
        if (false == photonView.IsMine)
        {
            transform.position = remotePos;
            return;
        }
    }*/


    public void ingredientInfo()
  {
    Debug.Log("재료 이름 :: " + ingredientData.name);
    Debug.Log("재료 가격 :: " + ingredientData.Price);
    Debug.Log("재료 종류 :: " + ingredientData.IngredientType);
  }


    [PunRPC]
    void UpdateInteractingObjectPosition(Vector3 position)
    {
        Debug.Log(position);
        transform.position = remotePos;

        Debug.Log("끝");
    }

    

}
