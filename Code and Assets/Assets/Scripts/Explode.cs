using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float Force;
    public float SplashDamage;

    private DisplayHitmarker HitMarker;

    private List<CharacterFunctions> Collisions = new List<CharacterFunctions>();
    void Start()
    {
       HitMarker = GameObject.Find("Crosshair").GetComponent<DisplayHitmarker>();
    }
    private void OnEnable()
    {
       GetComponent<SphereCollider>().enabled = true;

        GetComponent<ParticleSystem>().Play();
        GetComponent<ParticleSystem>().startSize = GetComponent<SphereCollider>().radius / 6f;
        StartCoroutine (DestroyObject(GetComponent<ParticleSystem>().main.duration));
    }

    private void OnTriggerEnter(Collider other)
    {

        CharacterFunctions enemy = other.transform.root.GetComponent<CharacterFunctions>();

        if (enemy != null && NewCollision (enemy)==true)
        {
            Collisions.Add(enemy);
            enemy.TakeDamage(SplashDamage);
            HitMarker.ShowHitMarker();
        }


        if (other.GetComponent <Rigidbody>()!=null)
        {
            other.GetComponent<Rigidbody>().AddForce(transform.forward *Force);
            other.GetComponent<Rigidbody>().AddForce(transform.up * Force );

        }
    }
  
    private IEnumerator DestroyObject(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(transform.root.gameObject);
    }

    private bool NewCollision(CharacterFunctions enemy)
    {
        foreach (CharacterFunctions  Hit in Collisions)
        {
            if (enemy == Hit)
            {
                return false;
            }
        }        
        
        return true;
    }
}
