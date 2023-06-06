using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class WasteCanController : MonoBehaviour
{
    AudioSource audioSoure;
    public bool isPlayerEnter;
    public PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        audioSoure = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    private void Destroy(int viewId)
    {
            Debug.Log(PhotonView.Find(viewId).gameObject.name);
            Destroy(PhotonView.Find(viewId).gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerEnter = true;
        }
        if (other.gameObject.tag == "Pickup" && (other.gameObject.transform.parent == null || other.gameObject.transform.parent.name != "GameObject"))
        {
            PV.RPC(nameof(Destroy), RpcTarget.All, other.gameObject.GetComponent<PhotonView>().ViewID);
            audioSoure.Play();
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
