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
            //해당 재료로 만들수 있는 요리찾기
            //들어온 재료와 음식중 해당 재료를 가진 음식 찾기
            //들어온 재료 반복문
            ingres.ForEach(food =>
            {
                produceFood.Clear();
                //기존요리들 전부 검색
                for (int i = 0; i< tempAllFood.Count;i++)
                {
                    bool isSame=false;//무한루프 방지
                    //들어온 재료 보다 재료많은 요리 거르기
                    if(tempAllFood[i].Ingredients.Count<= ingres.Count)
                    {
                        //기존음식의 재료중 같은 거 있는지 찾기
                        tempAllFood[i].Ingredients.ForEach(IngredientData =>
                        {
                            //Debug.Log(IngredientData.name);
                            if (IngredientData.name.Equals(food))
                            {
                                isSame = true;
                            }
                            if (loopNum++ > 10000) throw new Exception("Infinite Loop"); //무한루프방지
                        });
                    }
                    if (isSame) produceFood.Add(tempAllFood[i]);//같은거를 모은 리스트에 추가
                    
                }
                tempAllFood.Clear();
                tempAllFood = produceFood.ToList();
            });
            //재료 삭제하기
            for(int i = 1 ; i< transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            //음식 생성
            if (tempAllFood.Count==1) {
                Managers.Resource.Instantiate("요리/" + tempAllFood[0].name,transform);
            }
            else//실패 음식
            {
                Managers.Resource.Instantiate("요리/Clinker", transform);
            }
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
