using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientContainerUI2 : MonoBehaviour
{
    public GameObject containerPanel;
    bool activeContainer = false;

    private void Start()
    {
        containerPanel.SetActive(activeContainer);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            activeContainer = !activeContainer;
            containerPanel.SetActive(activeContainer);
        }
    }
}
