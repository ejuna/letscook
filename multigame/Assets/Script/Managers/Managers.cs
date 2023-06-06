using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Managers : MonoBehaviourPun, IPunObservable
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

    public PhotonView PV;
    public static PhotonView PhotonView { get { return Instance.PV; } }
    void Start()
    {
        init();

  }

    // Update is called once per frame
    void Update()
    {
        Event.OnUpdate();
        Input.OnUpdate();
        Date.OnUpdate();
        if(PhotonNetwork.IsMasterClient){
          Orders.OnUpdate();
        }
        
        if(Date.isChangeDay == true){ //날짜 바뀌면
          Orders.DateUpdate();
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
                go.AddComponent<PhotonView>();
            }
            s_instance = go.GetComponent<Managers>();

            Orders.init();
            Event.init();
            Life.Init();
        }
  }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(Life.life);
            stream.SendNext(Date.day);
            stream.SendNext(Date.time);
            stream.SendNext(Fame.fame);
            stream.SendNext(Money.money);
            stream.SendNext(Money.tempMoney);
            stream.SendNext(Fame.tempFame);
            stream.SendNext(Orders.tempOrder);

        }
        else
        {
            // Network player, receive data
            Life.life = (int)stream.ReceiveNext();
            Date.day = (int)stream.ReceiveNext();
            Date.time = (float)stream.ReceiveNext();
            Fame.fame = (int)stream.ReceiveNext();
            Money.money = (int)stream.ReceiveNext();
            Money.tempMoney = (int)stream.ReceiveNext();
            Fame.tempFame = (int)stream.ReceiveNext();
            Orders.tempOrder = (int)stream.ReceiveNext();

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

    //일일 정산 창 활성화
    public static void Result()
    {
        GameObject.Find("Canvas").transform.Find("Result").gameObject.SetActive(true);
        GameObject.Find("Money-Text").gameObject.GetComponent<TextMeshProUGUI>().text = "Money  " + (Money.money - Money.tempMoney);
        GameObject.Find("Fame-Text").gameObject.GetComponent<TextMeshProUGUI>().text = "Fame  " + (Fame.fame - Fame.tempFame);

        int point = (Money.money - Money.tempMoney) + ((Fame.fame - Fame.tempFame) * 10);

        GameObject.Find("Result").transform.Find("Star1").gameObject.SetActive(false);
        GameObject.Find("Result").transform.Find("Star2").gameObject.SetActive(false);
        GameObject.Find("Result").transform.Find("Star3").gameObject.SetActive(false);
        GameObject.Find("Result").transform.Find("Star4").gameObject.SetActive(false);
        GameObject.Find("Result").transform.Find("Star5").gameObject.SetActive(false);

        if (point <= 200)
        {
            GameObject.Find("Result").transform.Find("Star1").gameObject.SetActive(true);
        }
        else if (point <= 400)
        {
            GameObject.Find("Result").transform.Find("Star1").gameObject.SetActive(true);
            GameObject.Find("Result").transform.Find("Star2").gameObject.SetActive(true);
        }
        else if (point <= 600)
        {
            GameObject.Find("Result").transform.Find("Star1").gameObject.SetActive(true);
            GameObject.Find("Result").transform.Find("Star2").gameObject.SetActive(true);
            GameObject.Find("Result").transform.Find("Star3").gameObject.SetActive(true);
        }
        else if (point <= 800)
        {
            GameObject.Find("Result").transform.Find("Star1").gameObject.SetActive(true);
            GameObject.Find("Result").transform.Find("Star2").gameObject.SetActive(true);
            GameObject.Find("Result").transform.Find("Star3").gameObject.SetActive(true);
            GameObject.Find("Result").transform.Find("Star4").gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("Result").transform.Find("Star1").gameObject.SetActive(true);
            GameObject.Find("Result").transform.Find("Star2").gameObject.SetActive(true);
            GameObject.Find("Result").transform.Find("Star3").gameObject.SetActive(true);
            GameObject.Find("Result").transform.Find("Star4").gameObject.SetActive(true);
            GameObject.Find("Result").transform.Find("Star5").gameObject.SetActive(true);
        }
    }

    //정산 창에서 진행하기 버튼 클릭
    public void NextDayButtonClick()
    {
        GameObject.Find("Result").gameObject.SetActive(false);
        Money.tempMoney = Money.money;
        Fame.tempFame = Fame.fame;
        Date.dateUpdate();
        Orders.TimeClear();
    }
}
