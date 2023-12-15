using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class Missile_Manager : MonoBehaviour
{
    public GameObject missilePrefab;
    public Transform missileCSVPanel;
    public Time_Manager timeManager;
    public Missile_Launcher_Manager missile_Launcher_Manager;
    public Missile[] missileArray;
    void Awake()
    {
        for (int i = 0; i < missileCSVPanel.childCount; i++)
        {
            missileCSVPanel.GetChild(i).GetComponent<TMP_InputField>().text = "C:/GIT/Replay_Rocket/Assets/Data/hava-kara/hava2kara_m" + (i+1) + ".csv";
        }
        transform.position = new Vector3(0f,0f,0f);
    }
    public void CreateMissilePrefabs(){

        gameObject.GetComponent<Camera_Manager>().cameras = new GameObject[missileCSVPanel.childCount];
        for (int i = 0; i < missileCSVPanel.childCount; i++)
        {
            GameObject gameObjectInstance = Instantiate(missilePrefab,transform);
            gameObjectInstance.GetComponent<Missile>().inputField = missileCSVPanel.GetChild(i).GetComponent<TMP_InputField>();
            gameObjectInstance.GetComponent<Missile>().GenerateDataFromCSV();
            gameObjectInstance.GetComponent<Missile>().ChangeColorWithRandom();
            gameObjectInstance.GetComponent<Missile>().timeManager = timeManager;
            gameObject.GetComponent<Camera_Manager>().cameras[i] = gameObjectInstance.transform.GetChild(1).GetChild(0).gameObject;
            
        }
    }
    public void DeleteMissilePrefabs(){
        int childCount = transform.childCount;
        for (int i = (childCount-1); i >= 0 ; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        gameObject.GetComponent<Camera_Manager>().cameras = new GameObject[0];
    }

    public void LoadCSV(){
        DeleteMissilePrefabs();
        CreateMissilePrefabs();

        missile_Launcher_Manager.DeleteLaunchers();
        missile_Launcher_Manager.CreateLaunchers();
    }
}
