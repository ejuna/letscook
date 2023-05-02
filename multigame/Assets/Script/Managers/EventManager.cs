using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    int orderCount = 0;
    int time = 0;
    public void OnUpdate()
    {
        //조건을 확인 하면서 이벤트 발생시킨다.
        //오늘 주문횟수가 10이면 미식가 이벤트 발생
        if (orderCount == 10)
        {
            gourmandEvent();
        }
        //현재 시간 1000이면 단체손님 이벤트 발생
        if (time == 1000) {
            groupGuestEvent();
        }
    }
    public void gourmandEvent() 
    {
        //미식가 주문생성
    }
    public void groupGuestEvent() 
    {
        //단체손님 주문생성
    }

    public void Clear() { }
}
