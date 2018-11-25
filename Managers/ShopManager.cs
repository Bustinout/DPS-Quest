using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    public UnityEngine.UI.Text itemInfo;
    public UnityEngine.UI.Text equippedItemInfo;
    public UnityEngine.UI.Text shopDialogue;
    public UnityEngine.UI.Text Goldtext;
    public UnityEngine.UI.Text cost;

    public int identifyCost;
    public item currentlySelectedItem;

    public mysteryItem slot;

    void Start () {
        shopDialogue.text = "Welcome!";
        updateGoldText();
        identifyCost = (int) Mathf.Round(250 * Game.current.player.expMultiplier);
        cost.text ="Cost: " + CurrencyConverter.Instance.GetCurrencyToString(identifyCost);
        currentlySelectedItem = null;
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                ReturnToMenu();
            }
        }
    }

    public void updateGoldText()
    {
        Goldtext.text = "Gold: " + CurrencyConverter.Instance.GetCurrencyToString(Game.current.player.gold);
    }

    public void ReturnToMenu()
    {
        retrieveItem();
        SaveLoad.Save();
        SceneManager.LoadScene("Menu");
    }

    public void toUpgrade()
    {
        retrieveItem();
        SaveLoad.Save();
        SceneManager.LoadScene("Upgrade");
    }

    public void toCombine()
    {
        retrieveItem();
        SaveLoad.Save();
        SceneManager.LoadScene("Combine");
    }

    public void clearItemInfo()
    {
        itemInfo.text = "";
        equippedItemInfo.text = "";
    }

    public void swapItemInfo()
    {
        string temp = itemInfo.text;
        itemInfo.text = equippedItemInfo.text;
        equippedItemInfo.text = temp;
    }

    public void identifyItem()
    {
        if (currentlySelectedItem == null)
        {
            generateMystery(Game.current.player.level);
            slot.refresh();
            SaveLoad.Save();
        }
        else
        {
            shopDialogue.text = "Don't forget to take that item first!";
        }
    }

    public void equipItem()
    {
        if (currentlySelectedItem != null)
        {
            Game.current.player.equipItem(currentlySelectedItem);
            slot.gear = null;
            clearItemInfo();
            currentlySelectedItem = null;
            slot.refresh();
            shopDialogue.text = "";
            SaveLoad.Save();
        }
        else
        {
            shopDialogue.text = "Hey! Put the box down!";
        }
    }

    public void stashItem()
    {
        if (currentlySelectedItem != null)
        {
            Game.current.player.addToInventory(currentlySelectedItem);
            slot.gear = null;
            clearItemInfo();
            currentlySelectedItem = null;
            slot.refresh();
            SaveLoad.Save();
        }
        else
        {
            shopDialogue.text = "That's not yours to take.";
        }
    }

    public void sellItem()
    {
        if (currentlySelectedItem != null)
        {
            Game.current.player.gold += currentlySelectedItem.sell;
            Game.current.player.totalGold += currentlySelectedItem.sell;
            Game.current.player.goldFromSelling += currentlySelectedItem.sell;
            Game.current.player.itemsSold += 1;
            slot.gear = null;
            clearItemInfo();
            updateGoldText();
            currentlySelectedItem = null;
            slot.refresh();
            shopDialogue.text = "Thank you very much!";
            SaveLoad.Save();
        }
        else
        {
            shopDialogue.text = "What do you want me to sell?";
        }
    }

    public string[] commonDialogue = new string[] { "Better luck next time!", "I was hoping someone would take that...", "I would've gave you that for free.", "Bamboozled!", "Scammed!", "Welcome to Bolt.gg!", "What a sense of pride and accomplishment!"};
    public string[] uncommonDialogue = new string[] { "At least its not garbage.", "You could always try again!", "Welp. That sucks.", "Get that thing outta here!" };
    public string[] rareDialogue = new string[] { "Someone else ordered that last week!", "Guess it's yours now.", "So that's where I left that thing.", "Not too bad.", "Nice." };
    public string[] epicDialogue = new string[] { "Holy!", "Could I get that back?", "What a deal!", "What's that doing in those crates?", "Could you sell that back?" };
    public string[] legendaryDialogue = new string[] { "Woah! How did I forget that there?", "What the heck? I owned that?", "What? How?", "Wow, you are lucky."};
    public string[] ancientDialogue = new string[] { "Where... where did you find that?", "That item shouldn't be here...", "How could it be?", "Could it be?..." };

    public void retrieveItem()
    {
        if (currentlySelectedItem != null)
        {
            Game.current.player.addToInventory(currentlySelectedItem);
        }
    }

    public void generateMystery(int level)
    {
        if (Game.current.player.gold >= identifyCost)
        {
            string[] dialogues;
            Game.current.player.gold -= identifyCost;
            updateGoldText();
            float rareRNG = Random.value;

            if (rareRNG < .30)
            {
                dialogues = commonDialogue;
                currentlySelectedItem = (new item().generateCommonItem(level));
            }
            else if (rareRNG < .57)
            {
                dialogues = uncommonDialogue;
                currentlySelectedItem = (new item().generateUncommonItem(level));
            }
            else if (rareRNG < .81)
            {
                dialogues = rareDialogue;
                currentlySelectedItem = (new item().generateRareItem(level));
            }
            else if (rareRNG < .93)
            {
                dialogues = epicDialogue;
                currentlySelectedItem = (new item().generateEpicItem(level));
            }
            else if (rareRNG < .98)
            {
                dialogues = legendaryDialogue;
                currentlySelectedItem = (new item().generateLegendaryItem(level));
            }
            else
            {
                dialogues = ancientDialogue;
                currentlySelectedItem = (new item().generateAncientItem(level));
            }
            slot.gear = currentlySelectedItem;
            shopDialogue.text = dialogues[Random.Range(0, dialogues.Length)];
            itemInfo.text = "Identified Item:\n" + currentlySelectedItem.itemInfoString();
            if (Game.current.player.equipped[currentlySelectedItem.equipSlot] != null)
            {
                equippedItemInfo.text = "Equipped Item:\n" + Game.current.player.equipped[currentlySelectedItem.equipSlot].itemInfoString();
            }
            Game.current.player.mysteryItemsBought += 1;

            SaveLoad.Save();
        }
        else
        {
            shopDialogue.text = "Sorry. You don't have enough money...";
            //random multiple lines of dialogue?
        }
        
    }

}
