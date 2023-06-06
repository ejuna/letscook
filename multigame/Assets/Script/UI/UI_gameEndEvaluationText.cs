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
            text = "주문에 실패하여, 패배하셨습니다.\n"
            + "목표 금액: " + Managers.Event.targetMoney + "  달성 금액: " + Managers.Money.money + "\n"
            + "목표 일수: " + Managers.Event.targetDate + "  달성 일수: " + Managers.Date.day;
        }
        else if(Managers.Event.targetMoney<=Managers.Money.money)
        {
            text = "목표했던 금액에 달성해 클리어 하셨습니다.\n"
            + "목표 금액: " + Managers.Event.targetMoney + "  달성 금액: " + Managers.Money.money + "\n"
            + "목표 일수: " + Managers.Event.targetDate + "  달성 일수: " + Managers.Date.day;
        }
        else if (Managers.Event.targetDate <= Managers.Date.day)
        {
            text = "목표했던 일수에 달성해 클리어 하셨습니다.\n"
            + "목표 일수: " + Managers.Event.targetDate + "  달성 일수: " + Managers.Date.day + "\n"
            + "목표 금액: " + Managers.Event.targetMoney + "  달성 금액: " + Managers.Money.money;
        }
        //if(포인트 계산하고 이를 통해 다른문구를 띄운다.)
        tmp.text = text;

    }

 

    // Update is called once per frame
    void Update()
    {

    }
}
