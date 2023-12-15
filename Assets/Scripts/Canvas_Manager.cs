using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using UnityEngine.UI;
public class Canvas_Manager : MonoBehaviour
{
    public Transform missileCsvTMPPanel;
    public Transform targetCsvTMPPanel;
    public TMP_InputField CsvTMP;
    private bool csvPanelActive = false;
    public tabTypes tabType;
    public GameObject missilePanel;
    public GameObject targetPanel;
    public Button missileButton;
    public Button targetButton;
    public Color activeColor;
    public Color disableColor;
    public GameObject csvPanel;
    
    void Start()
    {
        UpdateTab();
        UpdateCSVPanelEnable();
    }

    public void CreateNewMissileCsvTMP(){
        Instantiate(CsvTMP,missileCsvTMPPanel);
    }
    public void DeleteMissileCsvTMP(){
        if (missileCsvTMPPanel.childCount > 1){
            DestroyImmediate(missileCsvTMPPanel.GetChild(missileCsvTMPPanel.childCount-1).gameObject);
        }
    }
    public void CreateNewTargetCsvTMP(){
        Instantiate(CsvTMP,targetCsvTMPPanel);
    }
    public void DeleteTargetCsvTMP(){
        if (targetCsvTMPPanel.childCount > 1){
            DestroyImmediate(targetCsvTMPPanel.GetChild(targetCsvTMPPanel.childCount-1).gameObject);
        }
    }
    public void ActivateMissileTab(){
        tabType = tabTypes.Missile;
        UpdateTab();
    }
    public void ActivateTargetTab(){
        tabType = tabTypes.Target;
        UpdateTab();
    }
    public void UpdateTab(){
    
        switch (tabType)
        {
            case tabTypes.Missile:
                targetPanel.SetActive(false);
                missilePanel.SetActive(true);
                missileButton.image.color = activeColor;
                targetButton.image.color = disableColor;
                break;
            case tabTypes.Target:
                targetPanel.SetActive(true);
                missilePanel.SetActive(false);
                missileButton.image.color = disableColor;
                targetButton.image.color =  activeColor;
                break;
            default:
                break;
            
        }
    }
    public void ChangeCSVPanelEnable(){
        if (csvPanelActive)
        {
            csvPanelActive = false;
        }
        else
        {
            csvPanelActive = true;
        }
        UpdateCSVPanelEnable();
    }
    public void UpdateCSVPanelEnable(){
        if (csvPanelActive)
        {
            csvPanel.SetActive(true);
        }
        else
        {
            csvPanel.SetActive(false);
        }
    }
}

  public enum tabTypes{
        Missile,
        Target
        }