using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class Target_Manager : MonoBehaviour
{
    public GameObject targetPrefab;
    public Transform targetCSVPanel;
    public Time_Manager timeManager;

    void Awake()
    {
        for (int i = 0; i < targetCSVPanel.childCount; i++)
        {   targetCSVPanel.GetChild(i).GetComponent<TMP_InputField>().text = "C:/GIT/Replay_Rocket/Assets/Data/hava-kara/hava2kara_hedef" + (i+1) +".csv";
        }
        transform.position = new Vector3(0f,0f,0f);
    }

    public void CreateTargetPrefabs(){
        transform.position = new Vector3(0f,0f,0f);
        gameObject.GetComponent<Camera_Manager>().cameras = new GameObject[targetCSVPanel.childCount];
        for (int i = 0; i < targetCSVPanel.childCount; i++)
        {
            
            GameObject gameObjectInstance = Instantiate(targetPrefab,transform);
            gameObjectInstance.GetComponent<Target>().inputField = targetCSVPanel.GetChild(i).GetComponent<TMP_InputField>();
            gameObjectInstance.GetComponent<Target>().GenerateDataFromCSV();
            gameObjectInstance.GetComponent<Target>().ChangeColorWithRandom();
            gameObjectInstance.GetComponent<Target>().timeManager = timeManager;
            gameObject.GetComponent<Camera_Manager>().cameras[i] = gameObjectInstance.transform.GetChild(1).GetChild(0).gameObject;
            
        }
    }
    public void DeleteTargetPrefabs(){
        int childCount = transform.childCount;
        for (int i = (childCount-1); i >= 0 ; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        gameObject.GetComponent<Camera_Manager>().cameras = new GameObject[0];
    }
    
    public void LoadCSV(){
        DeleteTargetPrefabs();
        CreateTargetPrefabs();
    }
}
