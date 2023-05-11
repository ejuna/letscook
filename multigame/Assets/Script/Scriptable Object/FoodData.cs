using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Food Data", menuName = "Scriptable Object/Food Data", order = int.MaxValue)]
public class FoodData : ScriptableObject
{
  [SerializeField]
  private string foodName;
  public string FoodName{ get{ return foodName; } }

  [SerializeField]
  private int foodTier;
  public int FoodTier{ get { return foodTier; } }

  [SerializeField]
  private List<IngredientData> ingredients;
  public List<IngredientData> Ingredients{ get { return ingredients; } }

  [SerializeField]
  private Image img;
  public Image Img { get { return img; } }

}
