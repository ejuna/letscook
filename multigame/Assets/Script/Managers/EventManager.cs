using Photon.Pun;
using POpusCodec.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager
{
    public Action<string> WarningAction;

    private float randOrderCountTrigger;
    //이벤트 이벤트 발생하는 시기에 랜덤한 시간을 생성
    private float randTimeTrigger;
    //랜덤으로 생성된 단체손님 수
    int randGroupNum = 0;
    public GameObject uiContainer;
    public GameObject warning;

    public bool isTodayGourmand;
    public bool isTodayGroupGeuset;
    bool isGameOver;
    public int targetDate { get; set; }//무한이면 -1
    public int targetMoney { get; set; }//무한이면 -1

    private float timer;
    public string eventInfo;


    public void init()
    {
        eventInfo = "";
        timer = 0.0f;
        randOrderCountTrigger = UnityEngine.Random.Range(0, 7);
        randTimeTrigger = UnityEngine.Random.Range(30, Constants.Day_MAX_time);
        //randOrderCountTrigger = -1;
        //randTimeTrigger = 1;
        WarningAction -= Warning;
        WarningAction += Warning;
        isTodayGourmand = false;
        isTodayGroupGeuset = false;
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
              Debug.Log("Gourmand");
              eventInfo = "미식가 이벤트 발생!";
              gourmandEvent();
              isTodayGourmand = false;

        }
        //시간 조건을 확인하고 랜덤한 시간에 단체손님 발생
        if (Managers.Date.time >= randTimeTrigger && isTodayGroupGeuset) {
            Debug.Log("GroupGeuset");
            eventInfo = "단체손님 이벤트 발생!";
            groupGuestEvent();
            isTodayGroupGeuset = false;
        }
        //목표일짜 및 목표 금액 달성시 게임 클리어
        if (isGameOver && (Managers.Date.day == targetDate || Managers.Money.money >= targetMoney|| Managers.Life.life <= 0 ))
        {
            gameClear();
            isGameOver = false;
        }

        if(!eventInfo.Equals("")){
          timer += Time.deltaTime;
          Warning(eventInfo);
          if (timer > 5.0f){
              Debug.Log(timer);
              eventInfo = "";
              warning.SetActive(false);
              timer = 0.0f;
          }
        }
  }
    public void Warning(String str)
    {
        warning.SetActive(true);
        TextMeshProUGUI warningText = warning.GetComponentInChildren<TextMeshProUGUI>();
        warningText.text = str;
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
