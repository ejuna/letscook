using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OrderRecipe : MonoBehaviour
{
    private GameObject orderUI;
    public GameObject OrderUI{ get { return orderUI; } }
  // Update is called once per frame

  private void Awake()
  {
  }

  void Update()
    {
        
    }

  public void onDisplay(){

  }

  public void deleteRecipe()
  {
    Destroy(orderUI);
  }

  public void setRecipe(Order order,GameObject orderPrefab)
  {
      Canvas canvas = GameObject.Find("Orders").GetComponent<Canvas>();
      int recipeSize = order.Food.Ingredients.Count;
      orderUI = Instantiate(orderPrefab);

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
      Slider timer = orderPrefab.GetComponentInChildren<Slider>();
      timer.maxValue = order.TimeLimit;

    orderUI.transform.SetParent(canvas.transform);
  }
}
