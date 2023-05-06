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
    public TextMeshProUGUI nickName; //닉네임 표시
    public Button createButton; //방생성 버튼
    public Button joinButton; //방참가 버튼

    private Dictionary<string, GameObject> roomDict = new Dictionary<string, GameObject>();
    public GameObject roomPrefab; //룸 프리팹
    public Transform scrollContent;

    public GameObject createRoomMenu; //방 세부 설정 메뉴
    public TMP_InputField roomNameText; //방 이름
    public TMP_InputField password; //방 비밀번호
    public Button makeRoomButton; //방개설 버튼



    void Start()
    {
        nickName.text = PhotonNetwork.LocalPlayer.NickName;
    }

    public void createButtonClick()
    {
        createRoomMenu.SetActive(true);  //방만들기 메뉴 설정 활성화
    }

    public void makeRoomButtonClick()  //방만들기 버튼 클릭
    {

        RoomOptions ro = new RoomOptions();  //방 옵션 설정
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 4;

        PhotonNetwork.CreateRoom(roomNameText.text, ro);
        createRoomMenu.SetActive(false);
        SceneManager.LoadScene("Room");
    }

    public void joinButtonClick()
    {

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        /*GameObject tempRoom = null;
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
                    _room.SetActive(true);
                    Debug.Log("방생성했음");
                }
                else
                {
                    roomDict.TryGetValue(room.Name, out tempRoom);
                    tempRoom.GetComponent<RoomData>().RoomInfo = room;
                }
            }
        }

    }*/
       
    }
}
