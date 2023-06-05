using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class OrderRecipe : MonoBehaviour
{
    public GameObject orderUI;
    public GameObject OrderUI{ get { return orderUI; } }
    public Slider timer;

    public bool isCreate;
    public int id;


  private void Awake()
  {

  }

  void Update()
  {
    if(isCreate){
      if (timer.value > 0.0f)
      {
        timer.value -= Time.deltaTime;
      }
      else
      {
        Managers.Life.lifeDecrease();
        Managers.Orders.deleteOrder(id);
      }
    }
  }

  public void onDisplay(){
    orderUI.SetActive(true);
  }

  public void onDisplayFirstOrder(){
    Image image = orderUI.transform.Find("Image").GetComponent<Image>(); ;
    image.color = new Color32(255,129,124,255);

  }

  public void deleteRecipe()
  {
    isCreate = false;
    Destroy(orderUI);
  }


  public OrderRecipe GetRecipe(Order order, GameObject orderPrefab, int id)
  {
    orderUI = PhotonNetwork.Instantiate("Prefab/OrderRecipe/"+orderPrefab.name,transform.position,transform.rotation);
    OrderRecipe recipe = orderUI.GetComponent<OrderRecipe>();
    recipe.setRecipe(order, orderUI, id);
    return recipe;
  }


  public void setRecipe(Order order,GameObject orderUI,int id)
  {
      this.id = id;
      Canvas canvas = GameObject.Find("Orders").GetComponent<Canvas>();
      int recipeSize = order.Food.Ingredients.Count;
      orderUI.name = id.ToString();

      //음식 설정
      TextMeshProUGUI foodText = orderUI.transform.Find("FoodName").GetComponent<TextMeshProUGUI>();
      foodText.text = order.Food.FoodName;

      Image image = orderUI.transform.Find("Food").GetComponent<Image>();
      Texture2D texture = order.Food.Img;
      Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

      image.sprite = sprite;

      //재료 설정

      Image ingredients = orderUI.transform.Find("Ingredients").GetComponent<Image>();

      for (int i = 0; i < recipeSize; i++)
      {
        Image ingredient = ingredients.transform.Find("Ingredient" + (i + 1)).GetComponent<Image>();
        Texture2D ingre_texture = order.Food.Ingredients[i].Img;
        Sprite ingre_sprite = Sprite.Create(ingre_texture, new Rect(0, 0, ingre_texture.width, ingre_texture.height), Vector2.one * 0.5f);
        ingredient.sprite = ingre_sprite;

        TextMeshProUGUI ingre_text = ingredient.GetComponentInChildren<TextMeshProUGUI>();

        ingre_text.text = order.Food.Ingredients[i].IngredientName;

      }

      //타임 설정
      timer = orderUI.GetComponentInChildren<Slider>();
      timer.maxValue = order.TimeLimit;
      timer.value = timer.maxValue;
      orderUI.transform.SetParent(canvas.transform);

      this.orderUI = orderUI;
      orderUI.SetActive(false);
      isCreate = true;
  }
}
