using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OrderController : MonoBehaviour
{
    private static float timer = 0f;
    OrderManager Orders;
    public GameObject orderPrefab;
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
      Order order = Orders.createOrder(50);
      GameObject orderUI = Instantiate(orderPrefab,canvas.transform);
      
      //음식 설정
      TextMeshProUGUI foodText = orderUI.transform.Find("FoodName").GetComponent<TextMeshProUGUI>();
      foodText.text = order.Food.FoodName;

      Image image = orderUI.transform.Find("Food").GetComponent<Image>();
      //Sprite sprite = Sprite.Create(order.Food.Img, image.rectTransform.rect, Vector2.one * 0.5f);
      Texture2D texture = order.Food.Img;
      Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

      image.sprite = sprite;

      //재료 설정

      Image ingredients = orderUI.transform.Find("Ingredients").GetComponent<Image>();
      Image ingredient1 = ingredients.transform.Find("Ingredient1").GetComponent<Image>();
      Image ingredient2 = ingredients.transform.Find("Ingredient2").GetComponent<Image>();

      Texture2D ingre1_texture = order.Food.Ingredients[0].Img;
      Texture2D ingre2_texture = order.Food.Ingredients[1].Img;

      Sprite ingre1_sprite = Sprite.Create(ingre1_texture, new Rect(0, 0, ingre1_texture.width, ingre1_texture.height), Vector2.one * 0.5f);
      Sprite ingre2_sprite = Sprite.Create(ingre2_texture, new Rect(0, 0, ingre2_texture.width, ingre2_texture.height), Vector2.one * 0.5f);

      ingredient1.sprite = ingre1_sprite;
      ingredient2.sprite = ingre2_sprite;

      TextMeshProUGUI ingre1_text = ingredient1.GetComponentInChildren<TextMeshProUGUI>();
      TextMeshProUGUI ingre2_text = ingredient2.GetComponentInChildren<TextMeshProUGUI>();

      ingre1_text.text = order.Food.Ingredients[0].IngredientName;
      ingre2_text.text = order.Food.Ingredients[1].IngredientName;

      //타이머 초기화
      timer = 0f;
    }

  }
}
