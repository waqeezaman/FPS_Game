using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    class AbilityUI
    {

        static public IEnumerator CountdownDisplay( bool Available, float CooldownTime, Text CountdownText)
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
