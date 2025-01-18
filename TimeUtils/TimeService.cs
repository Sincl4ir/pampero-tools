using System;
using UnityEngine;
using Pampero.Utils;

namespace Pampero.TimeSystem
{
    public class TimeService
    {
        private readonly TimeSettings _settings;
        private DateTime _currentTime;
        private readonly TimeSpan _sunriseTime;
        private readonly TimeSpan _sunsetTime;

        public DateTime CurrentTime => _currentTime;

        public event Action OnSunrise = delegate { };
        public event Action OnSunset = delegate { };
        public event Action OnHourChange = delegate { };

        private readonly Observable<bool> _isDayTime;
        private readonly Observable<int> _currentHour;

        public TimeService(TimeSettings settings)
        {
            _settings = settings;
            _currentTime = DateTime.Now.Date + TimeSpan.FromHours(settings.startHour);
            _sunriseTime = TimeSpan.FromHours(settings.sunriseHour);
            _sunsetTime = TimeSpan.FromHours(settings.sunsetHour);

            _isDayTime = new Observable<bool>(IsDayTime());
            _currentHour = new Observable<int>(_currentTime.Hour);

            _isDayTime.ValueChanged += day => (day ? OnSunrise : OnSunset)?.Invoke();
            _currentHour.ValueChanged += _ => OnHourChange?.Invoke();
        }

        public void UpdateTime(float deltaTime)
        {
            _currentTime = _currentTime.AddSeconds(deltaTime * _settings.timeMultiplier);
            _isDayTime.Value = IsDayTime();
            _currentHour.Value = _currentTime.Hour;
        }

        public float CalculateSunAngle()
        {
            bool isDay = IsDayTime();
            float startDegree = isDay ? 0 : 180;
            TimeSpan start = isDay ? _sunriseTime : _sunsetTime;
            TimeSpan end = isDay ? _sunsetTime : _sunriseTime;

            TimeSpan totalTime = CalculateDifference(start, end);
            TimeSpan elapsedTime = CalculateDifference(start, _currentTime.TimeOfDay);

            double percentage = elapsedTime.TotalMinutes / totalTime.TotalMinutes;
            return Mathf.Lerp(startDegree, startDegree + 180, (float)percentage);
        }

        // This method checks whether the current game time falls within the daytime period.
        // It returns true if the current time of day is later than sunriseTime and earlier than sunsetTime,
        // representing daytime. Otherwise, it returns false, indicating it is nighttime.
        bool IsDayTime() => _currentTime.TimeOfDay > _sunriseTime && _currentTime.TimeOfDay < _sunsetTime;

        // This method calculates the difference between two TimeSpan objects ("from" and "to").
        // If the calculated difference is negative, this indicates that the "from" time is ahead of the "to" time.
        // In such cases, 24 hours (representing a full day) is added to the negative difference to calculate the actual
        // time difference taking into account the next day.    
        TimeSpan CalculateDifference(TimeSpan from, TimeSpan to)
        {
            TimeSpan difference = to - from;
            return difference.TotalHours < 0 ? difference + TimeSpan.FromHours(24) : difference;
        }
    }
}
//EOF.