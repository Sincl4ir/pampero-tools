using System;

namespace Pampero.Tools.TimeUtils
{
    public class CountdownTimer : Timer
    {
        private float _tickTimer = 1f;
        public Action OnTimerTicked = delegate { };

        public CountdownTimer(float value) : base(value) { }

        public override void Tick(float deltaTime)
        {
            if (!IsRunning || Time < 0) { return; }

            Time -= deltaTime;
            _tickTimer -= deltaTime;

            if (_tickTimer <= 0)
            {
                OnTimerTicked?.Invoke();
                _tickTimer = 1f;
            }

            if (Time <= 0)
            {
                Stop();
            }
        }

        public bool IsFinished => Time <= 0;

        public void Reset() => Time = initialTime;

        public void Reset(float newTime)
        {
            initialTime = newTime;
            Reset();
        }
    }
}
//EOF.