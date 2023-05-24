using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Money : MonoBehaviour
{
    public TMP_Text tmp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = "Money : " + Managers.Money.money;
    }
}
