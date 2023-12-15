using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller_Target : MonoBehaviour
{
    public GameObject target;
    public float distance_m;
    private Vector3 initPos;
    private Vector3 initTargetPos;
    void Awake(){
        initPos = gameObject.transform.position;
        initTargetPos = target.transform.position;
    }
    void Update()
    {
        gameObject.transform.position = target.transform.position  -  (initTargetPos - initPos).normalized*distance_m;
        gameObject.transform.LookAt(target.transform.position);
    }
}
