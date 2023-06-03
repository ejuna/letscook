using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Define;

public class OrderIngredientController : MonoBehaviour
{
  public Button[] btnList = new Button[4];
  public GameObject[] uiList = new GameObject[4];
  public GameObject[] container = new GameObject[4];

  IngredientContainer ingredientContainer;

  void Start()
  {
    for(int i=0;i<4;i++){
      uiList[i].SetActive(false);
      Button btn = btnList[i];
      btn.onClick.AddListener(() => onClickTypeButton(btn,i));
    }
  }

  public void onClickTypeButton(Button button,int index){
    Debug.Log(button.ToString());
    uiList[index].SetActive(true);
    ingredientContainer = uiList[index].GetComponent<IngredientContainer>();

    Button[] buttons = uiList[index].GetComponentsInChildren<Button>(true);
    foreach(Button btn in buttons){
      btn.onClick.AddListener(() => selectOrder(btn));
    }
  }

  public void selectOrder(Button btn){
    
  }
  public void orderIngredient(){

  }

}
