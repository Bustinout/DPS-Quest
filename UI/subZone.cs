using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class subZone : MonoBehaviour {

    public UnityEngine.UI.Text zoneProgress;
    public UnityEngine.UI.Text subzoneName;
    public GameObject bossButton;
    public GameObject notUnlocked;
    public Slider progressBar;

    public BattleSelectManager BSM;

    public int zoneNumber;

    // Use this for initialization
    void Start () {
		if (Game.current.player.zoneUnlock[zoneNumber])
        {
            if (Game.current.player.zoneProgress[zoneNumber] >= 100)
            {
                bossButton.SetActive(true);
                progressBar.value = 100;
                zoneProgress.text = "Boss has appeared!";
            }
            else
            {
                progressBar.value = Game.current.player.zoneProgress[zoneNumber];
                zoneProgress.text = Game.current.player.zoneProgress[zoneNumber] + "%";
            }
            if (Game.current.player.zoneCleared[zoneNumber])
            {
                zoneProgress.text += " (Cleared)";
            }
        }
        else
        {
            zoneProgress.text = "???";
            subzoneName.text = "???";
            notUnlocked.SetActive(true);
        }
	}

    public void toBattle()
    {
        BSM.selectedZone = zoneNumber;
        BSM.toBattle();
    }

    public void toBoss()
    {
        BSM.selectedZone = zoneNumber;
        BSM.toBossBattle();
    }

}
