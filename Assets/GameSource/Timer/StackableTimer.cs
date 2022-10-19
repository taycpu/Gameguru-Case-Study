using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomFeatures.Timer
{
    public class StackableTimer : Timer
    {
        private Queue<TimerData> timerDatas = new Queue<TimerData>();

        public override void SetTimer(Action act, float time)
        {
            if (OnTiming)
            {
                timerDatas.Enqueue(new TimerData(act, time));
                return;
            }

            SetTimerLocal(act, time);
        }

        protected override void KillTiming()
        {
            base.KillTiming();

            if (!timerDatas.Any()) return;
            TimerData tData = timerDatas.Dequeue();
            SetTimer(tData.act, tData.time);
        }
    }
}