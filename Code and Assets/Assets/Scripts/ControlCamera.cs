using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{

    public Camera PlayerCam;
   // public Transform PlayerCam;

    public float Sensitivity;
    private float XRotation=0;
    private float YRotation=0;

    public float MaxYRotation;
    public float MinYRotation;
 void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        Turn();
        
    }

    public void Turn()
    {
      
   XRotation =   Mathf.Clamp(  XRotation + Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime*-1,MinYRotation,MaxYRotation);
        YRotation += Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, YRotation, 0);
        PlayerCam.transform.rotation = Quaternion.Euler(XRotation, YRotation , 0);
        
    }

  
}
