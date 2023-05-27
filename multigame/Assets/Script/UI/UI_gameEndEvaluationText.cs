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
        if (Managers.Life.getLife() <= 0)
        {
            text = "주문에 실패하셨습니다.";
        }
        //if(포인트 계산하고 이를 통해 다른문구를 띄운다.)
        tmp.text = text;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
