using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombineManager : MonoBehaviour {

    public UnityEngine.UI.Text itemInfo;
    public UnityEngine.UI.Text equippedItemInfo;

    public int combineCount;
    public bool gotSpace;

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

    public combineItem c0;
    public combineItem c1;
    public combineItem c2;
    public combineItem c3;
    public combineItem c4;


    // Use this for initialization
    void Start()
    {
        gotSpace = true;
        combineCount = 0;
        refreshAll();
        itemInfo.text = "Combine five pieces of equipment of the same rarity into one of the next!";
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

    public void refreshCombine()
    {
        c0.updateSprites();
        c1.updateSprites();
        c2.updateSprites();
        c3.updateSprites();
        c4.updateSprites();
    }

    public void clearCombine()
    {
        c1.gear = null;
        c2.gear = null;
        c3.gear = null;
        c4.gear = null;
        combineCount = 1;
        gotSpace = true;
    }

    public void combineItems()
    {
        if (gotSpace == false)
        {
            string rarity = c0.gear.rarity;
            if (rarity.Equals(c1.gear.rarity) & rarity.Equals(c2.gear.rarity) & rarity.Equals(c3.gear.rarity) & rarity.Equals(c4.gear.rarity))
            {
                int levelofItem = (int) Mathf.Round((c0.gear.level + c1.gear.level + c2.gear.level + c3.gear.level + c4.gear.level)/5);

                if (rarity.Equals("Common"))
                {
                    c0.gear = new item().generateUncommonItem(levelofItem);
                    clearCombine();
                }
                else if (rarity.Equals("Uncommon"))
                {
                    c0.gear = new item().generateRareItem(levelofItem);
                    clearCombine();
                }
                else if (rarity.Equals("Rare"))
                {
                    c0.gear = new item().generateEpicItem(levelofItem);
                    clearCombine();
                }
                else if (rarity.Equals("Epic"))
                {
                    c0.gear = new item().generateLegendaryItem(levelofItem);
                    clearCombine();
                }
                else if (rarity.Equals("Legendary"))
                {
                    c0.gear = new item().generateAncientItem(levelofItem);
                    clearCombine();
                }
                else
                {
                    itemInfo.text = "These pieces are already too powerful.";
                }

                itemInfo.text = "Selected Item:\n" + c0.gear.itemInfoString();
                if (Game.current.player.equipped[c0.gear.equipSlot] != null)
                {
                    equippedItemInfo.text = "Currently Equipped:\n" + Game.current.player.equipped[c0.gear.equipSlot].itemInfoString();
                    clearCombine();
                }
                Game.current.player.itemCombines += 1;
                refreshCombine();

            }
            else
            {
                itemInfo.text = "All pieces need to be of the same rarity.";
                equippedItemInfo.text = "";
            }
            SaveLoad.Save();
        }
        else
        {
            itemInfo.text = "You need five pieces of equipment in here.";
            equippedItemInfo.text = "";
        }
    }

    public void addToCombine(item addItem)
    {
        if (c0.gear == null)
        {
            c0.gear = addItem;
            combineCount += 1;
            c0.updateSprites();
        }
        else if (c1.gear == null)
        {
            c1.gear = addItem;
            combineCount += 1;
            c1.updateSprites();
        }
        else if (c2.gear == null)
        {
            c2.gear = addItem;
            combineCount += 1;
            c2.updateSprites();
        }
        else if (c3.gear == null)
        {
            c3.gear = addItem;
            combineCount += 1;
            c3.updateSprites();
        }
        else if (c4.gear == null)
        {
            c4.gear = addItem;
            combineCount += 1;
            c4.updateSprites();
        }
        else
        {
            //combine full / shouldnt ever be reached
        }

        if (combineCount == 5)
        {
            gotSpace = false;
        }
    }

    public void retrieveItems()
    {
        if (c0.gear != null)
        {
            Game.current.player.addToInventory(c0.gear);
        }
        if (c1.gear != null)
        {
            Game.current.player.addToInventory(c1.gear);
        }
        if (c2.gear != null)
        {
            Game.current.player.addToInventory(c2.gear);
        }
        if (c3.gear != null)
        {
            Game.current.player.addToInventory(c3.gear);
        }
        if (c4.gear != null)
        {
            Game.current.player.addToInventory(c4.gear);
        }
    }

    public void ReturnToShop()
    {
        retrieveItems();
        SaveLoad.Save();
        SceneManager.LoadScene("Shop");
    }
}
