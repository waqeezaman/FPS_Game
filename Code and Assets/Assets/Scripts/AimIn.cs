using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimIn : MonoBehaviour
{
    private Camera PlayerCamera;
    private Camera ADSCam;
    private Canvas  ADSCanvas;
    private Canvas HUD;

 
    private string CamTag= "Camera";
    private string MainCamTag = "MainCamera";

    private Fire Weapon;
    void Start()
    {
        Weapon = transform.GetComponentInParent<Fire>();
        

        ADSCam = GetComponent<Camera>();
        ADSCam.enabled = false;
        gameObject.tag =CamTag;
        HUD = GameObject.Find("HUD").GetComponent <Canvas>();

        ADSCanvas = GameObject.Find("ADS Canvas").GetComponent <Canvas >();
        ADSCanvas.enabled = false;
        PlayerCamera = GameObject.Find("Player Camera").GetComponent<Camera>();

        transform.position = PlayerCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && !Weapon.Reloading)
        {
            EnterADS();
        }
        else if (Input.GetMouseButtonUp(1) || Weapon.Reloading)
        {
            ExitADS();

        }
    }

    void ExitADS()
    {
        gameObject.tag = CamTag;
        PlayerCamera.tag = MainCamTag;

        PlayerCamera.enabled = true;
        HUD.enabled = true;

        ADSCam.enabled = false;
        ADSCanvas.enabled = false;
    }

    void EnterADS()
    {
        HUD.enabled = false;
        PlayerCamera.enabled = false;

        ADSCam.enabled = true;
        ADSCanvas.enabled = true;

        gameObject.tag = MainCamTag;
        PlayerCamera.tag = CamTag;
    }
}
