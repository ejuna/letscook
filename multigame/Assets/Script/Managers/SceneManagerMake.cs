using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerMake
{
    public BaseScenes CurrentScene { get { return GameObject.FindAnyObjectByType<BaseScenes>(); } }
    public void LoadScene(Define.Scene scene)
    {
        Managers.Clear();
        SceneManager.LoadScene(getSceneName(scene));
    }

    string getSceneName(Define.Scene scene)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), scene);
        return name;
    }
    public void Clear() { 
        CurrentScene.Clear(); 
    }
}
