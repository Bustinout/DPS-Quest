using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expBar : MonoBehaviour {

    public Slider _expBar;
    private float counter;
    private float trickle;
    public Button menuButton;
    public Button battleAgain;
    public UnityEngine.UI.Text lowLvl;
    public UnityEngine.UI.Text highLvl;
    public UnityEngine.UI.Text expText;

    private float expGain;

    void Start()
    {
        _expBar.value = Game.current.player.exp / Game.current.player.maxexp * 100;
        expGain = PlayerPrefs.GetFloat("expGain");

        if (expGain > 0)
        {
            trickle = expGain / 20;
            counter = 0f;
            lowLvl.text = Game.current.player.level.ToString();
            highLvl.text = (Game.current.player.level + 1).ToString();
            StartCoroutine("EXPBARTRICKLE");
        }
        else
        {
            lowLvl.text = Game.current.player.level.ToString();
            highLvl.text = (Game.current.player.level + 1).ToString();
            expText.text = "EXP: " + Game.current.player.exp.ToString("f0") + "/" + Game.current.player.maxexp.ToString("f0");
            SaveLoad.Save();
            menuButton.enabled = true;
            battleAgain.enabled = true;
        }
    }

    void addExpToBar(float gain)
    {
        Game.current.player.exp += trickle;
        if (Game.current.player.exp >= Game.current.player.maxexp)
        {
            Game.current.player.levelUp(); //handles exp-maxexp
            lowLvl.text = Game.current.player.level.ToString();
            highLvl.text = (Game.current.player.level + 1).ToString();
        }
        _expBar.value = Game.current.player.exp / Game.current.player.maxexp * 100;
    }

    IEnumerator EXPBARTRICKLE()
    {
        while (true)
        {
            if (counter < 1)
            {

                addExpToBar(trickle);
                expText.text = "EXP: " + Game.current.player.exp.ToString("f0") + "/" + Game.current.player.maxexp.ToString("f0") + " (+" + expGain + ")";
                counter += 0.05f;
            }
            else
            {
                PlayerPrefs.SetFloat("expGain", 0);
                menuButton.enabled = true;
                battleAgain.enabled = true;
                SaveLoad.Save();
                StopCoroutine("EXPBARTRICKLE");
            }
            yield return new WaitForSeconds(0.025f);
        }
    }

}
