using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CountertopController : MonoBehaviourPun
{
    public List<string> ingres;
    bool isPlayerEnter;
    public PhotonView PV;

    AudioSource audioSoure;

    // Start is called before the first frame update
    void Start()
    {
        
        ingres = new List<string>();
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        audioSoure = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && isPlayerEnter)
        {
            makeFood();
            PV.RPC(nameof(clearIngres), RpcTarget.Others);
        }

    }
    [PunRPC]
    void makeFood()
    {
        if (ingres.Count == 0)
        {
            return;
        }

        List<FoodData> tempAllFood = Managers.Orders.allFoods.ToList();
        List<FoodData> produceFood = new List<FoodData>();
        //해당 재료로 만들수 있는 요리찾기
        //들어온 재료와 음식중 해당 재료를 가진 음식 찾기
        //들어온 재료 반복문
        int loopNum = 0;
        ingres.ForEach(food =>
        {
            produceFood.Clear();
            //기존요리들 전부 검색
            for (int i = 0; i < tempAllFood.Count; i++)
            {
                bool isSame = false;//무한루프 방지
                                    //들어온 재료 보다 재료많은 요리 거르기
                if (tempAllFood[i].Ingredients.Count <= ingres.Count)
                {
                    //기존음식의 재료중 같은 거 있는지 찾기
                    tempAllFood[i].Ingredients.ForEach(IngredientData =>
                    {
                        //Debug.Log(IngredientData.name);
                        if (IngredientData.IngredientName.Equals(food))
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

        ingres.Clear();
        ingres = new List<string>();
        //재료 삭제하기
        PV.RPC(nameof(DestroyChild), RpcTarget.All);

        //음식 생성
        GameObject go;
        Vector3 newPosition = transform.position + new Vector3(2f, 2f, 0f);

        if (tempAllFood.Count == 1)
        {
            Debug.Log("완성");
            go = PhotonNetwork.Instantiate("Prefabs/요리/" + tempAllFood[0].name, newPosition, transform.rotation);
            audioSoure.Play();
        }
        else//실패 음식
        {
            go = PhotonNetwork.Instantiate("Prefabs/요리/Clinker", newPosition, transform.rotation);
        }
    }

    [PunRPC]
    private void DestroyChild()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            Debug.Log(transform.GetChild(i).gameObject.name);
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    [PunRPC]
    void clearIngres()
    {
        ingres.Clear();
        ingres = new List<string>();
    }
    [PunRPC]
    void onTheboard(int viewId , int parentviewId)
    {
        GameObject other = PhotonView.Find(viewId).gameObject;
        GameObject parent = PhotonView.Find(parentviewId).gameObject;
        other.transform.SetParent(parent.transform, false);
        other.transform.localPosition = Vector3.zero;
        other.transform.rotation = new Quaternion(0, 1, 0, 0);
        setEquip(other.gameObject, true);
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
        if (other.gameObject.tag=="Player"&& other.gameObject.GetComponent<PhotonView>().IsMine)
        {
            isPlayerEnter = true;
        }
        if (other.gameObject.GetComponent<Ingredient>() != null && other.gameObject.tag == "Pickup" &&  (other.gameObject.transform.parent == null || other.gameObject.transform.parent.name != "GameObject"))
        {
            Debug.Log(other.gameObject.GetComponent<PhotonView>().ViewID);
            Debug.Log(transform.gameObject.GetComponent<PhotonView>().ViewID);
            onTheboard(other.gameObject.GetComponent<PhotonView>().ViewID, transform.gameObject.GetComponent<PhotonView>().ViewID);
            ingres.Add(other.gameObject.GetComponent<Ingredient>().ingredientName);
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
