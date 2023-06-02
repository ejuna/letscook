using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CountertopController : MonoBehaviour
{
    List<GameObject> ingres;
    bool isPlayerEnter;
    // Start is called before the first frame update
    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnKeyboard()
    {
        //조합하기
        if (Input.GetKey(KeyCode.V) && isPlayerEnter)
        {
            List<FoodData> tempAllFood = Managers.Orders.allFoods.ToList();
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
            other.transform.SetParent(transform, false);
            other.transform.localPosition = Vector3.zero;
            other.transform.rotation = new Quaternion(0, 1, 0, 0);
            ingres.Add(other.gameObject);
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
