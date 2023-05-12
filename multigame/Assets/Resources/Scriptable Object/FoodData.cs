using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
  private Texture2D img;
  public Texture2D Img { get { return img; } }

}
