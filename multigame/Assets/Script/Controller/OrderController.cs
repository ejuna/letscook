using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OrderController : MonoBehaviour
{
    private static float timer = 0f;
    OrderManager Orders;
    public Canvas canvas;

    void Start()
    {
        Orders = Managers.Orders;
    }

    void Update()
    {
    timer += Time.deltaTime;
    if (timer >= 5f)
    {
      Order order = Orders.createOrder();
      setOrderRecipe(order);

      //타이머 초기화
      timer = 0f;
    }

  //order에서 검사해서 
  }

  public void setOrderRecipe(Order order){
    GameObject orderPrefab;
    int recipeSize = order.Food.Ingredients.Count;

  //프리팹 선택
    string prefabLocation = "";
    switch(recipeSize){
      case 2:
        prefabLocation = "OrderTwo";
        break;
      case 3:
        prefabLocation = "OrderThree";
        break;
      case 4:
        prefabLocation = "OrderFour";
        break;
      case 5:
        prefabLocation = "OrderFive";
        break;
      default: break;
    }
    orderPrefab = Resources.Load<GameObject>("Prefabs/OrderRecipe/" + prefabLocation);
    GameObject orderUI = Instantiate(orderPrefab, canvas.transform);

    //음식 설정
    TextMeshProUGUI foodText = orderUI.transform.Find("FoodName").GetComponent<TextMeshProUGUI>();
    foodText.text = order.Food.FoodName;

    Image image = orderUI.transform.Find("Food").GetComponent<Image>();
    Texture2D texture = order.Food.Img;
    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

    image.sprite = sprite;

    //재료 설정

    Image ingredients = orderUI.transform.Find("Ingredients").GetComponent<Image>();

    for(int i=0;i<recipeSize;i++){
      Image ingredient = ingredients.transform.Find("Ingredient"+(i+1)).GetComponent<Image>();
      Texture2D ingre_texture = order.Food.Ingredients[i].Img;
      Sprite ingre_sprite = Sprite.Create(ingre_texture, new Rect(0, 0, ingre_texture.width, ingre_texture.height), Vector2.one * 0.5f);
      ingredient.sprite = ingre_sprite;

      TextMeshProUGUI ingre_text = ingredient.GetComponentInChildren<TextMeshProUGUI>();

      ingre_text.text = order.Food.Ingredients[i].IngredientName;

    }

    //타임 설정
    Slider timer = orderPrefab.GetComponentInChildren<Slider>();
    timer.maxValue = order.TimeLimit;
  }
}
