using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // 게임 버전
    private string userId;

    public TMP_InputField userIdText;
    public TMP_InputField roomNameText;

    private Dictionary<string, GameObject> roomDict = new Dictionary<string, GameObject>();
    public GameObject roomPrefab;
    public Transform scrollContent;

    private void Awake()
    {
        //씬 자동싱크 설정
        PhotonNetwork.AutomaticallySyncScene = true;

        // 게임 버전 설정
        PhotonNetwork.GameVersion = gameVersion;

        //포톤서버 접속
        PhotonNetwork.ConnectUsingSettings();
    }


    void Start()
    {
        Debug.Log("포톤 매니저 시작");
        userId = PlayerPrefs.GetString("User_ID", $"User_{Random.Range(0, 100):00}");
        userIdText.text = userId;
        PhotonNetwork.NickName = userId;
        Screen.SetResolution(1920, 1080, false);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("포톤 서버 접속");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("로비 접속");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("방 생성");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("방 접속");

        /*if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Level_1");
        }*/

        GameObject.Find("View").transform.Find("LobbyView").gameObject.SetActive(false);
        GameObject.Find("View").transform.Find("RoomView").gameObject.SetActive(true);


    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject tempRoom = null;
        foreach(var room in roomList)
        {
            if (room.RemovedFromList == true)
            {
                roomDict.TryGetValue(room.Name, out tempRoom);
                Destroy(tempRoom);
                roomDict.Remove(room.Name);
            }
            else
            {
                if (roomDict.ContainsKey(room.Name) == false)
                {
                    GameObject _room = Instantiate(roomPrefab, scrollContent);
                    _room.GetComponent<RoomData>().RoomInfo = room;
                    roomDict.Add(room.Name, _room);
                }
                else
                {
                    roomDict.TryGetValue(room.Name, out tempRoom);
                    tempRoom.GetComponent<RoomData>().RoomInfo = room;
                }
            }
        }
    }

    public void OnMakeRoomClick()
    {
        RoomOptions ro = new RoomOptions();  //방 옵션 설정
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 4;

      

        PhotonNetwork.CreateRoom(roomNameText.text, ro);
    }

}
