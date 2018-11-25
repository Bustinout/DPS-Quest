using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class channelingBar : MonoBehaviour
{
    public GameObject channelBar;
    private Slider _channelBar;
    public UnityEngine.UI.Text channelBarText;

    void Start()
    {
        _channelBar = GetComponentInChildren<Slider>();
        channelBarText.text = "";
        channelBar.SetActive(false);
    }

    public void UpdateChannelBar(string abilityname, float currentChanenl, float maxChannel)
    {
        channelBar.SetActive(true);
        if (abilityname.Equals(""))
        {
            channelBarText.text = "";
            _channelBar.value = 0;
            channelBar.SetActive(false);
        }
        else
        {
            channelBarText.text = abilityname + ": " + currentChanenl.ToString("f1") + "/" + maxChannel;
            _channelBar.value = (1-(currentChanenl / maxChannel)) * 100;
        }
    }

}
