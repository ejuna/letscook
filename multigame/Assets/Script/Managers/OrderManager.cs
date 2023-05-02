using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager
{
  private Queue<Order> orderList;
  private List<Food> todaysOrder;
  private int complete;
  private int defaltTime;
  private const int MAX_GROUP = 4;

  public string fileName = "load_order"; // 불러올 파일의 이름


  public OrderManager(int day,int fame){
    orderList = new Queue<Order>();
    todaysOrder = createTodaysOrder(day, fame);
    defaltTime = getDefaltTime(day, fame);
    complete = 0;
  }

///txt 파일 불러오고 리스트에 저장.
  public List<Food> createTodaysOrder(int day, int fame)
  {
    List<Food> list = new List<Food>();
    TextAsset textAsset = Resources.Load<TextAsset>(fileName);
    string fileContent = textAsset.text;
    string[] line = fileContent.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);

    for(int i=0;i<line.Length;i++){
      string name = line[i].Split('/')[0];
      int tier = int.Parse(line[i].Split('/')[1]);
      string ingredient = line[i].Split('/')[2];
      int price = int.Parse(line[i].Split('/')[3]);

      
    }
    return list;
  }


  public void createOrder(){
    int rand = Random.Range(0, todaysOrder.Count);
    Order order = new Order(todaysOrder[rand], defaltTime);
    orderList.Enqueue(order);
  }

  public void createOrder(int time)
  {
    int rand = Random.Range(0, todaysOrder.Count);
    Order order = new Order(todaysOrder[rand], time);
    orderList.Enqueue(order);
  }


  public void createGourmandOrder(){
    createOrder((int)(defaltTime - defaltTime * 0.3));
  }

  public void createGroupGuestOrder(){
    int group_num = Random.Range(2, MAX_GROUP);
    for (int i=0;i<= group_num;i++){
      createOrder();
    }
  }

  public bool checkOrder(Food food){
    if (orderList.Count == 0)
    {
      return false;
    }

    Food order_food = orderList.Dequeue().getFood();
    List<Ingredient> ingredients = food.getIngredients();
    List<Ingredient> order_ingredients = order_food.getIngredients();
    int ingredients_size = ingredients.Count;
    int order_ingredients_size = order_ingredients.Count;


    List<bool> check = new List<bool>();
    for(int i=0;i<order_ingredients.Count;i++){
      check.Add(false);
    }


    for(int i=0;i< ingredients_size; i++){
      string ingredient = ingredients[i].name;
      for(int j=0;j< order_ingredients_size; j++){
        if(check[j] == false && ingredient.Equals(order_ingredients[j].name)){
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


  public bool deleteOrder(){
    if(orderList.Count == 0){
      return false;
    }
    orderList.Dequeue();
    return true;
  }


///팀원들과 함께 상의하여 day와 fame에 따라 기본 제한시간을 정하기
  private int getDefaltTime(int day,int fame){
    return 0;
  }
}
