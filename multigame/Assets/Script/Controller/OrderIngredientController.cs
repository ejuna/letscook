using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Define;

public class OrderIngredientController : MonoBehaviour
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
  

  void Start()
  {
    type = -1;
    inputUI.SetActive(false);
    noMoneyUI.SetActive(false);
    for (int i=0;i<4;i++){
      uiList[i].SetActive(false);
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
    int orderCount =int.Parse(orderNum.text);

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

    int order = int.Parse(orderNum.text);
    ingredientContainer.countIncrease(selectIngredientIndex, order);
    inputUI.SetActive(false);
  }
}
