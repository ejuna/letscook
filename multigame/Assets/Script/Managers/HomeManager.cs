using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class HomeManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // 게임 버전

    public TextMeshProUGUI connectionInfoText; //네트워크 정보 표시 텍스트
    public Button enterButton; //서버 접속 버튼
    public TMP_InputField nickName; //유저닉네임

    public void ValueChanged() //인풋필드 입력변화 감지
    {
        enterButton.interactable = true;  //닉네임 입력시 접속버튼 활성화
    }


    public void EnterButtonClick()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
   

        enterButton.interactable = false;
        connectionInfoText.text = "마스터 서버에 접속중...";
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.NickName = nickName.text;
        connectionInfoText.text = "온라인: 마스터 서버와 연결됨";
        PhotonNetwork.JoinLobby();
        SceneManager.LoadScene("Lobby");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        enterButton.interactable = true;
        connectionInfoText.text = "오프라인: 마스터 서버와 연결되지 않음";
    }

}
