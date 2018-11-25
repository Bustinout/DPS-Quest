using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class combineItem : MonoBehaviour {

    public Image gearIcon;
    public Image gearSprite;
    public item gear;
    public UnityEngine.UI.Text itemInfoText;
    public UnityEngine.UI.Text equippedText;

    public CombineManager CM;
    

    public void updateSprites()
    {
        if (gear == null)
        {
            gearIcon.overrideSprite = Resources.Load<Sprite>("Sprites/UI/emptyslot");
            gearSprite.overrideSprite = Resources.Load<Sprite>("Sprites/UI/transparent");
            gear = null;
        }
        else
        {
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

            Game.current.player.addToInventory(gear);
            CM.combineCount -= 1;
            CM.gotSpace = true;
            gear = null;
            CM.refreshAll();
            updateSprites();
        }
        else
        {
            itemInfoText.text = "";
            equippedText.text = "";
        }
    }

    // Use this for initialization
    void Start () {
        gear = null;
        updateSprites();
        
    }
	

}
