using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    bool _moveToDest = false;
    bool ispicking = false;
    GameObject hand;
    
    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;

        hand = GameObject.FindGameObjectWithTag("hand");
    }

    void Update()
    {
        
    }

    public void Pickup(GameObject gameObject)
    {
        setEquip(gameObject, true);
        ispicking = true;
    }
    public void drop()
    {
        GameObject go = hand.GetComponentInChildren<Rigidbody>().gameObject;
        hand.transform.DetachChildren();

        setEquip(go, false);


        ispicking = false;
    }
    public void setEquip(GameObject gameObject, bool isEquip)
    {
        Collider[] gameObjectColliders = gameObject.GetComponents<Collider>();
        Rigidbody gameObjectRigidbody = gameObject.GetComponent<Rigidbody>();

        foreach(Collider itemcollider in gameObjectColliders)
        {
            itemcollider.enabled = !isEquip;
        }
        gameObjectRigidbody.isKinematic = isEquip;
    }
    void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.Space)&&ispicking==true)
        {
            drop();
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }

        _moveToDest = false;
    }
}
