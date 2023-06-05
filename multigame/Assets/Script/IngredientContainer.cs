using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Define;
using Photon.Pun;

public class IngredientContainer : MonoBehaviourPun, IPunObservable
{
    public IngredientType ingredientType;
    public Ingredient[] ingredients;
    public int[] counts;
    public TextMeshProUGUI[] textArea;

    public GameObject uiContainer;
    public GameObject[] ingredientObject;
    public Transform[] pos;

    private bool isActive;
    MainController mainController;

    void Start()
    {
        isActive = false;
        uiContainer.SetActive(false);
        mainController = FindObjectOfType<MainController>();
        for(int i=0;i<counts.Length;i++){
          textArea[i].text = counts[i].ToString();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 데이터를 전송하는 플레이어 (송신)
            for (int i = 0; i < counts.Length; i++)
            {
                stream.SendNext(counts[i]);
            }
        }
        else
        {
            // 데이터를 수신하는 플레이어 (수신)
            for (int i = 0; i < counts.Length; i++)
            {
                counts[i] = (int)stream.ReceiveNext();
                textArea[i].text = counts[i].ToString();
            }
        }
    }

    public void countIncrease(int index, int num)
    {
        counts[index] += num;
        textArea[index].text = counts[index].ToString();
        photonView.RPC("UpdateCount", RpcTarget.OthersBuffered, index, counts[index]);
    }

    [PunRPC]
    private void UpdateCount(int index, int value)
    {
        counts[index] = value;
        textArea[index].text = value.ToString();
    }

    public bool countDecrease(int index,int num){
      if(counts[index] < num){
        Debug.Log("갯수부족");
        return false;
      }
      counts[index] -= num;
      textArea[index].text = counts[index].ToString();
      return true;
    }


    public void enter()
    {
        isActive = !isActive;
        uiContainer.SetActive(isActive);
        if (isActive && mainController != null)
        {
            mainController.freeze();
        }
        else if (!isActive && mainController != null)
        {
            mainController.unfreeze();
        }
    }
    public void exit()
    {
        isActive = false;
        uiContainer.SetActive(false);
        if (mainController != null)
        {
            mainController.unfreeze();
        }
    }
    public void getIngredient(int index) // 재료오브젝트 생성
    {
        if(!countDecrease(index,1)){
          return;
        }
        Vector3 newPosition = pos[index].position + new Vector3(0f, 2f, 0f);
        //Instantiate(ingredientObject[index], newPosition, pos[index].rotation);
        PhotonNetwork.Instantiate("Prefabs/재료/" + ingredientObject[index].name, newPosition, pos[index].rotation);
        exit();
    }
}