using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public static Managers Instance{get { init();  return s_instance; } }

    EventManager _event = new EventManager();
    DateManager _date =new DateManager();
    SceneManagerMake _scene = new SceneManagerMake();
    InputManager _input = new InputManager();
    IngredientManager _ingredient = new IngredientManager();
    MoneyManager _money = new MoneyManager();
    OrderManager _order = new OrderManager();
    public static EventManager Event { get { return Instance._event; } }
    public static DateManager Date { get { return Instance._date; } }
    public static SceneManagerMake Scene { get { return Instance._scene; } }
    public static InputManager Input { get {  return Instance._input; } }
    public static IngredientManager Ingredient { get { return Instance._ingredient; } }
    public static MoneyManager Money { get { return Instance._money; } }
    public static OrderManager Order{ get { return Instance._order; } }  
  // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        Event.OnUpdate();
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

    public static void Clear()
    {
        Event.Clear();
        Date.Clear();
        Scene.Clear();
        Input.Clear();
        Ingredient.Clear();
        Money.Clear();
        Order.Clear();
    }
}
