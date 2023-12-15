using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Missile_Launcher_Manager : MonoBehaviour
{
    public LauncherType[] launcherTypes;
    public GameObject groundLauncherPrefab;
    public GameObject airLauncherPrefab;
    public GameObject missileManager;
    public Time_Manager time_Manager;
    private GameObject selectedLauncherPrefab;
    private GameObject[] launcherArray;
    private Renderer missileRenderer;
    private Renderer launcherRenderer;
    public void CreateLaunchers(){
        
        transform.position = new Vector3(0f,0f,0f);
        launcherArray = new GameObject[missileManager.transform.childCount];
        launcherTypes = new LauncherType[missileManager.transform.childCount];
        for (int i = 0; i < missileManager.transform.childCount; i++)
        {
            float altitude_m = missileManager.transform.GetChild(i).GetComponent<Missile>().xyz_m[0].z*(-1f);
            if (altitude_m>50)
            {
                launcherTypes[i] = LauncherType.Air;
                SelectPrefab(launcherTypes[i]);
                launcherArray[i] = Instantiate(selectedLauncherPrefab,transform);
                Vector3 initPosMissile = missileManager.transform.GetChild(i).GetComponent<Missile>().xyz_m[0];
                float initVelocity = missileManager.transform.GetChild(i).GetComponent<Missile>().initVelocity;
                float startTime = missileManager.transform.GetChild(i).GetComponent<Missile>().startTime;
                launcherArray[i].GetComponent<F16_Move>().timeManager = time_Manager;
                launcherArray[i].GetComponent<F16_Move>().initPosition = initPosMissile;
                launcherArray[i].GetComponent<F16_Move>().tangentialVelocity = initVelocity*2;
                launcherArray[i].GetComponent<F16_Move>().startTime = startTime;
                launcherArray[i].transform.localEulerAngles = new Vector3(launcherArray[i].transform.localEulerAngles.x,missileManager.transform.GetChild(i).GetComponent<Missile>().phiThetaPsiDeg[0].y+90f,launcherArray[i].transform.localEulerAngles.z);
                launcherArray[i].GetComponent<F16_Move>().CreateMovement();
            }
            else{
                launcherTypes[i] = LauncherType.Ground;
                 SelectPrefab(launcherTypes[i]);
                launcherArray[i] = Instantiate(selectedLauncherPrefab,transform);
                Vector3 initPosMissile = missileManager.transform.GetChild(i).GetComponent<Missile>().xyz_m[0];
                launcherArray[i].transform.position = new Vector3(initPosMissile.y,initPosMissile.z*(-1f),initPosMissile.x);
                launcherArray[i].transform.localEulerAngles = new Vector3(launcherArray[i].transform.localEulerAngles.x,missileManager.transform.GetChild(i).GetComponent<Missile>().phiThetaPsiDeg[0].z+90f,launcherArray[i].transform.localEulerAngles.z);

            }
           
        }
        // UpdateColorFromMissile();
    }
    public void DeleteLaunchers(){
        if (launcherArray != null)
        {    
            for (int i = 0; i < launcherArray.Length; i++)
            {
                DestroyImmediate(launcherArray[i]);
            }
        }
    }

    // public void UpdateColorFromMissile(){
    //     for (int i = 0; i < missileManager.transform.childCount; i++)
    //     {
    //         missileRenderer = missileManager.transform.GetChild(i).GetComponent<Renderer>();
    //         launcherRenderer = transform.GetChild(i).GetComponent<Renderer>();
    //         launcherRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
    //         launcherRenderer.sharedMaterial.color = missileRenderer.sharedMaterial.color;
    //     } 
    // }
    public void SelectPrefab(LauncherType launcherType){
        switch (launcherType)
        {
            case LauncherType.Air:
                selectedLauncherPrefab = airLauncherPrefab;
                break;
            case LauncherType.Ground:
                selectedLauncherPrefab = groundLauncherPrefab;
                break;            
        }
    }
}
public enum LauncherType{
    Ground,
    Air,
}



