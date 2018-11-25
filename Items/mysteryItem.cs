using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mysteryItem : MonoBehaviour {

    public Image gearIcon;
    public Image gearSprite;
    public item gear;
    public UnityEngine.UI.Text itemInfoText;

    public ShopManager SM;

    void Start()
    {
        gear = null;
    }

    public void refresh()
    {
        if (gear != null) { 
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
            gearSprite.overrideSprite = Resources.Load<Sprite>("Sprites/UI/mysteryIcon");
        }
    }

    public void itemClicked()
    {
        if (gear == null)
        {
            itemInfoText.text = "Mystery Item:\n"+ "\n???";
        }
        else
        {
            itemInfoText.text = "Identified Item:\n" + gear.itemInfoString();
        }
    }

}
