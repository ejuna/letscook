using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Define;
using Photon.Pun;

public class OrderIngredientController : MonoBehaviourPun, IPunObservable
{
  public GameObject[] uiList = new GameObject[4];
  public GameObject[] container = new GameObject[4];
  public GameObject inputUI;
  public GameObject noMoneyUI;
  public TMP_InputField orderNum;
  

  private IngredientContainer ingredientContainer;
  private int type;
  private int selectIngredientIndex;
  private int cost;
  private int orderCount;

  private int isOrder = 0;

  void Start()
  {
    type = -1;
    inputUI.SetActive(false);
    noMoneyUI.SetActive(false);
    for (int i=0;i<4;i++){
      uiList[i].SetActive(false);
    }
  }

  void Update(){

    if (isOrder == 1)
    {
      Debug.Log("ddd");
      
      isOrder = 0;
    }

  }

  public void onClickTypeButton(int index){
    if(type != -1){
      uiList[type].SetActive(false);
    }
    type = index;
    uiList[type].SetActive(true);
    ingredientContainer = container[type].GetComponent<IngredientContainer>();
  }


  public void selectOrder(int index){
    selectIngredientIndex = index;
    inputUI.SetActive(true);

    //초기화
    orderNum.text = "0";
  }

  public void exit(GameObject ui){
    ui.SetActive(false);
  }

  public void onInputValue(){
    TextMeshProUGUI priceText = inputUI.transform.Find("price").GetComponent<TextMeshProUGUI>();

    //개수 입력받은거
    orderCount =int.Parse(orderNum.text);

    //재료 가격 가져오기
    Ingredient orderIngredient = ingredientContainer.ingredientObject[selectIngredientIndex].GetComponent<Ingredient>();
    int ingredientPrice = orderIngredient.ingredientData.Price;
    
    //비용계산
    cost = ingredientPrice * orderCount;
    priceText.text = "Price : " + cost.ToString();
  }

  public void orderIngredient(){

    //돈 감소 -> 동기화 필요
    if (!Managers.Money.moneyDecrease(cost))
    {
      noMoneyUI.SetActive(true);
      Debug.Log("돈이부족");
      inputUI.SetActive(false);
      return;
    }

    ingredientContainer.countIncrease(selectIngredientIndex, orderCount);
    isOrder = 1;
    inputUI.SetActive(false);
  }


  public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    Debug.Log("ㄹ포톤뷰");
    if (stream.IsWriting)
    {
      // We own this player: send the others our data
      stream.SendNext(type);
      stream.SendNext(selectIngredientIndex);
      stream.SendNext(orderCount);
      stream.SendNext(isOrder);
    }
    else
    {
      type = (int)stream.ReceiveNext();
      selectIngredientIndex = (int)stream.ReceiveNext();
      orderCount = (int)stream.ReceiveNext();
      isOrder = (int)stream.ReceiveNext();

      Debug.Log(type + "," + selectIngredientIndex + "," + orderCount + "," + isOrder);
    }
  }
}
