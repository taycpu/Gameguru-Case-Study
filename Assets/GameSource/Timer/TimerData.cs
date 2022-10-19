using System;

namespace CustomFeatures.Timer
{
    struct TimerData
    {
        public Action act;
        public float time;

        public TimerData(Action act, float time)
        {
            this.act = act;
            this.time = time;
        }
    }
}