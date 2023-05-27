using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearScene : BaseScenes
{
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.GameClear;
    }

}
