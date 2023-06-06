using Photon.Pun;
using POpusCodec.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager: MonoBehaviour
{
    public Action<string> WarningAction;

    private float randOrderCountTrigger;
    //이벤트 이벤트 발생하는 시기에 랜덤한 시간을 생성
    private float randTimeTrigger;
    //랜덤으로 생성된 단체손님 수
    int randGroupNum = 0;
    public GameObject uiContainer;
    public GameObject warning;
    public PhotonView PV;

    bool isTodayGourmand;
    bool isTodayGroupGeuset;
    bool isGameOver;
    public int targetDate { get; set; }//무한이면 -1
    public int targetMoney { get; set; }//무한이면 -1
    
    public void init()
    {
        randOrderCountTrigger = UnityEngine.Random.Range(0, 7);
        randTimeTrigger = UnityEngine.Random.Range(30, Constants.Day_MAX_time);
        //randOrderCountTrigger = -1;
        //randTimeTrigger = 1;
        WarningAction -= Warning;
        WarningAction += Warning;
        isTodayGourmand = true;
        isTodayGroupGeuset = true;
        isGameOver = true;

        //게임 종료 UI연결
        uiContainer = GameObject.FindGameObjectWithTag("GameEnd");
        uiContainer.SetActive(false);
        warning = GameObject.FindGameObjectWithTag("warning");
        warning.SetActive(false);

        //============이준하 수정=============//
        GameObject obj = GameObject.Find("PhotonManager");

        targetDate = obj.GetComponent<PhotonManager>().targetDay;
        targetMoney = obj.GetComponent<PhotonManager>().targetMoney;
        //====================================//
    }


    public void OnUpdate()
      {
        //조건을 확인 하면서 미식가 이벤트 발생시킨다.
        if (Managers.Orders.complete >= randOrderCountTrigger && isTodayGourmand)
        {
            if (PV.IsMine) PV.RPC(nameof(gourmand), RpcTarget.All);
        }
        //시간 조건을 확인하고 랜덤한 시간에 단체손님 발생
        if (Managers.Date.time >= randTimeTrigger && isTodayGroupGeuset) {
            if (PV.IsMine) PV.RPC(nameof(groupGuest), RpcTarget.All);
        }
        //목표일짜 및 목표 금액 달성시 게임 클리어
        if (isGameOver && (Managers.Date.day == targetDate || Managers.Money.money >= targetMoney|| Managers.Life.life <= 0 ))
        {
            gameClear();
            isGameOver = false;
        }
    }
    public void Warning(String str)
    {
        warning.SetActive(true);
    }
    [PunRPC]
    public void gourmand()
    {
        Debug.Log("Gourmand");
        WarningAction("미식가 이벤트");
        gourmandEvent();
        isTodayGourmand = false;
    }
    public void gourmandEvent() 
    {
        int fameLogicCount = 0;
        int fame = Managers.Fame.fame;
        while (fame >= 1)
        {
            fame = fame / 2;
            fameLogicCount++;
        }
        //미식가 주문생성
        //주문생성에 필요한 변수등의 식을 생성(명성,)
        Managers.Orders.createGourmandOrder(fameLogicCount);
    }
    [PunRPC]
    public void groupGuest() 
    {
        Debug.Log("GroupGeuset");
        WarningAction("단체손님 이벤트");
        groupGuestEvent();
        isTodayGroupGeuset = false;
    }
    public void groupGuestEvent() 
    {
        //명성,현재주문수,
        //단체손님 주문생성
        int fameLogicCount = 0;
        int fame = Managers.Fame.fame;
        int complete = Managers.Orders.complete;
        while (fame >= 1)
        {
            fame = fame / 2;
            fameLogicCount++;
        }
        fameLogicCount = fameLogicCount + complete;
        while (fameLogicCount >= 4)
        {
            fameLogicCount=fameLogicCount / 2;
        }
        if (fameLogicCount < 2) fameLogicCount=fameLogicCount + 2;


        Managers.Orders.createGroupGuestOrder(fameLogicCount);
    }

    public void gameClear()
    {
        uiContainer.SetActive(true);
    }
    public void Clear() { }
}
