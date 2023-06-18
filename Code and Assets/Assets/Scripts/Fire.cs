using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fire : MonoBehaviour
{

    private Animator AnimationController;

    public float Damage;

    public int MagazineSize;
    public int RoundsInClip;
    public float ReloadTime;
    public bool Reloading;

    [SerializeField]
    private PlayerUI GunUI;

    public float Range;

    public float FireRate;
   
    private bool CanShoot=true;

    public bool HitScan;
    public GameObject Projectile;


    public Transform FirePoint;


    public float BulletTrailTime;

    private DisplayHitmarker Hitmarker;

 
    public LayerMask  PlayerMask;


    private bool IsPlayer;

    void Start()
    {

         if (transform.root.CompareTag("Player"))
        {
            IsPlayer = true;
        }

      

        FireRate =  60/FireRate ;


        if (IsPlayer)
        {
            GunUI = GameObject.Find("HUD").GetComponent<PlayerUI>();
            GunUI.UpdateAmmoText(MagazineSize, RoundsInClip);
        

            if (HitScan)
            {
                Hitmarker = GameObject.Find("Crosshair").GetComponent<DisplayHitmarker>();
            }
            else
            {
                Range = Projectile.GetComponentInChildren<BulletTravel>().Range;
            }
        }
     


    
    }

  
    void Update()
    {

        if (!Reloading && (RoundsInClip <= 0 ))
        {
            StartReload();
        }

    }
    private void OnEnable()
    {
        if (!CanShoot)
        {
            StartCoroutine(DelayBetweenShots());
        }

        if (IsPlayer)
        {
            if (GunUI == null)
            {
                GunUI = GameObject.Find("HUD").GetComponent<PlayerUI>();
            }
            GunUI.WeaponSwitch();
        }
    }
    private void OnDisable()
    {
        if (IsPlayer)
        {
            if (Reloading)
            {
                Reloading = false;

                GunUI.CloseReloadSlider();
            }
        }
    }

    public void Shoot(Transform Origin)
    {



        if (CanShoot & !Reloading)
        {

           
            RoundsInClip -= 1;
            if (IsPlayer)
            {
                GunUI.UpdateAmmoText(MagazineSize, RoundsInClip);

            }

            StartCoroutine(DelayBetweenShots());


            if (HitScan)
            {
                    FireHitScan(Origin);
                    StartCoroutine(ActivateBulletTrail());

            }
            else
            {
                    SpawnProjectile(Origin);

            }


            

        }
    

    }

  

    private void FireHitScan(Transform Origin)
    {
        RaycastHit hit;

        if (Physics.Raycast(Origin.position, Origin.forward, out hit, Range,PlayerMask ))
        {
            
            CharacterFunctions enemy = hit.collider.transform.root.GetComponent<CharacterFunctions>();
            if (enemy != null)
            {
                enemy.TakeDamage(Damage);
                if (IsPlayer)
                {
                    Hitmarker.ShowHitMarker();
                }
            }

        }
     
        
    }

    private void SpawnProjectile(Transform Origin)
    {
        RaycastHit hit;
        GameObject newProjectile = Instantiate(Projectile, FirePoint.position,FirePoint.rotation);
        
        if (!Physics.Raycast(Origin.position, Origin.forward, out hit, Range,PlayerMask ))      
        {
            hit.point = Origin.transform.position+ Origin.forward*Range;
        }
        
        
        newProjectile.transform.LookAt(hit.point);
        newProjectile.transform.SetParent(transform);
        if (!IsPlayer)
        {
            newProjectile.GetComponent<BulletTravel>().OverwriteValues(Damage, Range);
        }
    }

 
    public void StartReload()
    {
        StartCoroutine(Reload());
    }
    private IEnumerator Reload()
    {

       
        Reloading = true;
       
        if (IsPlayer)
        {
            StartCoroutine(GunUI.ActivateReloadSlider(ReloadTime));
        }
        yield return new WaitForSeconds (ReloadTime );

        Reloading = false;
        RoundsInClip = MagazineSize;

        if (IsPlayer)
        {
            GunUI.UpdateAmmoText(MagazineSize, RoundsInClip);
        }
     

    }

    private IEnumerator DelayBetweenShots()
    {
        
        CanShoot = false;
        yield return new WaitForSeconds(FireRate) ;
        CanShoot = true;


    }

    private IEnumerator ActivateBulletTrail()
    {
        // start bullet trail
        yield return new WaitForSeconds(BulletTrailTime );
       // end bullet trail

    }

}
