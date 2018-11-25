using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gearSlotButton : MonoBehaviour {

    public int slotNumber;
    public Image gearIcon;
    public Image gearSprite;
    public item gear;
    public UnityEngine.UI.Text itemInfoText;
    public UnityEngine.UI.Text equippedText;
    

    public CharacterManager CM;
    public UpgradeManager UM;

    public void refresh()
    {
        if (Game.current.player.equipped[slotNumber] != null)
        {
            gear = Game.current.player.equipped[slotNumber];
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
        else
        {
            gear = null;
            gearIcon.overrideSprite = Resources.Load<Sprite>("Sprites/UI/emptyslot");
            gearSprite.overrideSprite = Resources.Load<Sprite>("Sprites/UI/transparent");
        }
    }

    public void refreshSelect(CharacterSave selectedCharacter)
    {
        if (selectedCharacter.equipped[slotNumber] != null)
        {
            gear = selectedCharacter.equipped[slotNumber];
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
        else
        {
            gear = null;
            gearIcon.overrideSprite = Resources.Load<Sprite>("Sprites/UI/emptyslot");
            gearSprite.overrideSprite = Resources.Load<Sprite>("Sprites/UI/transparent");
        }
    }


    void Start()
    {
        refresh();
    }

    public void gearClicked()
    {
        if (gear != null)
        {
            itemInfoText.text = gear.itemInfoString();
            CM.currentlySelectedItem = gear;
            CM.currentlySelectedSlot = slotNumber;
            CM.EquipButton.SetActive(true);
        }
        else
        {
            itemInfoText.text = "";
            CM.EquipButton.SetActive(false);
        }
    }

    public void upgradeClicked()
    {
        refresh();
        if (gear != null)
        {
            itemInfoText.text = gear.itemInfoString();
            itemInfoText.text = "Selected Item:\n" + gear.itemInfoString();
            UM.currentlySelectedItem = gear;
            UM.refresh();
            UM.inInventory = false;
            UM.slotNumber = slotNumber;
            UM.updateCost();
        }
        else
        {
            
        }
    }

}
