using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class PhotonManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // 게임 버전
    private string userId; //유저 닉네임

    public Button RoadGame;  //로비접속버튼

    public TMP_InputField userIdText; //유저 입력 닉네임
    public TMP_InputField roomNameText; //방 이름
    public TMP_InputField roomPassword; //방 비밀번호


    private Dictionary<string, GameObject> roomDict = new Dictionary<string, GameObject>(); //방 목록 
    public GameObject roomPrefab; //방 프리팹
    public Transform scrollContent; //방 표시 콘텐츠

    public GameObject[] roomPlayer; //방 접속 플레이어 리스트

    public int targetMoney;
    public int targetDay;

    public bool inLobbyRoom;

    private void Awake()
    {
        //씬이 바뀌어도 포톤매니저 오브젝트 삭제 방지
        DontDestroyOnLoad(transform.gameObject);

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

    //로드게임버튼 클릭
    public void RoadGameButtonClick()
    {
        GameObject.Find("Title").gameObject.SetActive(false);
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

        //로비화면 비할성화
        GameObject.Find("View").transform.Find("LobbyView").gameObject.SetActive(false);
        //룸화면 활성화
        GameObject.Find("View").transform.Find("RoomView").gameObject.SetActive(true);

        //로비룸 입장 true 설정
        inLobbyRoom = true;

        //룸 플레이어 업데이트
        PlayerUpdate();

        //현재 룸의 목표 금액,일수 설정으로 연동
        if (PhotonNetwork.InRoom)
        {
            Hashtable cp = PhotonNetwork.CurrentRoom.CustomProperties;

            targetMoney = int.Parse(cp["m"].ToString());
            targetDay = int.Parse(cp["d"].ToString());
        }

    }

    //방 접속 실패
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("방 접속실패");

        //returnCode 32758 = 비밀번호 틀림
        if (returnCode == 32758)
        {
            //다른 터치 방지벽 활성화
            GameObject.Find("Panel-BackGround").transform.Find("Blocker").gameObject.SetActive(true);

            //비밀번호 오류창 활성화
            GameObject.Find("Panel-BackGround").transform.Find("Panel-PasswordError").gameObject.SetActive(true);
        }
        GameObject.Find("Panel-BackGround").transform.Find("Panel-Password").gameObject.SetActive(false);
    }

    //방 퇴장
    public override void OnLeftRoom()
    {
        inLobbyRoom = false;
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

    //create 버튼 클릭
    public void CreateButtonClick()
    { 
        //다른 터치 방지벽 활성화
        GameObject.Find("Panel-BackGround").transform.Find("Blocker").gameObject.SetActive(true);

        //방만들기 메뉴 설정 활성화
        GameObject.Find("Panel-BackGround").transform.Find("Panel-CreateRoom").gameObject.SetActive(true);

        //방 옵션 입력창 초기화
        GameObject.Find("InputField-roomName").gameObject.GetComponent<TMP_InputField>().text = "";
        GameObject.Find("InputField-password").gameObject.GetComponent<TMP_InputField>().text = "";
    }

    //목표 금액 설정
    public void TargetMoneySetting()
    {
        int temp = GameObject.Find("Dropdown-targetMoney").gameObject.GetComponent<TMP_Dropdown>().value;

        switch (temp)
        {
            case 0:
                targetMoney = 1000000;
                break;
            case 1:
                targetMoney = 5000000;
                break;
            case 2:
                targetMoney = 10000000;
                break;
            case 3:
                targetMoney = -1;
                break;
        }
        
    }

    //목표 일수 설정
    public void TargetDaySetting()
    {
        int temp = GameObject.Find("Dropdown-targetDay").gameObject.GetComponent<TMP_Dropdown>().value;

        switch (temp)
        {
            case 0:
                targetDay = 30;
                break;
            case 1:
                targetDay = 45;
                break;
            case 2:
                targetDay = 60;
                break;
            case 3:
                targetDay = -1;
                break;
        }
    }


    //방 옵션 설정후 방만들기 버튼 클릭
    public void OnMakeRoomClick()
    { 
        //방이름과 비밀번호 입력 했는지 체크
        if (roomNameText.text != "" && roomPassword.text != "")
        {
            //목표 금액,일수 설정
            TargetMoneySetting();
            TargetDaySetting();


            //룸 옵션 설정
            string roomNamePassword = roomNameText.text + "_" + roomPassword.text;

            RoomOptions ro = new RoomOptions();
            ro.IsOpen = true;
            ro.IsVisible = true;
            ro.MaxPlayers = 4;
            ro.CustomRoomProperties = new Hashtable() { { "m", targetMoney }, { "d", targetDay } };


            PhotonNetwork.CreateRoom(roomNamePassword, ro);

            //방만들기 메뉴 설정 비활성화
            GameObject.Find("Panel-CreateRoom").SetActive(false);

            //다른 터치 방지벽 비활성화
            GameObject.Find("Panel-BackGround").transform.Find("Blocker").gameObject.SetActive(false);
        }
        else
        {
            //방만들기 메뉴 설정 비활성화
            GameObject.Find("Panel-CreateRoom").SetActive(false);

            //방만들기 오류창 활성화
            GameObject.Find("Panel-BackGround").transform.Find("Panel-CreateRoomError").gameObject.SetActive(true);
        }

        
    }

    //방 옵션 설정창 나가기
    public void CreateRoomExitClick()
    {
        //방 옵션 설정창 비활성화
        GameObject.Find("Panel-BackGround").transform.Find("Panel-CreateRoom").gameObject.SetActive(false);

        //다른 터치 방지벽 비활성화
        GameObject.Find("Panel-BackGround").transform.Find("Blocker").gameObject.SetActive(false);
    }

    //방만들기 오류창 나가기
    public void CreateRoomErrorExitClick()
    {
        //방만들기 오류창 비활성화
        GameObject.Find("Panel-BackGround").transform.Find("Panel-CreateRoomError").gameObject.SetActive(false);

        //다른 터치 방지벽 비활성화
        GameObject.Find("Panel-BackGround").transform.Find("Blocker").gameObject.SetActive(false);
    }

    //사운드 설정 버튼 클릭
    public void SoundSettingClick()
    {
        //다른 터치 방지벽 활성화
        GameObject.Find("Panel-BackGround").transform.Find("Blocker").gameObject.SetActive(true);

        //사운드 설정 창 활성화
        GameObject.Find("View").transform.Find("SoundView").gameObject.SetActive(true);

        
    }

    //사운드 설정 완료 버튼 클릭
    public void SoundSettingExit()
    {
        //사운드 설정창 비활성화
        GameObject.Find("View").transform.Find("SoundView").gameObject.SetActive(false);

        //다른 터치 방지벽 비활성화
        GameObject.Find("Panel-BackGround").transform.Find("Blocker").gameObject.SetActive(false);
    }


    //방 참가 버튼 클릭
    public void JoinButtonClick()
    {
        string tempName=GameObject.Find("JoinRoomName").gameObject.GetComponent<TextMeshProUGUI>().text;
        string tempPassword = GameObject.Find("InputField-Password").gameObject.GetComponent<TMP_InputField>().text;
        string tempRoom = tempName + "_" + tempPassword;
        PhotonNetwork.JoinRoom(tempRoom);

        //비밀번호 입력창 비활성화
        GameObject.Find("Panel-BackGround").transform.Find("Panel-Password").gameObject.SetActive(false);

        //다른 터치 방지벽 비활성화
        GameObject.Find("Panel-BackGround").transform.Find("Blocker").gameObject.SetActive(false);
    }

    //비밀번호 입력창 나가기
    public void PasswordExitClick()
    {
        //비밀번호 입력창 비활성화
        GameObject.Find("Panel-BackGround").transform.Find("Panel-Password").gameObject.SetActive(false);

        //다른 터치 방지벽 비활성화
        GameObject.Find("Panel-BackGround").transform.Find("Blocker").gameObject.SetActive(false);
    }

    //비밀번호 오류창 나가기
    public void PasswordErrorExitClick()
    {
        //비밀번호 오류창 비활성화
        GameObject.Find("Panel-BackGround").transform.Find("Panel-PasswordError").gameObject.SetActive(false);

        //다른 터치 방지벽 비활성화
        GameObject.Find("Panel-BackGround").transform.Find("Blocker").gameObject.SetActive(false);
    }

    //방 퇴장 버튼 클릭
    public void ExitClick()
    {
        PhotonNetwork.LeaveRoom();
    }

    //게임 시작 버튼 클릭
    public void GameStartClick()
    {

        //방장일 경우에만 게임 씬으로 이동
        if (PhotonNetwork.IsMasterClient)
        {
            
            PhotonNetwork.LoadLevel("Game");
            inLobbyRoom = false;

        }
    }


    //플레이어 리스트 업데이트
    public void PlayerUpdate()
    {
        if (inLobbyRoom)
        {
            //초기화 
            for (int i = 0; i < 4; i++)
            {
                roomPlayer[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = " ";
                roomPlayer[i].transform.GetChild(0).gameObject.SetActive(false);
            }

            //설정
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if (i == 0)
                {
                    //유저 닉네임 표시 (1번 플레이어는 서빙역할)
                    roomPlayer[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[i].NickName + " (서빙)";
                }
                else
                {
                    //유저 닉네임 표시
                    roomPlayer[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[i].NickName;
                }

                //유저 이미지 표시
                roomPlayer[i].transform.GetChild(0).gameObject.SetActive(true);
            }

            //방에 4명이면 게임시작 버튼 활성화
            if (PhotonNetwork.PlayerList.Length != 0)
            {
                GameObject.Find("GameStartButton").gameObject.GetComponent<Button>().interactable = true;
            }
            else
            {
                GameObject.Find("GameStartButton").gameObject.GetComponent<Button>().interactable = false;
            }
        }
        

    }


    //닉네임 변경
    public void NickNameChange()
    {
        PhotonNetwork.NickName = userIdText.text;
    }

    //LoadLevel 함수 호출시 자동으로 호출되는 함수
    private void OnLevelWasLoaded(int level)
    {
        //level=1 -> 게임 씬임
        if (level == 1)
        {
            InstantiatePlayer();
        }
       
    }


    //캐릭터 생성
    public void InstantiatePlayer()
    {
        //플레이어 넘버에 따라 다른 캐릭터 생성
        if (PhotonNetwork.PlayerList[0].NickName == PhotonNetwork.NickName)
        {
            PhotonNetwork.Instantiate("Prefabs/Player/Badger_Jasper", new Vector3(5, 1, -5), Quaternion.identity, 0);
        }
        else if (PhotonNetwork.PlayerList[1].NickName == PhotonNetwork.NickName)
        {
            PhotonNetwork.Instantiate("Prefabs/Player/Frog_Shanks", new Vector3(-6, 1, -5), Quaternion.identity, 0);
        }
        else if (PhotonNetwork.PlayerList[2].NickName == PhotonNetwork.NickName)
        {
            PhotonNetwork.Instantiate("Prefabs/Player/Panda_Apple", new Vector3(-4, 1, -5), Quaternion.identity, 0);
        }
        else if (PhotonNetwork.PlayerList[3].NickName == PhotonNetwork.NickName)
        {
            PhotonNetwork.Instantiate("Prefabs/Player/Rabbit_Sydney", new Vector3(-2, 1, -5), Quaternion.identity, 0);
        }
    }

    public void GoToLobbyButtonClick()
    {
        if (PhotonNetwork.IsMasterClient)
        {

            PhotonNetwork.LoadLevel("Lobby");

        }
    }
}
