using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHitmarker : MonoBehaviour
{

    public Texture Crosshair;
    public Texture Hitmarker;
    public float Duration;

    public Color HitMarkerColour;
    private Color CrossHairColour;

    private RawImage Image;
    void Start()
    {
        Image = GetComponent<RawImage>();
        Image.texture = Crosshair;

         CrossHairColour=Image.color ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void  ShowHitMarker()
    {
        StartCoroutine(CoRoutineShowHitmarker());
    }
   private IEnumerator CoRoutineShowHitmarker()
    {
        Image.texture = Hitmarker;
        Image.color = HitMarkerColour ;
        yield return new WaitForSeconds (Duration);
     
        Image.texture = Crosshair;
        Image.color = CrossHairColour;

    }
}
