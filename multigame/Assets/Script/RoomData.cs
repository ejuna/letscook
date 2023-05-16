using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomData : MonoBehaviour
{
    private TMP_Text RoomInfoText;
    private RoomInfo _roomInfo;


    public TMP_InputField userIdText;
    public TextMeshProUGUI joinRoomName;

    public RoomInfo RoomInfo
    {
        get
        {
            return _roomInfo;
        }
        set
        {
            _roomInfo = value;
            string[] roomName = _roomInfo.Name.Split('_');
            RoomInfoText.text = $"{roomName[0]}({_roomInfo.PlayerCount}/{_roomInfo.MaxPlayers})";
            GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnClickRoom(_roomInfo.Name));
        }
    }

    



    void Awake()
    {
        RoomInfoText = GetComponentInChildren<TMP_Text>();
        userIdText = GameObject.Find("NickName").GetComponent<TMP_InputField>();
    }

    void OnClickRoom(string room)
    {
        string[] password = room.Split('_');

        GameObject.Find("Panel-BackGround").transform.Find("Panel-Password").gameObject.SetActive(true);
        GameObject.Find("JoinRoomName").gameObject.GetComponent<TextMeshProUGUI>().text= password[0];

    }

    

}
