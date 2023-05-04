using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager
{
    //날자 정보(일수 정보, 하루시간 정보)를 저장하는 클래스 생성,
    public float day { get; set; }
    public float time { get; set; }

    public const float Day_MAX_time= 1000;


    //클래스는 별도의 저장소에서 관리?
    //dateUpadte에서 하루시간이 일정수치에 도달하면 일수가 넘어가면서

    public void init()
    {
        day = 1;
        time = 0;
    }
    public void onUpdate()
    {
        time+=Time.deltaTime;
        if (time == Day_MAX_time)
        //오더를 확인하고 남은 오더가 0일 경우 실행 하도록한다.
            dateUpdate();
        //라이프 초기화 <- 
        //하루에서 다음날로 넘어갈때 잠깐 다른씬을 보여주는가? -> 씬전환하고 날짜 업데이트를 실행  
        //아니면 그냥 진행 하는가? -> 그냥 넘어간다.
        //날짜 넘어갔을때 실행 해야 함수들을 실행한다.
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
    }
}

