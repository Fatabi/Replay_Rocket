using UnityEngine;

public class MoveAllObjects : MonoBehaviour
{
    public GameObject missile_Manager_GameObject;
    public GameObject missile_Launcher_Manager_GameObject;
    public GameObject target_Manager_GameObject;
    private Missile[] missiles;
    private F16_Move[] f16_Moves;
    private Target[] targets;
    public bool needLoad;
    void Start(){
        Initialize();
    }


    void Update(){
        if (needLoad)
        {
            Initialize();
        }
        foreach (F16_Move f16_Move in f16_Moves)
        {
            if (f16_Move != null)
            {
            f16_Move.UpdateState();
            }
        }
        foreach (Missile missile in missiles)
        {
            missile.UpdateState();
        }
        foreach (Target target in targets)
        {
            target.UpdateState();
        }        
    }
    public void SetNeedLoad(){
        needLoad = true;
    }
    public void Initialize(){
        missile_Manager_GameObject.GetComponent<Missile_Manager>().DeleteMissilePrefabs();
        missile_Launcher_Manager_GameObject.GetComponent<Missile_Launcher_Manager>().DeleteLaunchers();
        target_Manager_GameObject.GetComponent<Target_Manager>().DeleteTargetPrefabs();

        missile_Manager_GameObject.GetComponent<Missile_Manager>().CreateMissilePrefabs();
        missile_Launcher_Manager_GameObject.GetComponent<Missile_Launcher_Manager>().CreateLaunchers();
        target_Manager_GameObject.GetComponent<Target_Manager>().CreateTargetPrefabs();

        missiles = new Missile[missile_Manager_GameObject.transform.childCount];
        for (int i = 0; i < missile_Manager_GameObject.transform.childCount; i++)
        {
            missiles[i] = missile_Manager_GameObject.transform.GetChild(i).GetComponent<Missile>();
        }

        f16_Moves = new F16_Move[missile_Launcher_Manager_GameObject.transform.childCount];
        for (int i = 0; i < missile_Launcher_Manager_GameObject.transform.childCount; i++)
        {
            f16_Moves[i] = missile_Launcher_Manager_GameObject.transform.GetChild(i).GetComponent<F16_Move>();
        }

        targets = new Target[target_Manager_GameObject.transform.childCount];
        for (int i = 0; i < target_Manager_GameObject.transform.childCount; i++)
        {
            targets[i] = target_Manager_GameObject.transform.GetChild(i).GetComponent<Target>();
        }   
        needLoad = false;
    }

}
