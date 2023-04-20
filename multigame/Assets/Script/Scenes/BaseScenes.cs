using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScenes : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.None;
    void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {

    }
    public abstract void Clear();
}
