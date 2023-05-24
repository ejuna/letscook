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
    public void minusLife()
    {
        Managers.Resource.Destroy(transform.GetChild(transform.childCount).gameObject);
    }
    public void resetLife()
    {
        if (Managers.Life.getLife() < Constants.MAX_LIFE)
        {
            for (int i = Managers.Life.getLife(); i < Constants.MAX_LIFE; i++)
            {
                GameObject life = Managers.Resource.Instantiate("life");
                life.transform.SetParent(transform);
            }
        }
    }
}
