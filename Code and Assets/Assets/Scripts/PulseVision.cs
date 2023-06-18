using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PulseVision : Ability 
{


    //  private bool Available;


    
  
  

    public float Duration;
    public KeyCode PulseVisionKey;



    private Camera PlayerCamera;

    [SerializeField]
    private Color PulseCamBackGround;
    [SerializeField]
    private LayerMask PulseVisionLayerMask;

    private LayerMask PlayerCameraLayerMask;
    private Color PlayerCameraBackGround;
    

    void Start()
    {

     
        Available = true;

        PlayerCamera = GameObject.Find("Player Camera").GetComponent<Camera>();
        PlayerCameraLayerMask = PlayerCamera.cullingMask;
        PlayerCameraBackGround = PlayerCamera.backgroundColor;


        if ( CountdownText == null)
        {
            GameObject.Find("Pulse Vision Text");
        }
        CountdownText.text = "";

        transform.position = PlayerCamera.transform.position;
    }


    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(PulseVisionKey) && Available)
        {

            StartCoroutine(ActivatePulseVision());
        }
    }

  private IEnumerator ActivatePulseVision()
    {

        
        PlayerCamera.clearFlags = CameraClearFlags.Color;
        PlayerCamera.backgroundColor = PulseCamBackGround;
        PlayerCamera.cullingMask = PulseVisionLayerMask;

        StartCoroutine(CountdownDisplay()) ;

        yield return new WaitForSeconds(Duration);

        PlayerCamera.cullingMask = PlayerCameraLayerMask;
        PlayerCamera.backgroundColor = PlayerCameraBackGround;
        PlayerCamera.clearFlags = CameraClearFlags.Skybox;



       
    }




}
