using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndScene : BaseScenes
{
    void Awake()
    {
        Init();
    }

    private void Update()
    {
        //씬 변경하는 부분
        if(Input.GetKeyDown(KeyCode.Q)) {
            Managers.Scene.LoadScene(Define.Scene.None);
        }
    }
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.GameEnd;
    }
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

}
