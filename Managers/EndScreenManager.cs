using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour {

    public UnityEngine.UI.Text GoldText;
    public UnityEngine.UI.Text damageDone;
    public UnityEngine.UI.Text enemyDamageDone;
    public UnityEngine.UI.Text DPS;
    public UnityEngine.UI.Text enemyDPS;
    public UnityEngine.UI.Text timeText;
    public UnityEngine.UI.Text battleOutcomeText;

    public UnityEngine.UI.Text itemInfo;
    public UnityEngine.UI.Text equippedItemInfo;

    public item currentlySelectedItem;
    public int currentlySelectedSlot;

    public bool somethingSelected;

    public LootItem slot0;
    public LootItem slot1;
    public LootItem slot2;
    public LootItem slot3;
    public LootItem slot4;
    public LootItem slot5;

    public bool win;

    public float goldStart;
    public float goldGain; 
    public float goldEnd;
    public float goldTrickle;

    public float counter;
    public bool CRisRunning = false;

    public GameObject bossButton;

    private void Start()
    {
        somethingSelected = false;
        Game.current.player.clearLoot();
        if (PlayerPrefs.GetInt("battlewon") == 1)
        {
            win = true;
        }
        else
        {
            win = false;
        }
        PlayerPrefs.SetInt("battlewon", 0);

        counter = 0f;

        
        damageDone.text = PlayerPrefs.GetString("damageDone");
        enemyDamageDone.text = PlayerPrefs.GetString("enemyDamageDone");
        DPS.text = PlayerPrefs.GetString("DPS");
        enemyDPS.text = PlayerPrefs.GetString("enemyDPS");


        goldStart = Game.current.player.gold;
        goldGain = PlayerPrefs.GetFloat("goldGain");

        timeText.text = "Time: " + PlayerPrefs.GetString("timeElapsed") + "s";
        GoldText.text = "Gold: " + CurrencyConverter.Instance.GetCurrencyToString(goldStart);

        

        if (win)
        {
            battleOutcomeText.text = "Victory!";
            goldEnd = goldStart + goldGain;

            addLootToTable(PlayerPrefs.GetInt("EnemyLevel"), PlayerPrefs.GetString("EnemyType"));
            refreshAll();

            goldTrickle = goldGain / 20;

            Game.current.player.gold += goldGain;
            Game.current.player.goldFromMonsters += goldGain;
            Game.current.player.totalGold += goldGain;

            PlayerPrefs.SetFloat("goldGain", 0);

            addZoneProgress();


            StartCoroutine("EXPGOLDTRICKLE");

        }
        else
        {
            battleOutcomeText.text = "Defeat";
        }

        addDPSToStatistics();

        SaveLoad.Save();

        if (Game.current.player.zoneProgress[
           (PlayerPrefs.GetInt("LastPage")*4) + PlayerPrefs.GetInt("Zone")
            ] >= 100)
        {
            bossButton.SetActive(true);
        }
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                StashReturnToMenu();
            }
        }
    }

    public void toBossBattle()
    {
        PlayerPrefs.SetInt("Battle", 4);
        SceneManager.LoadScene("Battle");
    }

    public void addDPSToStatistics()
    {
        float tempDPS = PlayerPrefs.GetFloat("EndDPS");
        if (tempDPS > Game.current.player.highestDPS)
        {
            Game.current.player.highestDPS = tempDPS;
        }
        Game.current.player.totalDamageDone += PlayerPrefs.GetFloat("EndPlayerDamage");
        Game.current.player.totalDamageTaken += PlayerPrefs.GetFloat("EndEnemyDamage");
        Game.current.player.timeSpentBattling += PlayerPrefs.GetFloat("EndTimeElapsed");
    }

    public void addZoneProgress() 
    {
        int zone = PlayerPrefs.GetInt("Zone");
        int battle = PlayerPrefs.GetInt("Battle");

        if (battle == 4) // boss defeated
        {
            Game.current.player.zoneProgress[zone] = 0;
            Game.current.player.zoneCleared[zone] = true;
            if (zone != 23) //last zone
            {
                Game.current.player.zoneUnlock[zone + 1] = true;
            }
            Game.current.player.bossKills += 1; //BOSSKILL STATS
        }
        else
        {
            if (Game.current.player.zoneCleared[zone]) //when zone is already cleared = more progress
            {
                Game.current.player.zoneProgress[zone] = Mathf.Min(Game.current.player.zoneProgress[zone]+10, 100);
            }
            else
            {
                Game.current.player.zoneProgress[zone] = Mathf.Min(Game.current.player.zoneProgress[zone] + 5, 100);
            }
        }
        Game.current.player.monsterKills += 1; //MONSTERKILL STATS
        
    }

    public void addLootToTable(int level, string type)
    {
        //Game.current.player.addToLoot(new item().generateCommonItem(level));
        if (type.Equals("Normal"))
        {
            generateItemForNormal(level);
            generateItemForNormal(level);
            float rareRNG = Random.value;
            if (rareRNG > .5)
            {
                generateItemForNormal(level);
            }
        }
        else if (type.Equals("Rare"))
        {
            generateItemForRare(level);
            generateItemForRare(level);
            generateItemForRare(level);
            generateItemForRare(level);
            float rareRNG = Random.value;
            if (rareRNG > .5)
            {
                generateItemForRare(level);
            }
        }
        else
        {
            generateItemForRare(level);
            generateItemForRare(level);
            generateItemForRare(level);
            generateItemForRare(level);
            generateItemForRare(level);
            generateItemForRare(level);
        }
    }

    public void generateItemForNormal(int level)
    {
        float rareRNG = Random.value;

        if (rareRNG < .70)
        {
            Game.current.player.addToLoot(new item().generateCommonItem(level));
        }
        else if (rareRNG < .85)
        {
            Game.current.player.addToLoot(new item().generateUncommonItem(level));
        }
        else if (rareRNG < .94)
        {
            Game.current.player.addToLoot(new item().generateRareItem(level));
        }
        else if (rareRNG < .97)
        {
            Game.current.player.addToLoot(new item().generateEpicItem(level));
        }
        else if (rareRNG < .995)
        {
            Game.current.player.addToLoot(new item().generateLegendaryItem(level));
        }
        else
        {
            Game.current.player.addToLoot(new item().generateAncientItem(level));
        }
    }

    // 30      27        24    12     5         2
    public void generateItemForRare(int level)
    {
        float rareRNG = Random.value;

        if (rareRNG < .40)
        {
            Game.current.player.addToLoot(new item().generateCommonItem(level));
        }
        else if (rareRNG < .70)
        {
            Game.current.player.addToLoot(new item().generateUncommonItem(level));
        }
        else if (rareRNG < .84)
        {
            Game.current.player.addToLoot(new item().generateRareItem(level));
        }
        else if (rareRNG < .93)
        {
            Game.current.player.addToLoot(new item().generateEpicItem(level));
        }
        else if (rareRNG < .98)
        {
            Game.current.player.addToLoot(new item().generateLegendaryItem(level));
        }
        else
        {
            Game.current.player.addToLoot(new item().generateAncientItem(level));
        }
    }

    public void StashReturnToMenu()
    {
        sellAll2();

        StopCoroutine("EXPGOLDTRICKLE");
        SaveLoad.Save();
        SceneManager.LoadScene("Menu");
    }

    public void BattleAgain()
    {
        sellAll2();

        StopCoroutine("EXPGOLDTRICKLE");
        SaveLoad.Save();

        randomBattle();
        SceneManager.LoadScene("Battle");
    }

    public void randomBattle()
    {
        float rand = Random.value;

        if (rand < .3)
        {
            PlayerPrefs.SetInt("Battle", 0);
        }
        else if (rand < .6)
        {
            PlayerPrefs.SetInt("Battle", 1);
        }
        else if (rand < .9)
        {
            PlayerPrefs.SetInt("Battle", 2);
        }
        else
        {
            PlayerPrefs.SetInt("Battle", 3);
        }
    }

    public void sellAll2()
    {
        foreach (item x in Game.current.player.loot)
        {
            if (x != null)
            {
                Game.current.player.gold += x.sell;
                Game.current.player.totalGold += x.sell;
                Game.current.player.goldFromSelling += x.sell;
                Game.current.player.itemsSold += 1;
            }
        }
        Game.current.player.clearLoot();
        SaveLoad.Save();
    }


    public void stashAll()
    {
        foreach (item x in Game.current.player.loot)
        {
            if (x != null)
            {
                Game.current.player.addToInventory(x);
            }
        }
        Game.current.player.clearLoot();
        clearItemInfo();
        refreshAll();
        SaveLoad.Save();

        somethingSelected = false;
    }

    
    IEnumerator EXPGOLDTRICKLE()
    {
        CRisRunning = true;
        while (true)
        {
            if (counter < 1)
            {
                goldStart += goldTrickle;

                GoldText.text = "Gold: " + CurrencyConverter.Instance.GetCurrencyToString(goldStart) + " (+" + CurrencyConverter.Instance.GetCurrencyToString(goldGain) + ")";

                counter += 0.05f;
            }
            else
            {
                CRisRunning = false;
                StopCoroutine("EXPGOLDTRICKLE");
            }
            yield return new WaitForSeconds(0.025f);
        }
    }

    public void refreshAll()
    {
        slot0.updateSprites();
        slot1.updateSprites();
        slot2.updateSprites();
        slot3.updateSprites();
        slot4.updateSprites();
        slot5.updateSprites();
    }

    public void clearItemInfo()
    {
        itemInfo.text = "";
        equippedItemInfo.text = "";
    }

    public void EquipItem()
    {
        if (somethingSelected)
        {
            if (Game.current.player.loot[currentlySelectedSlot] != null)
            {
                Game.current.player.equipItem(currentlySelectedItem);
                Game.current.player.loot[currentlySelectedSlot] = null;
                clearItemInfo();
                currentlySelectedItem = null;
                currentlySelectedSlot = 99;
                refreshAll();
            }
            somethingSelected = false;
        }
        
    }
    public void SellItem()
    {
        if (somethingSelected)
        {
            if (Game.current.player.loot[currentlySelectedSlot] != null)
            {

                Game.current.player.gold += currentlySelectedItem.sell;
                Game.current.player.goldFromSelling += currentlySelectedItem.sell;
                Game.current.player.totalGold += currentlySelectedItem.sell;
                Game.current.player.itemsSold += 1;
                Game.current.player.loot[currentlySelectedSlot] = null;
                clearItemInfo();
                goldGain += currentlySelectedItem.sell;
                GoldText.text = "Gold: " + CurrencyConverter.Instance.GetCurrencyToString(Game.current.player.gold) + " (+" + CurrencyConverter.Instance.GetCurrencyToString(goldGain) + ")";
                currentlySelectedItem = null;
                currentlySelectedSlot = 99;
                refreshAll();
            }
            somethingSelected = false;
        }
        
    }
    public void StashItem()
    {
        if (somethingSelected)
        {
            if (Game.current.player.loot[currentlySelectedSlot] != null)
            {
                Game.current.player.addToInventory(currentlySelectedItem);
                Game.current.player.loot[currentlySelectedSlot] = null;
                clearItemInfo();
                currentlySelectedItem = null;
                currentlySelectedSlot = 99;
                refreshAll();
            }
            somethingSelected = false;
        }
        
    }
}
