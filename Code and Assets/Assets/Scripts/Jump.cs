using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField]
    private float JumpAcceleration;
    private float JumpSpeed;

    public float JumpHeight;
    public KeyCode JumpKey;


    public bool OnGround;
    public float HeightJumped;
    public bool Jumping;
   
    
    void Start()
    {

    }

    void Update()
    {
       
        if (Input.GetKeyDown (JumpKey) && !Jumping)
        {
            Jumping = true;
            
        }
      
        if (Jumping)
        {         
            if(HeightJumped<JumpHeight)
            {
                JumpSpeed += JumpAcceleration*Time.deltaTime ;
                HeightJumped += JumpSpeed *Time.deltaTime + Physics.gravity.y*-1*Time.deltaTime;
                transform.position += new Vector3(0, JumpSpeed * Time.deltaTime + Physics.gravity.y * -1*Time.deltaTime, 0);
            }
            else 
            {
                Jumping = false;
                JumpSpeed = 0;
            }
        }

    
    }
}
