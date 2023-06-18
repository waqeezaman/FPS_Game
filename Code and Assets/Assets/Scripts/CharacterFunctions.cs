using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFunctions : MonoBehaviour
{

    public float Health;
    public float MaxHealth;

    PlayerController playerController;
 

   

   public   void Start()
    {

        if (transform.root.CompareTag("Player"))
        {
            playerController = transform.root.GetComponent<PlayerController>();
        }

        Health = MaxHealth;


     
    }

    public void TakeDamage(float Damage )
    {
        Health -= Damage;
       

        if (Health <= 0)
        {
            if (playerController != null)
            {
                playerController.Die();
                return;
            }
            Destroy(gameObject);
        
        }

   

      
    }
}
