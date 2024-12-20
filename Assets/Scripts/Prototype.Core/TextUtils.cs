using System;
using System.Globalization;

namespace Prototype
{
    public static class TextUtils
    {
        public static string ValueToShortString(float numberOfIntems)
        {
            var thousand = numberOfIntems / 1000f;

            if (thousand >= 1 && thousand < 1000)
            {
                return $"{thousand.ToString("0.0")}K";
            }

            var milions = thousand / 1000f;

            if (milions >= 1 && milions < 1000)
            {
                return $"{milions.ToString("0.0")}M";
            }

            var bilions = milions / 1000f;

            if (bilions >= 1)
            {
                return $"{bilions.ToString("0.0")}B";
            }

            return numberOfIntems.ToString("0.0");
        }


        public static string SplitBy3Number(float value)
        {
            return value.ToString("N0", new NumberFormatInfo()
            {
                NumberGroupSizes = new[] { 3 },
                NumberGroupSeparator = " "
            });
        }

        public static string TimeFormat(TimeSpan time)
        {
            if (time.Hours != 0)
            {
                return $"{time.Hours}h {time.Minutes}m ";
            }
            else if (time.Minutes != 0)
            {
                return $"{time.Minutes}m {time.Seconds}s ";
            }
            else if (time.Seconds != 0)
            {
                return $"{time.Seconds}s ";
            }

            return "0s";
        }
    }
}
