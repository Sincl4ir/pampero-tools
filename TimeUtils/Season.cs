using System;

namespace Pampero.Tools.TimeUtils
{
    [Flags]
    public enum Season
    {
        None = 0,       // No season
        Spring = 1 << 0, // 1 (binary 0001)
        Summer = 1 << 1, // 2 (binary 0010)
        Fall = 1 << 2,   // 4 (binary 0100)
        Winter = 1 << 3, // 8 (binary 1000)
        All = Spring | Summer | Fall | Winter // Combines all seasons
    }
}
//EOF.