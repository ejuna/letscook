using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CountertopController : MonoBehaviour
{
    List<string> ingres;
    bool isPlayerEnter;
    // Start is called before the first frame update
    void Start()
    {
        ingres = new List<string>();
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnKeyboard()
    {
        int loopNum = 0;
        if (Input.GetKeyDown(KeyCode.V) && isPlayerEnter)
        {

            List<FoodData> tempAllFood = Managers.Orders.allFoods.ToList();
            List<FoodData> produceFood = new List<FoodData>();

            ingres.ForEach(food =>
            {
                produceFood.Clear();
                for (int i = 0; i< tempAllFood.Count;i++)
                {
                    bool isSame=false;
                    if(tempAllFood[i].Ingredients.Count<= ingres.Count)
                    {
                        tempAllFood[i].Ingredients.ForEach(IngredientData =>
                        {
                            //Debug.Log(IngredientData.name);
                            if (IngredientData.name.Equals(food))
                            {
                                isSame = true;
                            }
                            if (loopNum++ > 10000)
                                throw new Exception("Infinite Loop");
                        });
                    }
                    if (isSame) produceFood.Add(tempAllFood[i]);
                    
                }
                tempAllFood.Clear();
                tempAllFood = produceFood.ToList();
            });

            for(int i = 1 ; i< transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            if (tempAllFood.Count==1) {
                Managers.Resource.Instantiate("요리/" + tempAllFood[0].name);
            }
            else
            {
                Managers.Resource.Instantiate("요리/Clinker");
            }
            //Managers.Orders 체크래시피 참이면 실행 
            //자식을 불러서 usetfood를 생성
            //userfood라는 클래스가 생성이 돼고 그 클래스들을 넘겨준다.

            //있으면 위에 있는 물건들을 전부 제거하고 조리법에 따라 물건을 던진다.
        }
    }

    public void setEquip(GameObject gameObject, bool isEquip)
    {
        Collider[] gameObjectColliders = gameObject.GetComponents<Collider>();
        Rigidbody gameObjectRigidbody = gameObject.GetComponent<Rigidbody>();

        foreach (Collider itemcollider in gameObjectColliders)
        {
            itemcollider.enabled = !isEquip;
        }
        gameObjectRigidbody.isKinematic = isEquip;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            isPlayerEnter = true;
        }
        if (other.gameObject.tag == "Pickup")
        {
            ingres.Add(other.gameObject.name);

            other.transform.SetParent(transform, false);
            other.transform.localPosition = Vector3.zero;
            other.transform.rotation = new Quaternion(0, 1, 0, 0);

            setEquip(other.gameObject, true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerEnter = false;
        }
    }
}
