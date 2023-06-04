using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//============이준하 수정=============//
using Photon.Pun;
using Photon.Realtime;
//====================================//

public class MainController : MonoBehaviourPunCallbacks
{
    public float speed;
    public Transform Player;
    public Transform GameObject;

    private GameObject interactingObject;
    private GameObject interactingContainer;
    private Rigidbody interactingRigidbody;

    float hAxis;
    float vAxis;
    bool isPicking;
    bool isFreeze;
    Vector3 moveVec;
    Animator animator;


    
    private PhotonView pv;
   


    void Awake()
    {
        animator = GetComponent<Animator>();

        
        pv = GetComponent<PhotonView>();

        //1인칭 카메라 비활성화
        transform.Find("SubCamera").GetComponent<Camera>().enabled = false;
        


    }
    void Start()
    {
        moveVec = Vector3.zero;
        isFreeze = false;
    }
    void Update()
    {
        
        if (pv.IsMine)
        
        {
            if (!isFreeze)
            {
                // 이동
                hAxis = Input.GetAxisRaw("Horizontal");
                vAxis = Input.GetAxisRaw("Vertical");


                moveVec = new Vector3(hAxis, 0, vAxis).normalized;

                transform.position += moveVec * speed * Time.deltaTime;

                animator.SetBool("isWalking", moveVec != Vector3.zero);

                transform.LookAt(transform.position + moveVec);
            }
            // item 들고 내리기
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (!isPicking)
                {
                    interactingObject = findInteractableObject();
                    if (interactingObject != null && interactingObject.CompareTag("Pickup"))
                    {
                        interactingObject.GetComponent<PhotonView>().RequestOwnership();
                        interactingRigidbody = interactingObject.GetComponent<Rigidbody>();
                        interactingRigidbody.isKinematic = true;

                        interactingObject.transform.SetParent(GameObject);
                        Collider ioc = interactingObject.GetComponent<Collider>();
                        ioc.isTrigger = true;
                        interactingObject.transform.localPosition = Vector3.zero;

                        PhotonView objPv = interactingObject.GetComponent<PhotonView>();
                        objPv.RPC("UpdateInteractingObjectPosition", RpcTarget.Others, interactingObject.transform.localPosition,objPv.ViewID) ;
                        Debug.Log("부모 설정 실행시켜");

                        isPicking = true; // 들고 있는지 아닌지 체크
                        animator.SetBool("isPicking", true); // 애니메이션에서 위의 isPicking과 다름
                    }
                }
                else if (isPicking)
                {
                    drop();
                }
            }
            // 재료 컨테이너 UI 생성, 삭제
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                interactingContainer = findInteractableContainer();
                if (interactingContainer != null && interactingContainer.CompareTag("Container"))
                {
                    IngredientContainer ic = interactingContainer.GetComponent<IngredientContainer>();
                    ic.enter();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                exit();
            }
            // 1인칭 카메라 설정
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if(transform.Find("SubCamera").GetComponent<Camera>().enabled == true)
                {
                    transform.Find("SubCamera").GetComponent<Camera>().enabled = false;
                }
                else
                {
                    transform.Find("SubCamera").GetComponent<Camera>().enabled = true;
                }
            }
        }
    }

    // item 인식(캐릭터 앞)
    private GameObject findInteractableObject()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = Player.position + new Vector3(0f, 1.1f, 0f);
        if (Physics.Raycast(raycastOrigin, Player.forward, out hit, 1.2f))
        {
            GameObject objectHit = hit.collider.gameObject;
            if (objectHit.CompareTag("Pickup"))
            {
                return objectHit;
            }
        }
        return null;
    }
    private GameObject findInteractableContainer()
    {
        RaycastHit hit;
        if (Physics.Raycast(Player.position, Player.forward, out hit, 1.2f))
        {
            GameObject objectHit2 = hit.collider.gameObject;
            if (objectHit2.CompareTag("Container"))
            {
                return objectHit2;
            }
        }
        return null;
    }
    void exit() // 재료 컨테이너 화면 끄기
    {
        if (interactingContainer != null)
        {
            IngredientContainer ic = interactingContainer.GetComponent<IngredientContainer>();
            if (ic != null)
            {
                ic.exit();
            }
        }
    }
    public void drop()
    {
        interactingRigidbody.isKinematic = false;

        Collider ioc = interactingObject.GetComponent<Collider>();
        ioc.isTrigger = false;
        interactingObject.transform.SetParent(null);
        interactingObject = null;
        isPicking = false;
        animator.SetBool("isPicking", false);
    }

    public void freeze()
    {
        isFreeze = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isPicking", false);
    }
    public void unfreeze()
    {
        isFreeze = false;
    }

    public GameObject getInteractingObject()
    {
        return interactingObject;
    }
    public bool getIsPicking()
    {
        return isPicking;
    }

    
}