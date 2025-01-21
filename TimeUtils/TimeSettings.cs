using UnityEngine;

namespace Pampero.Tools.TimeUtils
{
    [CreateAssetMenu(fileName = "TimeSettings", menuName = "Pampero/SO/TimeSettings")]
    public class TimeSettings : ScriptableObject
    {
        [field: SerializeField] public Season StartingSeason { get; private set; }
        [field: SerializeField] public int DaysPerSeason { get; private set; }
        [field: SerializeField] public float TimeMultiplier {get; private set;}
        [field: SerializeField] public float StartHour { get; private set; }
        [field: SerializeField] public float SunriseHour { get; private set; }
        [field: SerializeField] public float SunsetHour { get; private set;}
    }
}
//EOF.