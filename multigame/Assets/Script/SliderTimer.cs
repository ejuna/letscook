using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderTimer : MonoBehaviour
{
    Slider timer;
    private float defaltTime=2.0f;
    OrderManager Orders;

    // Start is called before the first frame update
    void Start()
    {
    Orders = Managers.Orders;
    timer = GetComponent<Slider>();
    timer.maxValue = defaltTime;
    }

    // Update is called once per frame
    void Update()
    {
    if (timer.value > 0.0f){
      timer.value -= Time.deltaTime;
    }else{
      Orders.deleteOrder();
      Destroy(transform.parent.gameObject);
    }
    }
}
