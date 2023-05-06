using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button exitButton;


    public TextMeshProUGUI user1_nickName;
    public GameObject user1_cha;


    public override void OnJoinedRoom()
    {
        user1_cha.SetActive(true);
        user1_nickName.text = PhotonNetwork.NickName;
    }

    public void exitButtonClick()
    {
        SceneManager.LoadScene("Lobby");
    }

}

