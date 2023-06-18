using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public KeyCode ForwardsKey,LeftKey,RightKey,BackwardsKey;
  //  public CharacterController Controller;

    public float MoveDistance;

    void Update()
    {
        Move();
    }


    private void Move()
    {
        if (Input.GetKey(ForwardsKey))
        {
            transform.position+=(transform.forward * MoveDistance*Time.deltaTime);
        }
        else if (Input.GetKey(BackwardsKey))
        {
            transform.position+=(transform.forward * MoveDistance*-1 * Time.deltaTime);
        }


        if (Input.GetKey(RightKey))
        {
            transform.position+=(transform.right  * MoveDistance * Time.deltaTime);
        }
        else if (Input.GetKey(LeftKey))
        {
            transform.position+=(transform.right * MoveDistance*-1 * Time.deltaTime);
        }
    }
}
