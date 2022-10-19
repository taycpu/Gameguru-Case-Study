using System;
using UnityEngine;

namespace CustomFeatures.Timer
{
    public class SoloTimer : Timer
    {
        public override void SetTimer(Action act, float time)
        {
            if (OnTiming)
            {
                Debug.Log("ON TIMING");
                return;
            }

            SetTimerLocal(act, time);
        }
    }
}