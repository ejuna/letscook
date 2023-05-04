using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    GameObject player;
    GameObject hand;
    bool isPlayerEnter;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hand = GameObject.FindGameObjectWithTag("hand");
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnKeyboard()
    {
        //오브젝트 상호작용 할 키를 정함
        if (Input.GetKey(KeyCode.Space)&&isPlayerEnter)
        {
            transform.SetParent(hand.transform);
            transform.localPosition = Vector3.zero;
            transform.rotation = new Quaternion(0, 0, 0, 0);

            isPlayerEnter = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (player==other.gameObject)
        {
            isPlayerEnter = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (player.Equals(other))
        {
            isPlayerEnter = false;
        }
    }
}
