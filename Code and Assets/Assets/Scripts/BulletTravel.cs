using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BulletTravel : MonoBehaviour
{

    public float Speed;

    public float Damage;
    public float Range;
    private float DistanceTravelled;

    private DisplayHitmarker Hitmarker;
    private bool IsPlayer;

    public bool Explode;
    public  GameObject Explosion;


    void Start()
    {
        transform.parent = null;

        if (transform.root.CompareTag("Player"))
        {
            IsPlayer = true;
            Hitmarker = GameObject.Find("Crosshair").GetComponent<DisplayHitmarker>();
        }

        if (Explode)
        {
            Explosion.SetActive(false);
        }
    }

    public void OverwriteValues(float damage,float range)
    {
        Damage = damage;
        Range = range;
    }
 
    void Update()
    {
        
        transform.position += transform.forward * Speed * Time.deltaTime;
        if (DistanceTravelled > Range)
        {
            DestroyProjectile();
        }
        DistanceTravelled += Speed * Time.deltaTime;

    }








    public void OnCollisionEnter(Collision other)
    {
        
    
        CharacterFunctions enemy = other.transform.root.GetComponent<CharacterFunctions>();
        
        if (enemy != null)
        {

            enemy.TakeDamage(Damage);
            if (IsPlayer)
            {
                Hitmarker.ShowHitMarker();
            }

        }


        DestroyProjectile();
       
        
    }

    private void ActivateExplosion()
    {  
        enabled = false;
        Explosion.SetActive(true);
    }


    private void DestroyProjectile()
    {
        if (Explode)
        {
            ActivateExplosion();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
