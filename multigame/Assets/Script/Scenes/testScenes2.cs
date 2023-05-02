using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testScenes2 : BaseScenes
{
    void Awake()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.Test);
        }
    }
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Test2;
    }
    public override void Clear()
    {
        Debug.Log("test2");
    }
}
