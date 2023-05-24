using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Fame : MonoBehaviour
{
    public TMP_Text tmp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = "Fame : " + Managers.Fame.fame;
    }
}
