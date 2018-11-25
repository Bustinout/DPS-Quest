using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    public UnityEngine.UI.Text itemInfo;
    public UnityEngine.UI.Text equippedItemInfo;
    public UnityEngine.UI.Text GoldText;

    public item currentlySelectedItem;
    public int currentlySelectedSlot;

    public bool itemSelected; //bug fix

    public InventoryItem slot0;
    public InventoryItem slot1;
    public InventoryItem slot2;
    public InventoryItem slot3;
    public InventoryItem slot4;
    public InventoryItem slot5;
    public InventoryItem slot6;
    public InventoryItem slot7;
    public InventoryItem slot8;
    public InventoryItem slot9;
    public InventoryItem slot10;
    public InventoryItem slot11;
    public InventoryItem slot12;
    public InventoryItem slot13;
    public InventoryItem slot14;
    public InventoryItem slot15;
    public InventoryItem slot16;
    public InventoryItem slot17;
    public InventoryItem slot18;
    public InventoryItem slot19;
    public InventoryItem slot20;
    public InventoryItem slot21;
    public InventoryItem slot22;
    public InventoryItem slot23;
    public InventoryItem slot24;
    public InventoryItem slot25;
    public InventoryItem slot26;
    public InventoryItem slot27;
    public InventoryItem slot28;
    public InventoryItem slot29;
    public InventoryItem slot30;
    public InventoryItem slot31;
    public InventoryItem slot32;
    public InventoryItem slot33;
    public InventoryItem slot34;
    public InventoryItem slot35;
    public InventoryItem slot36;
    public InventoryItem slot37;
    public InventoryItem slot38;
    public InventoryItem slot39;

    void Start()
    {
        GoldText.text = "Gold: " + CurrencyConverter.Instance.GetCurrencyToString(Game.current.player.gold);
        itemSelected = false;
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

    public void refreshAll()
    {
        slot0.updateSprites();
        slot1.updateSprites();
        slot2.updateSprites();
        slot3.updateSprites();
        slot4.updateSprites();
        slot5.updateSprites();
        slot6.updateSprites();
        slot7.updateSprites();
        slot8.updateSprites();
        slot9.updateSprites();
        slot10.updateSprites();
        slot11.updateSprites();
        slot12.updateSprites();
        slot13.updateSprites();
        slot14.updateSprites();
        slot15.updateSprites();
        slot16.updateSprites();
        slot17.updateSprites();
        slot18.updateSprites();
        slot19.updateSprites();
        slot20.updateSprites();
        slot21.updateSprites();
        slot22.updateSprites();
        slot23.updateSprites();
        slot24.updateSprites();
        slot25.updateSprites();
        slot26.updateSprites();
        slot27.updateSprites();
        slot28.updateSprites();
        slot29.updateSprites();
        slot30.updateSprites();
        slot31.updateSprites();
        slot32.updateSprites();
        slot33.updateSprites();
        slot34.updateSprites();
        slot35.updateSprites();
        slot36.updateSprites();
        slot37.updateSprites();
        slot38.updateSprites();
        slot39.updateSprites();
    }

    public void ReturnToMenu()
    {
        SaveLoad.Save();
        SceneManager.LoadScene("Menu");
    }

    public void toCharacter()
    {
        SaveLoad.Save();
        SceneManager.LoadScene("Character");
    }


    public void clearItemInfo()
    {
        itemInfo.text = "";
        equippedItemInfo.text = "";
    }

    public void EquipItem()
    {
        if (itemSelected == true)
        {
            if (Game.current.player.inventory[currentlySelectedSlot] != null)
            {
                //for when nothing equipped
                int tempSlot = currentlySelectedItem.equipSlot;

                item temp = Game.current.player.equipped[currentlySelectedItem.equipSlot];
                Game.current.player.equipped[currentlySelectedItem.equipSlot] = currentlySelectedItem;
                Game.current.player.inventory[currentlySelectedSlot] = temp;



                currentlySelectedItem = temp;
                refreshAll();

                if (currentlySelectedItem != null)
                {
                    if (Game.current.player.equipped[currentlySelectedItem.equipSlot] != null)
                    {
                        itemInfo.text = "Selected Item:\n" + temp.itemInfoString();
                        equippedItemInfo.text = "Currently Equipped:\n" + Game.current.player.equipped[temp.equipSlot].itemInfoString();
                    }
                    else
                    {
                        clearItemInfo();
                    }
                }
                else
                {
                    itemInfo.text = "Selected Item:\n";
                    equippedItemInfo.text = "Currently Equipped:\n" + Game.current.player.equipped[tempSlot].itemInfoString();
                }
            }
            SaveLoad.Save();
        }
    }

    public void SellItem()
    {
        if (Game.current.player.inventory[currentlySelectedSlot] != null)
        {
            Game.current.player.gold = Game.current.player.gold + currentlySelectedItem.sell;
            Game.current.player.totalGold += currentlySelectedItem.sell;
            Game.current.player.goldFromSelling += currentlySelectedItem.sell;
            Game.current.player.itemsSold += 1;
            Game.current.player.inventory[currentlySelectedSlot] = null;
            clearItemInfo();
            GoldText.text = "Gold: " + CurrencyConverter.Instance.convertDamage(Game.current.player.gold);
            currentlySelectedItem = null;
            currentlySelectedSlot = 99;
            refreshAll();
        }
        SaveLoad.Save();
    }

}
