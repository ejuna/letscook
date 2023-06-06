using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToGame : MonoBehaviour
{
  public GameObject ui;
  public Image[] page;
  public GameObject nextBtn;
  private int pageNum;

  private void Awake()
  {
    ui.SetActive(false);
  }

  public void exit(){
    page[pageNum].gameObject.SetActive(false);
    ui.SetActive(false);
    pageNum = 0;
  }

  public void join(){
    ui.SetActive(true);
    pageNum = 0;
    nextBtn.SetActive(true);
    page[pageNum].gameObject.SetActive(true);
  }

  public void next()
  {
    page[pageNum].gameObject.SetActive(false);
    pageNum++;
    page[pageNum].gameObject.SetActive(true);
    if (pageNum == page.Length-1)
    {
      nextBtn.SetActive(false);
    }
  }
}
