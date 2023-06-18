using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
  public  class Ability:MonoBehaviour 
    {

      public  bool Available;
    public   float CooldownTime;
           public Text CountdownText;
         public IEnumerator CountdownDisplay()
        {
            float CurrentCoolDown;

            Available = false;
            CurrentCoolDown = CooldownTime;

            while (CurrentCoolDown > 0)
            {
                CurrentCoolDown -= Time.deltaTime;
                CountdownText.text = Mathf.CeilToInt(CurrentCoolDown).ToString();
                yield return null;
            }


            CountdownText.text = "";
            Available = true;

        }

       
    }
}
