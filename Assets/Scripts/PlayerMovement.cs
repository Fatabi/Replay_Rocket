using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float cizgiselHiz;
    public float acisalHiz;
    void Update()
    {
        transform.Translate(cizgiselHiz*Time.deltaTime,0f,0f,transform);
        transform.Rotate(0f,acisalHiz*Time.deltaTime,0f);
    }
}
