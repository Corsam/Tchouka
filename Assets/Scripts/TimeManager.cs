using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public string TimeDisplay(float time, bool displayCents)
    {
        string retVal = "";

        int min = (int)time / 60;
        int sec = (int)(time - min * 60);
        int cent = (int)((time - min * 60 - sec) * 100);

        string secText = "";

        if (sec < 10)
        {
            secText += "0";
        }

        secText += sec.ToString();

        retVal = min + ":" + secText;

        if (displayCents)
        {
            retVal += "." + cent;
        }

        return retVal;
    }
}
