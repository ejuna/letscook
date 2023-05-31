using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Life : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        init();
        Managers.Life.minusLifeAction -= minusLife;
        Managers.Life.minusLifeAction += minusLife;
        Managers.Life.resetLifeAction -= resetLife;
        Managers.Life.resetLifeAction += resetLife;
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
        if (Managers.Life.life < Constants.MAX_LIFE)
        {
            for (int i = Managers.Life.life; i < Constants.MAX_LIFE; i++)
            {
                GameObject life = Managers.Resource.Instantiate("life");
                life.transform.SetParent(transform);
            }
        }
    }
}
