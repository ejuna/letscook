using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    public GameObject createRoomMenu; //방 세부 설정 메뉴
    

    public GameObject HomeView;  //홈 화면
    public GameObject LobbyView; //로비 화면
    public GameObject RoomView;  //룸 화면



    public void HomeViewOn()
    {
        HomeView.SetActive(true);
    }

    public void HomeViewOff()
    {
        HomeView.SetActive(false);
    }

    public void LobbyViewOn()
    {
        LobbyView.SetActive(true);
    }

    public void LobbyViewOff()
    {
        LobbyView.SetActive(false);
    }

    public void RoomViewOn()
    {
        RoomView.SetActive(true);
    }

    public void RoomViewOff()
    {
        RoomView.SetActive(false);
    }







    public void createButtonClick()
    {
        createRoomMenu.SetActive(true);  //방만들기 메뉴 설정 활성화
    }

    

   

    
}
