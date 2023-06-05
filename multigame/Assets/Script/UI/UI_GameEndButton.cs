using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameEndButton : MonoBehaviour
{

    public Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(OnClick);
    }

    

    void OnClick()
    {
        GameObject.Find("PhotonManager").GetComponent<PhotonManager>().GoToLobbyButtonClick();
    }
}
