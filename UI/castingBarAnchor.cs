using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class castingBarAnchor : MonoBehaviour {

    public GameObject castBar;
    private Slider _CastBar;
    public UnityEngine.UI.Text CastBarText;

    void Start()
    {
        _CastBar = GetComponentInChildren<Slider>();
        CastBarText.text = "";
        castBar.SetActive(false);
    }

    public void UpdateCastBar2(string abilityname, float currentCD, float maxCD)
    {
        castBar.SetActive(true);
        if (abilityname.Equals(""))
        {
            CastBarText.text = "";
            _CastBar.value = 0;
            castBar.SetActive(false);
        }
        else
        { 
            CastBarText.text = abilityname + ": " + currentCD.ToString("f1") + "/" + maxCD;
            _CastBar.value = currentCD / maxCD * 100;
        }
    }

}
