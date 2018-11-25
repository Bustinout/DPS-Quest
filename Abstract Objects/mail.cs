using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class mail{
    public string title;
    public string message;

    public item gift;
    public float gold;

    public mail()
    {

    }

    public mail(string title1, string message1, item item1, float gold1)
    {
        this.title = title1;
        this.message = message1;
        this.gift = item1;
        this.gold = gold1;
    }


    






}
