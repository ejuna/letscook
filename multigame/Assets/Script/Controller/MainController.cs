  using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//============이준하 수정=============//
using Photon.Pun;
using Photon.Realtime;
//====================================//

public class MainController : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    public Transform character;
    public Transform GameObject;

    private GameObject interactingObject;
    private Rigidbody interactingRigidbody;
    private bool isPicking;

    Vector3 moveVec;

    Animator animator;


    //============이준하 수정=============//
    private PhotonView pv;
    //====================================//


    void Awake()
    {
        animator = GetComponent<Animator>();

        //============이준하 수정=============//
        pv = GetComponent<PhotonView>();
        //====================================//


    }
    void Start()
    {
        moveVec = Vector3.zero;
    }
    void Update()
    {
        //============이준하 수정=============//
        if (pv.IsMine)
        //====================================//
        {
            // 이동
            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");

            moveVec = new Vector3(hAxis, 0, vAxis).normalized;

            transform.position += moveVec * speed * Time.deltaTime;

            animator.SetBool("isWalking", moveVec != Vector3.zero);

            transform.LookAt(transform.position + moveVec);

            // item 들고 내리기
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isPicking)
                {
                    interactingObject = FindInteractableObject();
                    if (interactingObject != null && interactingObject.CompareTag("Pickup"))
                    {
                        interactingRigidbody = interactingObject.GetComponent<Rigidbody>();
                        interactingRigidbody.isKinematic = true;

                        interactingObject.transform.SetParent(GameObject);
                        interactingObject.transform.localPosition = Vector3.zero;
                        isPicking = true;
                        animator.SetBool("isPicking", true);
                    }
                }
                else if (isPicking)
                {
                    interactingRigidbody.isKinematic = false;

                    interactingObject.transform.SetParent(null);
                    interactingObject = null;
                    isPicking = false;
                    animator.SetBool("isPicking", false);
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                interactingObject = FindInteractableObject2();
                if (interactingObject != null && interactingObject.CompareTag("Container"))
                {
                    IngredientContainer ic = interactingObject.GetComponent<IngredientContainer>();
                    ic.Enter();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Exit();
            }
        }
        
        
    }

    // item 인식(캐릭터 앞)
    private GameObject FindInteractableObject()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = character.position + new Vector3(0f, 1.1f, 0f);
        if (Physics.Raycast(raycastOrigin, character.forward, out hit, 1.2f))
        {
            GameObject objectHit = hit.collider.gameObject;
            if (objectHit.CompareTag("Pickup"))
            {
                return objectHit;
            }
        }
        return null;
    }
    private GameObject FindInteractableObject2()
    {
        RaycastHit hit;
        if (Physics.Raycast(character.position, character.forward, out hit, 1.2f))
        {
            GameObject objectHit2 = hit.collider.gameObject;
            if (objectHit2.CompareTag("Container"))
            {
                return objectHit2;
            }
        }
        return null;
    }
    void Exit()
    {
        if (interactingObject != null)
        {
            IngredientContainer ic = interactingObject.GetComponent<IngredientContainer>();
            if (ic != null)
            {
                ic.Exit();
            }
        }
    }
}