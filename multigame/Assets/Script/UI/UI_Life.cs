using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UI_Life : MonoBehaviour
{
    public GameObject hp1;
    public GameObject hp2;
    public GameObject hp3;
    void Start()
   {
        hp1 = GameObject.Find("Hp1");
        hp2 = GameObject.Find("Hp2");
        hp3 = GameObject.Find("Hp3");
    }


    // Update is called once per frame
    void Update()
    {
        switch (Managers.Life.life)
        {
            case 0:
                hp1.SetActive(false);
                hp2.SetActive(false);
                hp3.SetActive(false);
                break;
            case 1:
                hp1.SetActive(true);
                hp2.SetActive(false);
                hp3.SetActive(false);
                break;
            case 2:
                hp1.SetActive(true);
                hp2.SetActive(true);
                hp3.SetActive(false);
                break;
            case 3:
                hp1.SetActive(true);
                hp2.SetActive(true);
                hp3.SetActive(true);
                break;
        }
        


    }
    public void minusLife()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void resetLife()
    {
        if (Managers.Life.life < Constants.MAX_LIFE)
        {
            for (int i = Managers.Life.life; i < Constants.MAX_LIFE; i++)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
