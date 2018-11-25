using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyConverter : MonoBehaviour {

    private static CurrencyConverter instance;
    public static CurrencyConverter Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        CreateInstance();
    }

    void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public string GetCurrencyToString(float valueToConvert)
    {
        string converted;
        if (valueToConvert >= 1000000)
        {
            converted = (valueToConvert / 1000000f).ToString("f2") + "M";

        }
        else if (valueToConvert >= 100000)
        {
            converted = (valueToConvert / 1000f).ToString("f2") + "K";
        }
        else
        {
            converted = (valueToConvert).ToString("f0");
        }
        return converted;
    }

    public string convertDamage(float valueToConvert) //same as HP for now
    {
        string converted;

        if (valueToConvert >= 1000000)
        {
            converted = (valueToConvert / 1000000f).ToString("f2") + "M";

        }
        if (valueToConvert >= 10000)
        {
            converted = (valueToConvert / 1000f).ToString("f2") + "K";
        }
        else
        {
            converted = (valueToConvert).ToString("f0");
        }

        return converted;
    }
    
    public string convertTime(float valueToConvert)
    {
        string converted = "";
        float temp = valueToConvert;

        if (temp >= 3600)
        {
            converted += (valueToConvert / 3600).ToString("f0") + "H ";
            temp = temp % 3600;
        }
        else
        {
            converted += "0H ";
        }
        if (temp >= 60)
        {
            converted += (valueToConvert / 60).ToString("f0") + "M ";
            temp = temp % 60;
        }
        else
        {
            converted += "0M ";
        }
        converted += (temp).ToString("f0") + "S";

        return converted;
    }
}
