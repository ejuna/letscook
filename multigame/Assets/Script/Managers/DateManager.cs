using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager
{
    //날자 정보(일수 정보, 하루시간 정보)를 저장하는 클래스 생성,
    public int day { get; set; }
    public float time { get; set; }
    public bool isChangeDay { get; set; }

    public void init()
    {
        day = 1;
        time = 0;
        isChangeDay = false;
    }
    public void OnUpdate()
    {
        time+=Time.deltaTime;
        if (time >= Constants.Day_MAX_time&&Managers.Orders.isOrders()==false)
        {
            dateUpdate();
            Managers.Life.Init();
            isChangeDay = true;
        }

        //아니면 그냥 진행 하여 하루정산 창을 출현 하고 그 화면 뒤에서 초기화 실행
    }

    public void dateUpdate() 
    {
        day++;
        time = 0;
    }
    public void Clear() 
    {
        day = 0;
        time = 0;
    isChangeDay = false;
    }
}

