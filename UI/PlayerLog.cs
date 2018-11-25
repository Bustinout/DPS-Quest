using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLog : MonoBehaviour {

    //creds: https://answers.unity.com/questions/508268/i-want-to-create-an-in-game-log-which-will-print-a.html
    //"kevin002"

    private List<string> EventLog = new List<string>();
    private string guiText = "";
    public UnityEngine.UI.Text playerLog;

    public int maxLines;

    public void AddEvent(string eventString)
    {
        EventLog.Add(eventString);

        if (EventLog.Count >= maxLines)
        {
            EventLog.RemoveAt(0);
        }

        guiText = "";

        foreach (string logEvent in EventLog)
        {
            guiText += logEvent;
            guiText += "\n";
        }

        playerLog.text = guiText;
    }
}
