using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class monsterCard : MonoBehaviour {

    public UnityEngine.UI.Text monsterNameText;
    public UnityEngine.UI.Text monsterInfoText;
    public UnityEngine.UI.Text zoneText;
    public UnityEngine.UI.Text subzoneText;
    public UnityEngine.UI.Text pageNumberText;

    public UnityEngine.UI.Image monsterImage;

    public string[] zones;
    public string[] subzones0;
    public string[] subzones1;
    public string[] subzones2;
    public string[] subzones3;
    public string[] subzones4;
    public string[] subzones5;

    public string[] currentSubzone;

    public int zoneIndex;
    public int subzoneIndex;
    public int pageIndex;

    public string[] monsterNames;
    public string[] monsterInfo;
    public string[] monsterSprites;

    // Use this for initialization
    void Start () {
        zoneIndex = 0;
        subzoneIndex = 0;
        pageIndex = 0;

        writeArrays();
    }

    public void writeArrays()
    {
        zones = new string[] { "Mysterious Island", "Desert Continent", "Mountain Continent", "Lava Continent", "Deap Sea", "Crashed Ship" };
        subzones0 = new string[] { "Island Shore", "Deep Jungle", "Temple Outskirts", "Ancient Temple" };
        subzones1 = new string[] { "Beach", "Desert", "Oasis", "Heart of the Oasis" };
        subzones2 = new string[] { "Forest", "Cursed Woods", "Mountain", "Mountain Peak" };
        subzones3 = new string[] { "Savage Savanna", "Molten River", "Pillar of Origin", "The Stone Throne" };
        subzones4 = new string[] { "Crystal Reef", "Sunken Ship", "Dark Depths", "Lair of the Monster" };
        subzones5 = new string[] { "Crumbling Entrance", "The Armory", "Hall of the Council", "Ship Core" };

        monsterNames = new string[200];
        monsterInfo = new string[200];
        monsterSprites = new string[200];

        monsterNames[0] = "Boar";
        monsterInfo[0] = "\tThese boars wander around the island in search for those brave enough to relieve them of their boardom.";
        monsterSprites[0] = "Sprites/EnemyModels/boar";


        refresh();
    }

    public void refresh()
    {
        setCurrentSubzone();

        if (Game.current.player.zoneUnlock[zoneIndex * 4])
        {
            zoneText.text = zones[zoneIndex];
            if(Game.current.player.zoneUnlock[(zoneIndex * 4) + subzoneIndex])
            {
                subzoneText.text = currentSubzone[subzoneIndex];
                if (Game.current.player.monsterCardUnlock[(((zoneIndex * 4) + subzoneIndex)*5) + pageIndex])
                {
                    setCard((((zoneIndex * 4) + subzoneIndex) * 5) + pageIndex);
                }
                else
                {
                    monsterUnknown();
                }

            }
            else
            {
                monsterUnknown();
                subzoneText.text = "???";
            }
        }
        else
        {
            monsterUnknown();
            zoneText.text = "???";
            subzoneText.text = "???";
        }
        pageNumberText.text =(pageIndex+1) + "/5";
    }

    public void setCard(int x)
    {
        monsterNameText.text = monsterNames[x];
        monsterInfoText.text = monsterInfo[x];
        monsterImage.overrideSprite = Resources.Load<Sprite>(monsterSprites[x]);
    }

    public void monsterUnknown()
    {
        monsterNameText.text = "???";
        monsterInfoText.text = "\tMonster not yet discovered.";
        monsterImage.overrideSprite = Resources.Load<Sprite>("Sprites/EnemyModels/unknown");
    }

    public void setCurrentSubzone()
    {
        if (zoneIndex == 0)
        {
            currentSubzone = subzones0;
        }
        else if (zoneIndex == 1)
        {
            currentSubzone = subzones1;
        }
        else if (zoneIndex == 2)
        {
            currentSubzone = subzones2;
        }
        else if (zoneIndex == 3)
        {
            currentSubzone = subzones3;
        }
        else if (zoneIndex == 4)
        {
            currentSubzone = subzones4;
        }
        else if (zoneIndex == 5)
        {
            currentSubzone = subzones5;
        }
    }

    public void nextPage()
    {
        pageIndex = (pageIndex + 1) % 5;
        refresh();
    }

    public void prevPage()
    {
        pageIndex = (pageIndex + 5 - 1) % 5;
        refresh();
    }

    public void nextZone()
    {
        zoneIndex = (zoneIndex + 1) % zones.Length;
        refresh();
    }

    public void prevZone()
    {
        zoneIndex = (zoneIndex + zones.Length - 1) % zones.Length;
        refresh();
    }
	
    public void nextSubzone()
    {
        subzoneIndex = (subzoneIndex + 1) % 4;
        refresh();
    }

    public void prevSubzone()
    {
        subzoneIndex = (subzoneIndex + 4 - 1) % 4;
        refresh();
    }

}
