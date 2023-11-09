using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Manager : MonoBehaviour
{
    public GameObject[] cameras;
    void Update()
    {

        if (Input.GetKey(KeyCode.Alpha1))
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                if (i == 0){        
                    cameras[i].SetActive(true);
                }
                else{
                    cameras[i].SetActive(false);
                }
            }
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                if (i == 1){        
                    cameras[i].SetActive(true);
                }
                else{
                    cameras[i].SetActive(false);
                }
            }
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                if (i == 2){        
                    cameras[i].SetActive(true);
                }
                else{
                    cameras[i].SetActive(false);
                }
            }
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                if (i == 3){        
                    cameras[i].SetActive(true);
                }
                else{
                    cameras[i].SetActive(false);
                }
            }
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                if (i == 4){        
                    cameras[i].SetActive(true);
                }
                else{
                    cameras[i].SetActive(false);
                }
            }
        }
    }
}
