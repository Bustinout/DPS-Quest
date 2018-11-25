using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

    public UnityEngine.UI.Text itemInfo;
    public UnityEngine.UI.Text goldText;
    public UnityEngine.UI.Text costText;

    public Image gearIcon;
    public Image gearSprite;

    public item currentlySelectedItem;

    public bool inInventory;
    public int slotNumber;

    public int cost;

    public void ReturnToShop()
    {
        SaveLoad.Save();
        SceneManager.LoadScene("Shop");
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                ReturnToShop();
            }
        }
    }

    public void refresh()
    {
        if (currentlySelectedItem.rarity.Equals("Common"))
        {
            gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/common");
        }
        else if (currentlySelectedItem.rarity.Equals("Uncommon"))
        {
            gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/uncommon");
        }
        else if (currentlySelectedItem.rarity.Equals("Rare"))
        {
            gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/rare");
        }
        else if (currentlySelectedItem.rarity.Equals("Epic"))
        {
            gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/epic");
        }
        else if (currentlySelectedItem.rarity.Equals("Legendary"))
        {
            gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/legendary");
        }
        else
        {
            gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/ancient");
        }
        gearSprite.overrideSprite = Resources.Load<Sprite>(currentlySelectedItem.spriteLocation);
    }

    public void updateGoldText()
    {
        goldText.text = "Gold: " + CurrencyConverter.Instance.convertDamage(Game.current.player.gold);
    }

    public void replaceIcons(Sprite a, Sprite b)
    {
        gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/common");
        gearSprite.overrideSprite = b;
    }

    void Start()
    {
        updateGoldText();
        currentlySelectedItem = null;
        itemInfo.text = "Upgrade your armor... at a cost.";
    }

    public void updateCost()
    {
        if (currentlySelectedItem.upgradeLevel < 5)
        {
            cost = currentlySelectedItem.level * currentlySelectedItem.sell * (currentlySelectedItem.upgradeLevel + 1) * 3;
            costText.text = "Cost: " + CurrencyConverter.Instance.convertDamage(cost);
        }
        else
        {
            costText.text = "Cost: Fully Upgraded";
        }
    }

    public float[] upgradeChance = {0.2f, 0.4f, 0.6f, 0.8f, 0.9f};

    public void upgradeItem()
    {
        if (currentlySelectedItem != null)
        {

            if (Game.current.player.gold >= cost)
            {

                if (currentlySelectedItem.upgradeLevel < 5)
                {
                    float RNG = Random.value;
                    Game.current.player.gold = Game.current.player.gold - cost;
                    updateGoldText();
                    if (RNG > upgradeChance[currentlySelectedItem.upgradeLevel])
                    {

                        if (inInventory)
                        {
                            currentlySelectedItem.levelup();
                            Game.current.player.inventory[slotNumber] = currentlySelectedItem;
                            itemInfo.text = "Upgrade success!\n" + Game.current.player.inventory[slotNumber].itemInfoString();
                        }
                        else
                        {
                            currentlySelectedItem.levelup();
                            Game.current.player.equipped[slotNumber] = currentlySelectedItem;
                            itemInfo.text = "Upgrade success!\n" + Game.current.player.equipped[slotNumber].itemInfoString();
                        }
                        Game.current.player.successfulUpgrades += 1;
                        updateCost();
                    }
                    else
                    {
                        Game.current.player.failedUpgrades += 1;
                        itemInfo.text = "Upgrade failed.";
                    }
                }
                else
                {
                    itemInfo.text = "Cannot upgrade item any further.";
                    //error for max upgrade?
                }
                SaveLoad.Save();
            }
            else
            {
                itemInfo.text = "You don't have enough gold.";
            }
        }
        
    }

}
