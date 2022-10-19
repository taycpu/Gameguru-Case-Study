using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CustomFeatures.Timer
{
    public class TimerProcessor : TaycpuSingleton<TimerProcessor>
    {
        [SerializeField] private List<Timer> timers = new List<Timer>();


        private void Update()
        {
            if (!timers.Any()) return;

            for (int x = 0; x < timers.Count; x++)
            {
                if (!timers[x].CalculateRemainingTime())
                {
                    timers.RemoveAt(x);
                }
            }
        }

        public void AddTimer(float time, Action act)
        {
            SoloTimer soloTimer = new SoloTimer();
            soloTimer.SetTimer(act, time);
            timers.Add(soloTimer);
        }

        public void AddTimer(Timer timer)
        {
            timers.Add(timer);
        }
    }
}