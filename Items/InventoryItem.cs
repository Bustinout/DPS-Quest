using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour {

    public int slotNumber;
    public Image gearIcon;
    public Image gearSprite;
    public item gear;
    public UnityEngine.UI.Text itemInfoText;
    public UnityEngine.UI.Text equippedText;


    public InventoryManager ESM;
    public CombineManager CM;
    public UpgradeManager UM;

    void Start()
    {
        updateSprites();
    }

    public void updateSprites()
    {
        if (Game.current.player.inventory[slotNumber] == null)
        {
            gearIcon.overrideSprite = Resources.Load<Sprite>("Sprites/UI/emptyslot");
            gearSprite.overrideSprite = Resources.Load<Sprite>("Sprites/UI/transparent");
            gear = null;
        }
        else
        {
            gear = Game.current.player.inventory[slotNumber];
            if (gear.rarity.Equals("Common"))
            {
                gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/common");
            }
            else if (gear.rarity.Equals("Uncommon"))
            {
                gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/uncommon");
            }
            else if (gear.rarity.Equals("Rare"))
            {
                gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/rare");
            }
            else if (gear.rarity.Equals("Epic"))
            {
                gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/epic");
            }
            else if (gear.rarity.Equals("Legendary"))
            {
                gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/legendary");
            }
            else
            {
                gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/ancient");
            }
            gearSprite.overrideSprite = Resources.Load<Sprite>(gear.spriteLocation);
        }
    }

    public void gearClicked()
    {
        if (gear != null)
        {
            if (Game.current.player.equipped[gear.equipSlot] != null)
            {
                equippedText.text = "Currently Equipped:\n" + Game.current.player.equipped[gear.equipSlot].itemInfoString();
            }
            else
            {
                equippedText.text = "";
            }
            itemInfoText.text = "Selected Item:\n" + gear.itemInfoString();
            ESM.currentlySelectedItem = gear;
            ESM.currentlySelectedSlot = slotNumber;
            ESM.itemSelected = true;
        }
        else
        {
            itemInfoText.text = "";
            equippedText.text = "";
            ESM.currentlySelectedItem = null;
            ESM.currentlySelectedSlot = 99;
            ESM.itemSelected = false;
        }
    }

    public void combineClicked()
    {
        if (gear != null)
        {
            if (Game.current.player.equipped[gear.equipSlot] != null)
            {
                equippedText.text = "Currently Equipped:\n" + Game.current.player.equipped[gear.equipSlot].itemInfoString();
            }
            else
            {
                equippedText.text = "";
            }
            itemInfoText.text = "Selected Item:\n" + gear.itemInfoString();

            if (CM.gotSpace)
            {
                CM.addToCombine(gear);
                gear = null;
                Game.current.player.inventory[slotNumber] = null;
                updateSprites();
            }
            else
            {
                //no space
            }
        }
        else
        {
            itemInfoText.text = "";
            equippedText.text = "";
        }
    }

    public void upgradeClicked()
    {
        updateSprites();
        if (gear != null)
        {
            itemInfoText.text = gear.itemInfoString();
            itemInfoText.text = "Selected Item:\n" + gear.itemInfoString();
            
            UM.currentlySelectedItem = gear;
            UM.refresh();
            UM.inInventory = true;
            UM.slotNumber = slotNumber;
            UM.updateCost();
        }
        else
        {
            
        }
    }
}
