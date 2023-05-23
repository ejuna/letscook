using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessController : MonoBehaviour
{
    GameObject player;
    GameObject hand;
    PlayerController playerController;
    //썰기,굽기,다지기
    //음료수 혼합 , 조합하기
    bool isPlayerEnter;
    public float thorwForce;
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
        if (Input.GetKey(KeyCode.V) && isPlayerEnter&& playerController.isPicking)
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

    public virtual void prepIngredients(GameObject go){}
    public void throwObject(GameObject go)
    {
        Debug.Log("던지기");
        //go의 Rigidbody를 가져온다.
        Rigidbody rigidbody = go.GetComponent<Rigidbody>();

        //던질 각도,및 힘 선언 및 조절
        Vector3 thorwAngle = Vector3.zero;
        thorwAngle.y = 5f;
        thorwForce = 1f;
        //
        rigidbody.AddForce(thorwAngle * thorwForce, ForceMode.Impulse);
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
