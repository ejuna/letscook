using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager
{
  private Queue<Order> orderList;
  public List<FoodData> allFoods=new List<FoodData>();//게임 프리팹 넣기?
  public List<FoodData> todayFoods=new List<FoodData>();
  public int complete { get; set; }
  private const int DEFALT_TIME = 20;
  private static float timer = 0f;
  static bool isInit = false;


  public OrderManager(){

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
      orderList = new Queue<Order>();
      getAllFoodList();
      complete = 0;
      createTodaysOrder(1, 0);
      Debug.Log("초기화 완료");
      isInit = true;
    }
  }

  public void DateUpdate(int day,int fame){
    orderList.Clear();
    todayFoods.Clear();
    complete = 0;
    createTodaysOrder(day, fame);
  }

  public void OnUpdate(){
    timer += Time.deltaTime;
    if (timer >= 1f)
    {
      createOrder(50);
      timer = 0f;
    }
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
      orderList.Enqueue(order);
    Debug.Log(order.Food.FoodName);
  }

    public void createOrder(int time)
    {
      int rand = Random.Range(0, todayFoods.Count);
      Order order = new Order(todayFoods[rand], time);
      orderList.Enqueue(order);
    Debug.Log(order.Food.FoodName);
  }


    public void createGourmandOrder(){
      createOrder((int)(DEFALT_TIME - DEFALT_TIME * 0.3));
    }



    public void createGroupGuestOrder(int group_num)
    {
      for (int i=0;i<= group_num;i++){
        createOrder();
      }
    }


  public bool checkOrder(UserFood userFood){
    if (orderList.Count == 0)
    {
      return false;
    }

    FoodData order_food = orderList.Dequeue().Food;
    List<IngredientData> user_ingredients = userFood.Ingredients;
    List<IngredientData> order_ingredients = order_food.Ingredients;
    int ingredients_size = user_ingredients.Count;
    int order_ingredients_size = order_ingredients.Count;


    List<bool> check = new List<bool>();
    for(int i=0;i<order_ingredients.Count;i++){
      check.Add(false);
    }


    for(int i=0;i< ingredients_size; i++){
      string ingredient = user_ingredients[i].IngredientName;
      for(int j=0;j< order_ingredients_size; j++){
        if(check[j] == false && ingredient.Equals(order_ingredients[j].IngredientName)){
          check[j] = true;
          break;
        }

        //일치하는 재료가 없을때
        if (j == order_ingredients_size - 1) return false;
      }
    }

    for(int i = 0; i < order_ingredients_size; i++){
      if (check[i] == false) return false;
    }

    return true;
  }


//레시피 체크하려는 이유가 뭔지 물어보기
  public bool checkRecipe(UserFood userFood){
    return true;
  }


  public bool deleteOrder(){
    if(orderList.Count == 0){
      return false;
    }
    orderList.Dequeue();
    return true;
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
