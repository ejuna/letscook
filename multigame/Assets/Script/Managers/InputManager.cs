using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    //키보드 입력에 대한 이벤트 생성
    public Action KeyAction = null;

    public void OnUpdate()
    {
        if (Input.anyKey == false)
            return;
        if (KeyAction != null)
            KeyAction.Invoke();
    }
    public void Clear()
    {
        KeyAction = null;
    }
}
