using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager
{
  public List<Order> orderList;
  public List<FoodData> allFoods=new List<FoodData>();//게임 프리팹 넣기?
  public List<FoodData> todayFoods=new List<FoodData>();

  public int complete { get; set; }
  public int totalComplete{ get; set; }

  private const float DEFALT_TIME = 60.0f;

  private static bool isInit = false;
  private float timer;

  private int displayCount = 0;

  public OrderManager(){
    timer = 0f;
  }

  public void OnUpdate(){
    timer += Time.deltaTime;

    if (timer >= 30f)
    {
      timer = 0f;
      createOrder();
    }

    if(displayCount < 5){
      if(displayCount < orderList.Count){
        orderList[displayCount++].Recipe.onDisplay();
      }
      if(0 < orderList.Count){
        orderList[0].Recipe.onDisplayFirstOrder();
      }
    }
  }

  private void getAllFoodList() {
    FoodData[] foodDataArr = Resources.LoadAll<FoodData>("Scriptable Object/Food");
    foreach(FoodData foodData in foodDataArr){
      allFoods.Add(foodData);
      Debug.Log(foodData.FoodName);
    }
  }

  
  public void init(){
    if(!isInit){
      orderList = new List<Order>();
      getAllFoodList();
      complete = 0;
      createTodaysOrder(1, 0);
      Debug.Log("초기화 완료");
      isInit = true;
    }
  }
    
    public void DateUpdate()
    {
        DateUpdate(Managers.Date.day, Managers.Fame.fame);
    }
  
    public void DateUpdate(int day,int fame){
    orderList.Clear();
    todayFoods.Clear();
    complete = 0;
    createTodaysOrder(Managers.Date.day, Managers.Fame.fame);
  }



///명성과 일수 계산하여 tier 몇까지 받을건지
  public void createTodaysOrder(int day, int fame)
  {
    int tierScopeMin = 1;
    int tierScopeMax = 1;

    //임의로 계산하기 
    //day로 기본 min max 계산
    if (day > 20){
      tierScopeMin = 3;
    }else if(day > 10){
      tierScopeMin = 2;
    }

    if(day>15){
      tierScopeMax = 4;
    }else if(day>10){
      tierScopeMax = 3;
    }else if(day>3){
      tierScopeMax = 2;
    }

    //fame 으로 추가 티어 계산
    if(fame >= 90){
      tierScopeMax += 2;
      if (tierScopeMax > 4) tierScopeMax = 4;
    }else if(fame >=70){
      tierScopeMax += 1;
      if (tierScopeMax > 4) tierScopeMax = 4;
    }else if(fame <=30){
      tierScopeMax -= 1;
      if (tierScopeMax < tierScopeMin) tierScopeMax = tierScopeMin;
    }
    

    foreach(FoodData foodData in allFoods){
      int tier = foodData.FoodTier;
      if(tierScopeMin <= tier && tier <= tierScopeMax){
        todayFoods.Add(foodData);
      }
    }
  }


    public void createOrder(){
      int rand = Random.Range(0, todayFoods.Count);
      Order order = new Order(todayFoods[rand], DEFALT_TIME);
      orderList.Add(order);
      Debug.Log(order.Food.FoodName);
  }

    public void createOrder(int time)
    {
      int rand = Random.Range(0, todayFoods.Count);
      Order order = new Order(todayFoods[rand], time);
      orderList.Add(order);
      Debug.Log(order.Food.FoodName);
    }


  
    public void createGourmandOrder(int time){
      createOrder((int)(DEFALT_TIME - time));
    }



    public void createGroupGuestOrder(int group_num)
    {
      for (int i=0;i<= group_num;i++){
        createOrder();
      }
    }


//요리 오브젝트 이름 같으면 요리 제대로 만든것.
  public bool checkOrder(Food food){
    if (orderList.Count == 0)
    {
      return false;
    }

    FoodData order_food = orderList[0].Food;
    FoodData user_food = food.foodData;
    if(order_food.FoodName == user_food.FoodName){
      return true;
    }else{
      return false;
    }
  }


//유저가 만든 요리 해당 요리가 있는지 검사
  public bool checkRecipe(UserFood userFood){
    List<IngredientData> user_ingredients = userFood.Ingredients;
    List<IngredientData> food_ingredients;

    for (int i = 0; i < allFoods.Count; i++)
    {
      food_ingredients = allFoods[i].Ingredients;
      if(isSameRecipe(user_ingredients, food_ingredients)){
        return true;
      }
    }
    return false;

  }


  public bool isSameRecipe(List<IngredientData> my_ingredients, List<IngredientData> target_ingredients){
    int my_size = my_ingredients.Count;
    int target_size = target_ingredients.Count;


    List<bool> check = new List<bool>();
    for (int i = 0; i < target_size; i++)
    {
      check.Add(false);
    }


    for (int i = 0; i < my_size; i++)
    {
      string ingredient = my_ingredients[i].IngredientName;

      for (int j = 0; j < target_size; j++)
      {
        if (check[j] == false && ingredient.Equals(target_ingredients[j].IngredientName))
        {
          check[j] = true;
          break;
        }

        //일치하는 재료가 없을때
        if (j == target_size - 1) return false;
      }
    }

    for (int i = 0; i < target_size; i++)
    {
      if (check[i] == false) return false;
    }

    return true;
  }

  public bool deleteOrder(){
    if(orderList.Count == 0){
      return false;
    }
    orderList[0].deleteRecipe();
    orderList.Remove(orderList[0]);
    displayCount--;
    return true;
  }


  public bool deleteOrder(int id)
  {
    for(int i=0;i<orderList.Count;i++){
      if(orderList[i].Id == id){
        orderList[i].deleteRecipe();
        orderList.Remove(orderList[i]);
        displayCount--;
        return true;
      }
    }
    return false;
  }

  public bool isOrders()
    {
        if (orderList.Count == 0)
        {
            return false;
        }
        return true; ;
    }


  public void Clear(){

  }

}
