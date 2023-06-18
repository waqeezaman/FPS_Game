using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOnGround : MonoBehaviour
{
    public Jump PlayerJump;
    private const string JumpTag = "Jumpable";
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(JumpTag )   )
        {
            PlayerJump.OnGround = true;
            PlayerJump.HeightJumped = 0;

            PlayerJump.Jumping = false; 
        
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(JumpTag))
        {
            PlayerJump.OnGround = false;
        }
    }
}
