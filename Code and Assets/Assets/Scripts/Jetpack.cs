using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour
{


    public float Force;
    public float MaxFuel;
    public float FuelUseRate;
    public float FuelRechargeRate;
    
    private float Fuel;
    public Slider JetpackSlider;

    private Rigidbody PlayerBody;

    public float RechargeDelay;

 
    private bool Recharging;

    private ParticleSystem Particles;
    // Start is called before the first frame update
    void Start()
    {

        if (JetpackSlider == null)
        {
            GameObject.Find("Jetpack Slider").GetComponent<Slider>();
        }
        JetpackSlider.minValue = 0;
        JetpackSlider.maxValue = MaxFuel;

        PlayerBody = transform.root.GetComponent<Rigidbody>();

        Particles = GetComponent<ParticleSystem>();
        Particles.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Boost();
        SetJetpackSlider();
    }

    private void Boost()
    {
        
        if (!Recharging  && Input.GetMouseButton(1) && Fuel>0 )
        {

           
         PlayerBody.velocity = new Vector3(PlayerBody.velocity.x, 0, PlayerBody.velocity.z);
            

         PlayerBody.useGravity = false;

         PlayerBody.AddForce(new Vector3(0,Force*Time.deltaTime ,0));
         Fuel -= FuelUseRate * Time.deltaTime;
          
                Particles.enableEmission = true;    
            
        }
        else
        {
           
            Particles.enableEmission   =false;
          
            if (!Recharging && Fuel<=0f)
            {
                StartCoroutine(StartRechargeDelay());
            }
            Fuel = Mathf.Clamp(Fuel + FuelRechargeRate * Time.deltaTime, 0, MaxFuel);
            PlayerBody.useGravity = true;
        }

     
    }

    private void  SetJetpackSlider()
    {
        JetpackSlider.value = Fuel;
    }

    private IEnumerator StartRechargeDelay()
    {
        Recharging = true;
        
        yield return new WaitForSeconds (RechargeDelay );
        Recharging = false;

    }
}
