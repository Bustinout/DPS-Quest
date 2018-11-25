using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class statistics : MonoBehaviour {

    public GameObject BattleStats;
    public GameObject ShopStats;
    public GameObject OtherStats;

    public UnityEngine.UI.Text battleNumbersText;
    public UnityEngine.UI.Text shopNumbersText;
    public UnityEngine.UI.Text otherNumbersText;
    public UnityEngine.UI.Text pageNumberText;

    public int currentIndex;

    // Use this for initialization
    void Start() {
        setBattleStats();
        setShopStats();
        setOtherStats();

        currentIndex = 0;
        refresh();
    }

    public void setBattleStats()
    {
        battleNumbersText.text =
            Game.current.player.monsterKills.ToString() + "\n" +
            Game.current.player.bossKills.ToString() + "\n" +
            Game.current.player.deaths.ToString() + "\n" +
            Game.current.player.flees.ToString() + "\n" +
            CurrencyConverter.Instance.convertDamage(Game.current.player.highestPeakDPS) + "\n" +
            CurrencyConverter.Instance.convertDamage(Game.current.player.highestDPS) + "\n" +
            CurrencyConverter.Instance.convertDamage(Game.current.player.totalDamageDone) + "\n" +
            CurrencyConverter.Instance.convertDamage(Game.current.player.totalDamageTaken);
    }

    public void setShopStats()
    {
        shopNumbersText.text =
            CurrencyConverter.Instance.GetCurrencyToString(Game.current.player.totalGold) + "\n" +
            CurrencyConverter.Instance.GetCurrencyToString(Game.current.player.goldFromSelling) + "\n" +
            CurrencyConverter.Instance.GetCurrencyToString(Game.current.player.goldFromMonsters) + "\n" +
            Game.current.player.itemsSold.ToString() + "\n" +
            Game.current.player.mysteryItemsBought.ToString() + "\n" +
            Game.current.player.successfulUpgrades.ToString() + "\n" +
            Game.current.player.failedUpgrades.ToString() + "\n" +
            Game.current.player.itemCombines.ToString();
    }

    public void setOtherStats()
    {
        otherNumbersText.text =
            CurrencyConverter.Instance.convertTime(Game.current.player.timeSpent) + "\n" +
            CurrencyConverter.Instance.convertTime(Game.current.player.timeSpentBattling);

    }

    public void refresh()
    {
        pageNumberText.text = (currentIndex + 1) + "/3";

        if (currentIndex == 0)
        {
            BattleStats.SetActive(true);
            ShopStats.SetActive(false);
            OtherStats.SetActive(false);
        }
        else if (currentIndex == 1)
        {
            BattleStats.SetActive(false);
            ShopStats.SetActive(true);
            OtherStats.SetActive(false);
        }
        else
        {
            BattleStats.SetActive(false);
            ShopStats.SetActive(false);
            OtherStats.SetActive(true);
        }
    }

    public void nextPage()
    {
        currentIndex = (currentIndex + 1) % 3;
        refresh();
    }

    public void prevPage()
    {
        currentIndex = (currentIndex + 3 - 1) % 3;
        refresh();
    }

}
