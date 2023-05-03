using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float _speed = 1.0f;

    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
    }
    void Update()
    {

    }
    void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }

    }

}
