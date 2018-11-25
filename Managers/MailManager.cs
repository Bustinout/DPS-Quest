using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailManager : MonoBehaviour {

    public MenuManager MM;

    public UnityEngine.UI.Text TitleText;
    public UnityEngine.UI.Text MessageText;
    public UnityEngine.UI.Text itemInfoText;
    public UnityEngine.UI.Text equippedText;
    public UnityEngine.UI.Text GoldText;
    public UnityEngine.UI.Text PageNumberText;

    public Image gearIcon;
    public Image gearSprite;
    public GameObject popup;
    public GameObject itemInfo;
    public GameObject hideInfo;

    public int[] mailIndexes;
    public int currentIndex;

    public item giftItem;

    void Start()
    {
        startStuff();
    }

    public void startStuff() //was crashing on startup
    {
        currentIndex = 0;
        getMailIndexes();
        if (mailIndexes.Length != 0)
        {
            refresh();
        }
    }

    public void refresh()
    {
        TitleText.text = Game.current.player.mailbox[mailIndexes[currentIndex]].title;
        MessageText.text = Game.current.player.mailbox[mailIndexes[currentIndex]].message;
        GoldText.text = "Gold: " + CurrencyConverter.Instance.GetCurrencyToString(Game.current.player.mailbox[mailIndexes[currentIndex]].gold);
        PageNumberText.text = (currentIndex+1) + "/" + mailIndexes.Length;
        itemInfo.SetActive(false);
        if (Game.current.player.mailbox[mailIndexes[currentIndex]].gift != null)
        {
            giftItem = Game.current.player.mailbox[mailIndexes[currentIndex]].gift;
        }
        updateSprites();
    }

    public void updateSprites()
    {
        if (giftItem == null)
        {
            gearIcon.overrideSprite = Resources.Load<Sprite>("Sprites/UI/emptyslot");
            gearSprite.overrideSprite = Resources.Load<Sprite>("Sprites/UI/transparent");
        }
        else
        {
            if (giftItem.rarity.Equals("Common"))
            {
                gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/common");
            }
            else if (giftItem.rarity.Equals("Uncommon"))
            {
                gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/uncommon");
            }
            else if (giftItem.rarity.Equals("Rare"))
            {
                gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/rare");
            }
            else if (giftItem.rarity.Equals("Epic"))
            {
                gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/epic");
            }
            else if (giftItem.rarity.Equals("Legendary"))
            {
                gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/legendary");
            }
            else
            {
                gearIcon.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/ancient");
            }
            gearSprite.overrideSprite = Resources.Load<Sprite>(giftItem.spriteLocation);
        }
    }

    public void getMailIndexes()
    {
        List<int> indexList = new List<int>();

        for (int x = 0; x < Game.current.player.mailbox.Length; x++)
        {
            if (Game.current.player.mailbox[x] != null)
            {
                indexList.Add(x);
            }
        }

        mailIndexes = indexList.ToArray();
    }

    public void itemClicked()
    {
        if (giftItem != null)
        {
            itemInfo.SetActive(true);
            if (Game.current.player.equipped[giftItem.equipSlot] != null)
            {
                equippedText.text = "Currently Equipped:\n" + Game.current.player.equipped[giftItem.equipSlot].itemInfoString();
            }
            itemInfoText.text = "Selected Item:\n" + giftItem.itemInfoString();
            hideInfo.SetActive(true);
        }
    }

    public void hideInfoButton()
    {
        itemInfo.SetActive(false);
        hideInfo.SetActive(false);
    }


    public void openPopup()
    {
        popup.SetActive(true);
    }

    public void closePopup()
    {
        popup.SetActive(false);
    }

    public void ClaimMail()
    {
        mail temp = Game.current.player.mailbox[mailIndexes[currentIndex]];
        if (temp.gift != null)
        {
            if (Game.current.player.InventoryFull())
            {
                MM.UpdateErrorText("You're inventory is full.");
            }
            else
            {
                Game.current.player.gold += temp.gold;
                Game.current.player.addToInventory(temp.gift);
                Game.current.player.mailbox[mailIndexes[currentIndex]] = null;
                afterRemove();
            }
        }
        else
        {
            Game.current.player.gold += temp.gold;
            Game.current.player.mailbox[mailIndexes[currentIndex]] = null;
            afterRemove();
        }
    }

    public void DeleteMail()
    {
        mail temp = Game.current.player.mailbox[mailIndexes[currentIndex]];
        Game.current.player.gold += temp.gold;
        Game.current.player.gold += temp.gift.sell;
        Game.current.player.mailbox[mailIndexes[currentIndex]] = null;
        popup.SetActive(false);

        afterRemove();
    }

    public void afterRemove()
    {
        getMailIndexes();
        if (mailIndexes.Length == 0)
        {
            Game.current.player.youveGotMail = false;
            MM.mailIcon.SetActive(false);
            MM.closeMail();
            
        }
        else
        {
            if (currentIndex == mailIndexes.Length)
            {
                currentIndex -= 1;
            }
            refresh();
        }
        SaveLoad.Save();
    }

    public void nextMail()
    {
        if (mailIndexes.Length != 1)
        {
            currentIndex = (currentIndex + 1) % mailIndexes.Length;
            refresh();
        }
    }

    public void prevMail()
    {
        if (mailIndexes.Length != 1)
        {
            currentIndex = (currentIndex + mailIndexes.Length- 1) % mailIndexes.Length;
            refresh();
        }
    }
}
