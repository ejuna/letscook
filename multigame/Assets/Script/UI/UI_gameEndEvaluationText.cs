using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_gameEndEvaluationText : MonoBehaviour
{
    public TMP_Text tmp;

    // Start is called before the first frame update
    void Start()
    {
        string text="";
        int middlePoint = Managers.Orders.totalComplete * Constants.ORDER_POINT;

        if (Managers.Life.life <= 0)
        {
            text = "주문에 실패하여, 패배하셨습니다.";
        }

        if (Managers.Fame.fame < middlePoint * 0.7f)
        {
            text = "부족한 성적이지만 클리어를 축하합니다.";
        }
        if (Managers.Fame.fame > middlePoint * 0.7f)
        {
            text = "아쉬운 성적이지만 클리어를 축하합니다.";
        }
        if (Managers.Fame.fame > middlePoint * 0.9f)
        {
            text = "적단한 성적이지만 클리어를 축하합니다.";
        }
        if (Managers.Fame.fame > middlePoint * 0.1f)
        {
            text = "보통 성적으로 클리어를 축하합니다.";
        }
        if (Managers.Fame.fame > middlePoint * 1.1f)
        {
            text = "우수한 성적으로 클리어를 축하합니다.";
        }
        if (Managers.Fame.fame > middlePoint * 1.3f)
        {
            text = "엄청난 성적으로 클리어를 축하합니다.";
        }
        //if(포인트 계산하고 이를 통해 다른문구를 띄운다.)
        tmp.text = text;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
