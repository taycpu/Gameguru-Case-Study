using System;
using UnityEngine;

namespace CustomFeatures.Timer
{
    [Serializable]
    public class Timer
    {
        private bool onTiming;
        private float timer;
        private float currentTime;
        private Action timerEvent;

        public bool OnTiming
        {
            get => onTiming;
        }


        public void SetTimer()
        {
            currentTime = timer;
            onTiming = true;
        }

        public void SetTimer(Action act)
        {
            timerEvent = act;
        }

        public void SetTimer(float time)
        {
            currentTime = timer;
            onTiming = true;
        }

        public virtual void SetTimer(Action act, float time)
        {
            SetTimerLocal(act, time);
        }

        protected void SetTimerLocal(Action act, float time)
        {
            timer = time;
            currentTime = timer;
            timerEvent = act;
            onTiming = true;
        }


        public bool CalculateRemainingTime()
        {
            bool onTime = false;
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                onTime = true;
            }
            else if (currentTime <= 0)
            {
                KillTiming();
            }

            return onTime;
        }

        protected virtual void KillTiming()
        {
            timerEvent?.Invoke();
            onTiming = false;
        }
    }
}