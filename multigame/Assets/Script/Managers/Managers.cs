using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public static Managers Instance { get { init(); return s_instance; } }

    EventManager _event = new EventManager();
    DateManager _date = new DateManager();
    SceneManagerMake _scene = new SceneManagerMake();
    InputManager _input = new InputManager();
    IngredientManager _ingredient = new IngredientManager();
    OrderManager _order = new OrderManager();
    MoneyManager _money = new MoneyManager();
    LifeManager _life = new LifeManager();
    FameManager _fame = new FameManager();
    ResourceManager _resource = new ResourceManager();
    public static EventManager Event { get { return Instance._event; } }
    public static DateManager Date { get { return Instance._date; } }
    public static SceneManagerMake Scene { get { return Instance._scene; } }
    public static InputManager Input { get { return Instance._input; } }
    public static IngredientManager Ingredient { get { return Instance._ingredient; } }
    public static MoneyManager Money { get { return Instance._money; } }
    public static LifeManager Life { get { return Instance._life; } }
    public static OrderManager Orders { get { return Instance._order; } }
    public static FameManager Fame { get{ return Instance._fame; } }
    public static ResourceManager Resource { get { return Instance._resource;} }

 

  void Start()
    {
        init();
        Orders.init();
  }

    // Update is called once per frame
    void Update()
    {
        Event.OnUpdate();
        Input.OnUpdate();
        Date.OnUpdate();
        
        if(Date.isChangeDay == true){ //날짜 바뀌면
          Orders.DateUpdate(Date.day, Fame.fame);
          Date.isChangeDay = false;
        }
        
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

    public static void Clear()
    {
        Event.Clear();
        Date.Clear();
        Scene.Clear();
        Input.Clear();
        Ingredient.Clear();
        Money.Clear();
        Orders.Clear();
    }
}
