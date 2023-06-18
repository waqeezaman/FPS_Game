using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public Fire CurrentWeapon;

    [SerializeField]
    private Fire PrimaryWeapon;

    [SerializeField]
    private Fire SecondaryWeapon;

    [SerializeField]
    private KeyCode SwitchWeaponKey;

    [SerializeField]
    private KeyCode ReloadKey;

    
  
    // Start is called before the first frame update
    void Start()
    {

        CurrentWeapon = PrimaryWeapon;
        if (SecondaryWeapon != null)
        {
            SecondaryWeapon.gameObject.SetActive(false);
        }

  


    }

    // Update is called once per frame
    void Update()
    {
        CheckReload();
        SwitchWeapon();
        FireWeapon();
    }


    private void FireWeapon()
    {
        if ( Input.GetMouseButton(0))
        {
            CurrentWeapon.Shoot(Camera.main.transform);
        }
    }


    private void CheckReload()
    {
        if (Input.GetKey(ReloadKey))
        {
            CurrentWeapon.StartReload();
        }
    }

    private void SwitchWeapon()
    {
        if (Input.GetKeyDown(SwitchWeaponKey))
        {
            Debug.Log("Weapon Switched");

            if (PrimaryWeapon == CurrentWeapon && SecondaryWeapon!=null)
            {
                CurrentWeapon = SecondaryWeapon;
                PrimaryWeapon.gameObject.SetActive(false);
            }
         else if( SecondaryWeapon == CurrentWeapon && PrimaryWeapon!=null)
            {
                CurrentWeapon = PrimaryWeapon;
                SecondaryWeapon.gameObject.SetActive(false);
            }

            

            CurrentWeapon.gameObject.SetActive(true);
        }

       
    }

     public void Die()
    {
        SceneManager.LoadScene("Main Menu");
    }
 
}
