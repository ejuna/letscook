using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    //이벤트 이벤트 발생하는 시기에 랜덤한 시간을 생성
    float randtime = -1.0f;
    //랜덤으로 생성된 단체손님 수
    int rnadGroupNum = 0;
    int maxDate = 0;
    int maxMoney = 0;
    public void OnUpdate()
    {

        //조건을 확인 하면서 이벤트 발생시킨다.
        //오늘 주문횟수가 10이면 미식가 이벤트 발생
        if (Managers.Orders.complete == -1)
        {
            //주문생성에 필요한 변수등의 식을 생성()
            
        }
        //그시간을 이용하여 랜덤한 시간에 단체손님 발생
        if (Managers.Date.time==randtime) {
            //단체손님 수를 계산(명성,오늘처리한 주문수, 현재주문수,현재까지 성공한 주문수)
            
        }
        //목표일짜 및 목표 금액 달성시 게임 클리어
        if (Managers.Date.day == maxDate || Managers.Money.money == maxMoney)
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
        Managers.Orders.createGourmandOrder();
    }
    public void groupGuestEvent() 
    {
        //단체손님 주문생성
        Managers.Orders.createGroupGuestOrder(rnadGroupNum);
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
