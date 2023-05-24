using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_dayAndTime : MonoBehaviour
{
    public TMP_Text tmp;

    void Start()
    {
        Managers.Date.init();
        Debug.Log(tmp.text);
    }

    // Update is called once per frame
    void Update()
    {
        
        tmp.text = Managers.Date.day + "일 /" + (int)Managers.Date.time;
    }
}
