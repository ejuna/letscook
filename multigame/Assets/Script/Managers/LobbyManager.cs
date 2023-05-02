using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    public TextMeshProUGUI nickName; //닉네임 표시
    public Button createButton; //방생성 버튼
    public Button joinButton; //방참가 버튼

    void Start()
    {
        nickName.text = PhotonNetwork.LocalPlayer.NickName;
    }
    
    public void createButtonClick()
    {

    }

    public void joinButtonClick()
    {

    }
}
