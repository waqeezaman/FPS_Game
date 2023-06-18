using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Slider ReloadSlider;  //rename

    [SerializeField]
    private Text AmmoText;

    [SerializeField]
    private PlayerController Player;

    [SerializeField]
    private CharacterFunctions PlayerFunctions;

    [SerializeField]
    private Slider HealthBar;
    void Start()
    {
        if (Player == null)
        {
            Player = transform.root.GetComponent<PlayerController>();
        }

        if (PlayerFunctions == null)
        {
            PlayerFunctions = transform.root.GetComponent<CharacterFunctions>();
        }

        HealthBar.minValue = 0;
        HealthBar.maxValue = PlayerFunctions.MaxHealth;


        ReloadSlider.minValue = 0;
        ReloadSlider.maxValue = Player.CurrentWeapon.ReloadTime;

        ReloadSlider.gameObject .SetActive ( false);

      

    }


    public void UpdateAmmoText(int MagazineSize,int RoundsInClip)
    {
        AmmoText.text = $"{RoundsInClip}/{MagazineSize}";
    }

    public IEnumerator ActivateReloadSlider(float ReloadTime)
    {
        float elapsedTime=0f;
        ReloadSlider.gameObject.SetActive(true);

        while (elapsedTime <ReloadTime )
        {
            elapsedTime += Time.deltaTime;
            ReloadSlider.value = elapsedTime;
            yield return null;
        }
        CloseReloadSlider();
    }

    public void CloseReloadSlider()
    {
        ReloadSlider.value = ReloadSlider.minValue;
        ReloadSlider.gameObject.SetActive(false);
    }

    public void WeaponSwitch()
    {
        ReloadSlider.minValue = 0;
        ReloadSlider.maxValue = Player.CurrentWeapon.ReloadTime;

        UpdateAmmoText(Player.CurrentWeapon.MagazineSize, Player.CurrentWeapon.RoundsInClip);
    }

    private void Update()
    {
        UpdateHealthBar();
    }
    public void UpdateHealthBar()
    {
        HealthBar.value = PlayerFunctions.Health;
    }
}
