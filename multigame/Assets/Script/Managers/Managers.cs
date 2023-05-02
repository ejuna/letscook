using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public static Managers Instance{get { init();  return s_instance; } }

    EventManager _event = new EventManager();
    DateManager _date =new DateManager();
    InputManager _input = new InputManager();
    public static EventManager Event { get { return Instance._event; } }
    public static DateManager Date { get { return Instance._date; } }
    public static InputManager Input { get {  return Instance._input; } }

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        Input.OnUpdate();
    }

    static void init ()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject { name = "@Managers"};
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}
