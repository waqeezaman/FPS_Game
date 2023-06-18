using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash : Assets.Ability 
{

    public Transform[] DashPoints;
    public  Slider []Sliders;

    public float Distance;
    
    private float RechargeRate;
    

    public LayerMask CollisionMask;

    private Camera PlayerCamera;

    private MovePlayer PlayerMovement;

    void Start()
    {
        PlayerMovement = GetComponent<MovePlayer>();

        foreach (Slider Dash in Sliders)
        {
            Dash.value = Dash.minValue;
        }

        PlayerCamera = GameObject.Find("Player Camera").GetComponent <Camera>();

        RechargeRate = 1 / CooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
      

        if (Sliders[0].value>=1 )
        {
            Available = true;
        }
        else { Available = false; }

        if (Available && Input.GetMouseButtonDown  (1))
        {
           
            for (int i = 0; i <= Sliders.Length-2; i++)
            {
                Sliders[i].value = Sliders[i + 1].value;
            }

            Sliders[Sliders.Length - 1].value = 0;

            Move();

        }


        Recharge();
        
    }



    void Move()
    {
        Vector3 Direction=new Vector3 (0,0,0);
       
        Direction = FindDirection(Direction);
        transform.position = CastRay(Direction);
        
    }

    private Vector3 FindDirection(Vector3 Direction)
    {
        if (Input.GetKey (PlayerMovement .ForwardsKey ))
        {
            Direction += transform .forward ;
        }
        else if(Input.GetKey (PlayerMovement .BackwardsKey ))
        {
            Direction += transform.forward *-1;
        }
        if (Input.GetKey(PlayerMovement.RightKey ))
        {
            Direction += transform.right;
        }
        else if (Input.GetKey(PlayerMovement.LeftKey))
        {
            Direction += transform.right* -1;
        }

        if (Direction == new Vector3(0,0,0))
        {
            Direction += transform.forward;
        }

        return Direction;
    }

    Vector3 CastRay(Vector3 Direction)
    {
        RaycastHit hit;

        foreach (Transform DashPoint in DashPoints)
        {
            if (Physics.Raycast(DashPoint.position, Direction, out hit, Distance, CollisionMask))
            {
                return new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }
        }

    
            return  transform.position+Direction*Distance ;
            
      
    }

    void Recharge()
    {
        foreach (Slider Dash in Sliders)
        {
            if (Dash.value<Dash.maxValue )
            {
                Dash.value +=  RechargeRate * Time.deltaTime;
                
             return;
            }
        }
    }

  
}
