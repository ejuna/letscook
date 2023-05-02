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
            SceneManager.LoadScene("test");
        }
    }
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Test2;
    }
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }



}
