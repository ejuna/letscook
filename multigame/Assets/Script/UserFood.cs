using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserFood : MonoBehaviour
{
    public List<IngredientData> Ingredients { get; set; }    

    void Start()
    {
    Ingredients = new List<IngredientData>();       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
