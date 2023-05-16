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
    private string userId; //유저 닉네임

    public TMP_InputField userIdText; //유저 입력 닉네임
    public TMP_InputField roomNameText; //방 이름

    
    private Dictionary<string, GameObject> roomDict = new Dictionary<string, GameObject>(); //방 목록 
    public GameObject roomPrefab; //방 프리팹
    public Transform scrollContent; //방 표시 콘텐츠

    public GameObject[] roomPlayer;

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

    //포톤 마스터 서버에 접속
    public override void OnConnectedToMaster()
    {
        Debug.Log("포톤 서버 접속");

        PhotonNetwork.JoinLobby();
    }

    //로비에 접속
    public override void OnJoinedLobby()
    {
        Debug.Log("로비 접속");

        //룸화면 비활성화
        GameObject.Find("View").transform.Find("RoomView").gameObject.SetActive(false);
        //로비화면 할성화
        GameObject.Find("View").transform.Find("LobbyView").gameObject.SetActive(true);
    }

    //방 생성
    public override void OnCreatedRoom()
    {
        Debug.Log("방 생성");
    }

    //방 접속
    public override void OnJoinedRoom()
    {
        Debug.Log("방 접속");

        /*if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Level_1");
        }*/

        //로비화면 비할성화
        GameObject.Find("View").transform.Find("LobbyView").gameObject.SetActive(false);
        //룸화면 활성화
        GameObject.Find("View").transform.Find("RoomView").gameObject.SetActive(true);

        //룸 플레이어 업데이트
        PlayerUpdate();

    }

    //방 퇴장
    public override void OnLeftRoom()
    {
        Debug.Log("방 퇴장");
    }

    //새로운 플레이어 방에 접속
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log($"플레이어 {newPlayer.NickName} 가 방에 참가.");

        //룸 플레이어 업데이트
        PlayerUpdate();
    }

    //플레이어 방에서 퇴장
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log($"플레이어 {otherPlayer.NickName} 가 방에서 퇴장.");

        //룸 플레이어 업데이트
        PlayerUpdate();
    }


    //방 목록 업데이트
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject tempRoom = null;
        foreach(var room in roomList)
        {
            //방이 제거됐을 경우
            if (room.RemovedFromList == true)
            {
                roomDict.TryGetValue(room.Name, out tempRoom);
                Destroy(tempRoom);
                roomDict.Remove(room.Name);
            }
            else
            {
                //방이 생성됐을 경우
                if (roomDict.ContainsKey(room.Name) == false)
                {
                    GameObject _room = Instantiate(roomPrefab, scrollContent);
                    _room.GetComponent<RoomData>().RoomInfo = room;
                    roomDict.Add(room.Name, _room);
                }
                //방이 변경됐을 경우
                else
                {
                    roomDict.TryGetValue(room.Name, out tempRoom);
                    tempRoom.GetComponent<RoomData>().RoomInfo = room;
                }
            }
        }
    }

    //방 옵션 설정후 방생성 버튼 클릭
    public void OnMakeRoomClick()
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 4;

        PhotonNetwork.CreateRoom(roomNameText.text, ro);
    }

    //방 퇴장 버튼 클릭
    public void ExitClick()
    {
        PhotonNetwork.LeaveRoom();
    }


    //플레이어 리스트 업데이트
    public void PlayerUpdate()
    {
        //초기화 
        for(int i=0; i<4; i++)
        {
            roomPlayer[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = " ";
            roomPlayer[i].transform.GetChild(2).gameObject.SetActive(false);
        }

        //설정
        for(int i=0; i<PhotonNetwork.PlayerList.Length; i++)
        {
            //유저 닉네임 표시
            roomPlayer[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[i].NickName;
            //유저 이미지 표시
            roomPlayer[i].transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
