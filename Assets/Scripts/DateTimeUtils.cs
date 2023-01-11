using System;
using System.Globalization;
using UnityEngine;

public static class DateTimeUtils
{
    public static void SetDateTime(string key, DateTime value)
    {
        string convertedToString = value.ToString("u",CultureInfo.InvariantCulture);
        PlayerPrefs.SetString(key,convertedToString);
    }

    public static DateTime GetDateTime(string key, DateTime value)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string stored = PlayerPrefs.GetString(key);
            DateTime result = DateTime.ParseExact(stored, "u", CultureInfo.InvariantCulture);
            return result;
        }
        else
        {
            return value;
        }
    }
}
