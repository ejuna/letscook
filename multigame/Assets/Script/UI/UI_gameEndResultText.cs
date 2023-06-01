using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_gameEndResultText : MonoBehaviour
{
    public TMP_Text tmp;
    // Start is called before the first frame update
    void Start()
    {
        if (Managers.Life.life <= 0)
        {
            tmp.text = "게임오버";
        }
        else
        {
            tmp.text = "게임클리어";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
