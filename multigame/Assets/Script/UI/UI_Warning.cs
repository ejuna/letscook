using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Warning : MonoBehaviour
{
    public TMP_Text tmp;
    public GameObject uiObject;
    // Start is called before the first frame update
    void Awake()
    {
        Managers.Event.WarningAction -= setText;
        Managers.Event.WarningAction += setText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void setText(String str)
    {
        tmp.text = str+"이벤트가 발생했습니다.";
        Invoke(nameof(DeactivateUI), 5f);
    }
    private void DeactivateUI()
    {
        uiObject.SetActive(false);
    }
}
