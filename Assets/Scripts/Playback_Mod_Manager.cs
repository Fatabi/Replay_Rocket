using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting; // Import the TextMeshPro namespace

public class Playback_Mod_Manager : MonoBehaviour
{
    public GameObject missileManagerGameObject;
    public GameObject targetManagerGameObject;
    public Playback_Mod playbackMod = Playback_Mod.Target;
    public TextMeshProUGUI changeModButtonTMP;
    private int selectedCameraIdx;
    private int tempMissileselectedCameraIdx = 0;
    private int tempTargetselectedCameraIdx = 0;
    private Camera_Manager missileCameraManager;
    private Camera_Manager targetCameraManager;
    private bool needsUpdate;
    private int tempSelectedCameraIdx;
    private Playback_Mod tempPlaybackMod;
    public void Start(){
        missileCameraManager = missileManagerGameObject.GetComponent<Camera_Manager>();
        targetCameraManager = targetManagerGameObject.GetComponent<Camera_Manager>();
    }
    public void Update(){
        UpdateSelectedCameraIdx();
        if ((playbackMod != tempPlaybackMod) || (selectedCameraIdx != tempSelectedCameraIdx)){
                UpdateButtonText();
        }
        tempPlaybackMod = playbackMod;
        tempSelectedCameraIdx = selectedCameraIdx;
           
    }
    public void ChangePlaybackMod(){
        switch (playbackMod)
        {
            case Playback_Mod.Missile:
                tempMissileselectedCameraIdx = missileCameraManager.selectedCameraIdx;
                missileCameraManager.disableAll = true; // Makes all camera disable
                targetCameraManager.disableAll = false; 
                targetCameraManager.selectedCameraIdx = tempTargetselectedCameraIdx;;
                playbackMod = Playback_Mod.Target;
                break;
            case Playback_Mod.Target:
                tempTargetselectedCameraIdx = targetCameraManager.selectedCameraIdx;
                missileCameraManager.disableAll = false; 
                targetCameraManager.disableAll = true; // Makes all camera disable 
                missileCameraManager.selectedCameraIdx = tempMissileselectedCameraIdx; 
                playbackMod = Playback_Mod.Missile;
                break;
        }
        UpdateSelectedCameraIdx();
        UpdateButtonText();
        needsUpdate = true;
    }
    public void UpdateButtonText(){
        changeModButtonTMP.text = playbackMod.ToString() + " " + (selectedCameraIdx + 1); 
    }
    public void UpdateSelectedCameraIdx(){
        switch (playbackMod)
        {
            case Playback_Mod.Missile:
                selectedCameraIdx = missileCameraManager.selectedCameraIdx;
                break;

            case Playback_Mod.Target:
                selectedCameraIdx = targetCameraManager.selectedCameraIdx;
                break;
        }
        
    }
}

public enum Playback_Mod{
    Missile,
    Target
}