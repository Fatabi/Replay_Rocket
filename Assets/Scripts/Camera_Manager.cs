using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Manager : MonoBehaviour
{
    public Playback_Mod playBackMod; 
    public Playback_Mod_Manager playbackModManager;
    public GameObject[] cameras;
    public bool disableAll;
    public int selectedCameraIdx = 0 ;
    private int tempSelectedCamera = -1;
    void Update(){
        UpdateCameras();
        ChangeCamera();
    }
    void ChangeCamera()
    {
        tempSelectedCamera = selectedCameraIdx;
        for (int i = 0; i < cameras.Length; i++)
        {
            if (Input.GetKeyDown((KeyCode)(49+i)))
            {
                selectedCameraIdx = i;
            }
        }
    }
    void UpdateCameras(){
       
            for (int i = 0; i < cameras.Length; i++)
            {
                if (!disableAll)
                {
                    if ((i == selectedCameraIdx) && (playBackMod == playbackModManager.playbackMod))
                    {
                        cameras[i].SetActive(true);
                    }
                    else
                    {
                        cameras[i].SetActive(false);
                    }
                }
            
                else
                {
                     cameras[i].SetActive(false);
                }
            }
        
    }
}