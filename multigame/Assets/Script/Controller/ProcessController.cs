using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessController : MonoBehaviour
{
    GameObject player;
    GameObject hand;
    PlayerController playerController;

    bool isPlayerEnter;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hand = GameObject.FindGameObjectWithTag("hand");
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;

        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnKeyboard()
    {
        //오브젝트 상호작용 할 키를 정함
        if (Input.GetKey(KeyCode.C) && isPlayerEnter&& playerController.isPicking)
        {
            GameObject go = hand.GetComponentInChildren<Rigidbody>().gameObject;
            ObjectController objectController = go.GetComponent<ObjectController>();
            // 플레이어 손에 있는 물건을 연결은 끈고(dorp())
            playerController.drop();
            //그 물건을 이 물건 위에 올린다.
            objectController.setParent(transform);
            //그 물건의 상태및 종류에 따라 가공하는 함수를 실행 한다.
            prepIngredients(go);

        }
    }

    void prepIngredients(GameObject go)
    {
        Ingredient ig = go.GetComponent<Ingredient>();
        Destroy(ig);//오브젝트 제거
        //오브젝트를 넘겨 받고 오브젝트의 상태/종류에 따라서 손질한다.
        if (ig.Type == Define.IngredientType.Meat)
        {
            //새로 조리된 오브젝트를 생성후 주변에 던진다.
            //문제1 해당 오브젝트가 어떤 조리대인가를 확인 해야함;
            //문제2 지금 조리과정이 생략됨
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (player == other.gameObject)
        {
            isPlayerEnter = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (player == other.gameObject)
        {
            isPlayerEnter = false;
        }
    }
}
