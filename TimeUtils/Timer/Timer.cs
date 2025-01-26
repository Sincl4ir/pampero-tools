using System;

namespace Pampero.Tools.TimeUtils
{
    public abstract class Timer
    {
        protected float initialTime;
        public float Time { get; set; }
        public bool IsRunning { get; protected set; }

        public float Progress => Time / initialTime;

        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate { };

        protected Timer(float value)
        {
            initialTime = value;
            IsRunning = false;
        }

        public void Start()
        {
            Time = initialTime;
            if (IsRunning) { return; }

            IsRunning = true;
            OnTimerStart.Invoke();
        }

        public void Stop()
        {
            if (!IsRunning) { return; }

            IsRunning = false;
            OnTimerStop.Invoke();
        }

        public void Resume() => IsRunning = true;
        public void Pause() => IsRunning = false;

        public abstract void Tick(float deltaTime);
    }
}
//EOF.