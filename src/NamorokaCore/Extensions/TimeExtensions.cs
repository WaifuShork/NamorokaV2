﻿using System;

namespace NamorokaV2.NamorokaCore.Extensions
{
    public static class TimeExtensions
    {
        public static string ToTimeString(this DateTimeOffset dt) => dt.ToString("HH:mm:ss");
        public static string ToDateString(this DateTimeOffset dt) => dt.ToString("dddd, dd.MM.yyyy");
        public static string ToShortDateString(this DateTimeOffset dt) => dt.ToString("dd.MM.yyyy");
    }
}