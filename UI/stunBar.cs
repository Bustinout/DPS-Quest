using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stunBar : MonoBehaviour {

    private Slider _StunBar;
    public UnityEngine.UI.Text StunBarText;

    void Start()
    {
        _StunBar = GetComponentInChildren<Slider>();
        StunBarText.text = "";
    }

    public void UpdateStunBar(float timeStunned, float stunDuration)
    {
        StunBarText.text = "Stunned " + timeStunned.ToString("f1") + "/" + stunDuration.ToString("f1");
        _StunBar.value = timeStunned / stunDuration * 100;
    }

    public void clearStunBar()
    {
        StunBarText.text = "";
        _StunBar.value = 0;
    }
}
