using System;
using UnityEngine;

/// <summary>
/// Handle events/converts/static variables in the project
/// </summary>
public static class Module
{
    public static float sound_fx = 1;
    public static float music_fx = 1;

    #region Event Delegate


    #endregion

    #region Random

    private static System.Random mRandom = new System.Random();

    public static int EasyRandom(int range)
    {
        return mRandom.Next(range);
    }

    public static int EasyRandom(int min, int max) //không bao gồm max
    {
        return mRandom.Next(min, max);
    }

    public static float EasyRandom(float min, float max)
    {
        return UnityEngine.Random.RandomRange(min, max);
    }

    #endregion

    #region Convert

    public static string TimestampNow()
    {
        return DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
    }

    // number to text
    public static string NumberCustomToString(float _number)
    {
        string str = "";
        if (_number < 10000)
            str = _number.ToString("00");
        else if (10000 <= _number && _number < 1000000)
            str = (_number / 1000).ToString("0.#") + "K";
        else if (1000000 <= _number && _number < 1000000000)
            str = (_number / 1000000).ToString("0.##") + "M";
        else
            str = (_number / 1000000000).ToString("0.##") + "B";
        return str;
    }

    //Convert time s => form
    public static string SecondCustomToTime(int _second)
    {
        string str = "";
        int second = 0;
        int minute = 0;
        int hour = 0;
        second = _second % 60;
        if (second > 59) second = 59;
        minute = (int)(Mathf.Floor(_second / 60) % 60);
        hour = (int)(_second / 3600);


        if (hour > 0)
            str += hour.ToString("00") + "h";

        if (minute >= 0)
            str += minute.ToString("00") + "m";

        if (_second < 3600)
            str += second.ToString("00") + "s";

        //str = hour.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00");
        return str;
    }

    public static string SecondCustomToTime2(int _second)
    {
        string str = "00:00";
        int second = 0;
        int minute = 0;
        int hour = 0;
        second = _second % 60;
        if (second > 59) second = 59;
        minute = (int)(Mathf.Floor(_second / 60) % 60);
        hour = (int)(_second / 3600);


        str = hour.ToString("00") + "h:" + minute.ToString("00") + "m" /*+ second.ToString("00")*/;
        return str;
    }

    #endregion


}

public delegate void Refresh();
