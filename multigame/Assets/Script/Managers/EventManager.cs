using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    private float randOrderCountTrigger;
    //이벤트 이벤트 발생하는 시기에 랜덤한 시간을 생성
    private float randTimeTrigger;
    //랜덤으로 생성된 단체손님 수
    int randGroupNum = 0;

    public int targetDate { get; set; }//무한이면 -1
    public int targetMoney { get; set; }//무한이면 -1

    public void init()
    {
        randOrderCountTrigger = Random.Range(0, 15);
        randTimeTrigger = Random.Range(0, Constants.Day_MAX_time);
    }
    public void OnUpdate()
    {
       
        //조건을 확인 하면서 미식가 이벤트 발생시킨다.
        if (Managers.Orders.complete == randOrderCountTrigger)
        {
            gourmandEvent();
        }
        //시간 조건을 확인하고 랜덤한 시간에 단체손님 발생
        if (Managers.Date.time == randTimeTrigger) {

            groupGuestEvent();
        }
        //목표일짜 및 목표 금액 달성시 게임 클리어
        if (Managers.Date.day == targetDate || Managers.Money.money == targetMoney)
        {
            gameClear();
        }
        if (Managers.Life.getLife() <= 0)
        {
            gameOver();
        }
    }
    public void gourmandEvent() 
    {
        //미식가 주문생성
        //주문생성에 필요한 변수등의 식을 생성(명성,)
        Managers.Orders.createGourmandOrder();
    }
    public void groupGuestEvent() 
    {
        //단체손님 주문생성
        Managers.Orders.createGroupGuestOrder(randGroupNum);
    }

    public void gameOver()
    {
        //게임 오버씬으로 전환한다.
    }

    public void gameClear()
    {
        //게임 클리어씬으로 전환한다.
    }
    public void Clear() { }
}
