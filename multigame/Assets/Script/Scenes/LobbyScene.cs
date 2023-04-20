using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : BaseScenes
{
    void Start()
    {
        Init();
    }
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Lobby;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) {
            SceneManager.LoadScene("looding");
        }
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

}
