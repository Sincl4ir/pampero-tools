using System;
using UnityEngine;
using Pampero.Utils;

namespace Pampero.Tools.TimeUtils
{
    public class TimeService
    {
        private Season[] _orderedSeasons = { Season.Spring, Season.Summer, Season.Fall, Season.Winter };

        private readonly TimeSettings _settings;
        private DateTime _currentTime;
        private readonly TimeSpan _sunriseTime;
        private readonly TimeSpan _sunsetTime;
        private readonly Observable<bool> _isDayTime;
        private readonly Observable<int> _currentHour;
        private int startingSeasonIndex;

        public event Action OnSunrise = delegate { };
        public event Action OnSunset = delegate { };
        public event Action OnHourChanged = delegate { };
        public event Action OnDayChanged = delegate { };
        public event Action OnSeasonChanged = delegate { };

        public DateTime CurrentTime => _currentTime;
        public int ElapsedDays { get; private set; }
        public Season CurrentSeason { get; private set; }

        public TimeService(TimeSettings settings)
        {
            _settings = settings;
            _currentTime = DateTime.Now.Date + TimeSpan.FromHours(settings.StartHour);
            _sunriseTime = TimeSpan.FromHours(settings.SunriseHour);
            _sunsetTime = TimeSpan.FromHours(settings.SunsetHour);

            _isDayTime = new Observable<bool>(IsDayTime());
            _currentHour = new Observable<int>(_currentTime.Hour);

            ElapsedDays = 0;
            CurrentSeason = _settings.StartingSeason;

            _isDayTime.ValueChanged += day => (day ? OnSunrise : OnSunset)?.Invoke();
            _currentHour.ValueChanged += _ => OnHourChanged?.Invoke();
            SetStartingSeason();
        }

        private void SetStartingSeason()
        {
            // Find the index of the starting season in the ordered seasons array.
            startingSeasonIndex = Array.IndexOf(_orderedSeasons, _settings.StartingSeason);

            if (startingSeasonIndex == -1)
            {
                Debug.LogWarning("Invalid Starting Season set in TimeSettings. Defaulting to Spring.");
                startingSeasonIndex = 0; // Default to Spring if invalid.
            }

            CurrentSeason = _orderedSeasons[startingSeasonIndex];
        }

        public void UpdateTime(float deltaTime)
        {
            var previousDay = _currentTime.Day;
            _currentTime = _currentTime.AddSeconds(deltaTime * _settings.TimeMultiplier);
            _isDayTime.Value = IsDayTime();
            _currentHour.Value = _currentTime.Hour;
            EvaluateDayProgression(previousDay);
        }

        private void EvaluateDayProgression(int previousDay)
        {
            if (_currentTime.Day == previousDay) { return; }

            ElapsedDays++;
            OnDayChanged?.Invoke();
            EvaluateSeasonProgression(); 
        }

        private void EvaluateSeasonProgression()
        {
            // Calculate the index of the current season with the offset for the starting season.
            int seasonIndex = (startingSeasonIndex + (ElapsedDays / _settings.DaysPerSeason)) % _orderedSeasons.Length;
            if (_orderedSeasons[seasonIndex].Equals(CurrentSeason)) { return; }

            // Update the current season using the calculated index.
            CurrentSeason = _orderedSeasons[seasonIndex];
            OnSeasonChanged?.Invoke();
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

        public int GetElapsedDaysFromTimeSpan(DateTime startDateTime)
        {
            // Calculate the difference in days between the current time and the start time
            TimeSpan timeDifference = _currentTime - startDateTime;
            return (int)timeDifference.TotalDays;
        }
    }
}
//EOF.