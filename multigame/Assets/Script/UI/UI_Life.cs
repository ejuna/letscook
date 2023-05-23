using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Life : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        init();
    }
    void init()
    {
        for(int i = 0; i< Constants.MAX_LIFE; i++)
        {
            GameObject life = Managers.Resource.Instantiate("life");
            life.transform.SetParent(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
