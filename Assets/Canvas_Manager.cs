using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Manager : MonoBehaviour
{
    public GameObject panel;
    private bool hide;
    private bool tempHide;
    // Update is called once per frame
    void Update()
    {
        UpdateHideShow();
    }
    public void UpdateHideShow(){
        if (tempHide != hide)
        {
            panel.SetActive(!hide);
        }
        tempHide = hide;
    }
    public void ChangeHideMod(){
        if (hide){
            hide = false;
        }
        else
        {
            hide = true;
        }
    }
}
