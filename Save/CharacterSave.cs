using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterSave {

    public string characterName;
    public string className;
    public int classID;
    // 0 - blademaster 1 - pyromancer
    public string raceName;
    public string spriteLocation;
    public float gold;
    public float exp;
    public float maxexp;
    public float expMultiplier;
    public int level;
    public float hp; //base hp
    public float power;
    public int[] spellList;
    public int[] actionBars;

    public item[] equipped;
    public item[] inventory;
    public item[] loot;
    public mail[] mailbox;
    public bool youveGotMail;

    public string primaryStat;

    //stats
    public int primaryStatValue;
    public int stamina;
    public int armor;
    public int dodge;
    public int magicres;
    public int lifesteal;
    public int crit;
    public int bonuspower;
    public int cdr;
    public int damage;

    //journal stuff
    public bool[] zoneUnlock;
    public int[] zoneProgress;
    public bool[] zoneCleared;
    public bool[] journalEntryUnlock;
    public bool[] monsterCardUnlock;

    //battle stats
    public int monsterKills; //added in EndScreen
    public int bossKills; //added in EndScreen
    public int deaths; //added in Character
    public int flees; //added in BattleManager
    public float highestPeakDPS; //added in DPS
    public float highestDPS; //added in EndScreen
    public float totalDamageDone; //added in EndScreen
    public float totalDamageTaken; //added in EndScreen

    //shop stats
    public float totalGold; //added anywhere with selling/monsters
    public float goldFromSelling; //added in Endscreen and Inventory sell and Shop (mystery)
    public float goldFromMonsters; //added in Endscreen
    public int itemsSold; //same as Selling ^
    public int mysteryItemsBought; //hurr durr
    public int successfulUpgrades; //^
    public int failedUpgrades; //^
    public int itemCombines; //^

    //others
    public float timeSpent; //done in saveload 
    public float timeSpentBattling; //added in EndScreen

    public CharacterSave()
    {

    }

    public CharacterSave(string characterName, string characterClass, string characterRace, string characterSprite)
    {
        this.characterName = characterName;
        this.className = characterClass;
        this.raceName = characterRace;
        this.spriteLocation = "Sprites/PlayerModels/player";//characterSprite;
        this.gold = 0;
        this.exp = 0;
        this.maxexp = 1000;
        this.expMultiplier = 1;
        this.level = 1;
        this.hp = 100;
        this.spellList = new int[90];
        this.actionBars = new int[12];
        this.equipped = new item[11];
        this.inventory = new item[40];
        this.mailbox = new mail[100];
        this.youveGotMail = false;
        this.loot = new item[6];
        this.zoneUnlock = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false }; //24
        this.zoneProgress = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}; //24
        this.zoneCleared = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false }; //24
        this.journalEntryUnlock = new bool[200];
        this.monsterCardUnlock = new bool[200];

        this.primaryStatValue = 10;
        this.stamina = 10;
        this.armor = 0;
        this.dodge = 0;
        this.magicres = 0;
        this.lifesteal = 0;
        this.crit = 0;
        this.bonuspower = 0;
        this.cdr = 0;
        this.damage = 0;

        //statistics
        this.monsterKills = 0;
        this.bossKills = 0;
        this.deaths = 0;
        this.flees = 0;
        this.highestDPS = 0;
        this.totalDamageDone = 0;
        this.totalDamageTaken = 0;

        this.totalGold = 0;
        this.goldFromSelling = 0;
        this.goldFromMonsters = 0;
        this.itemsSold = 0;
        this.mysteryItemsBought = 0;
        this.successfulUpgrades = 0;
        this.failedUpgrades = 0;
        this.itemCombines = 0;

        this.timeSpent = 0;
        this.timeSpentBattling = 0;


        if (characterRace.Equals("Primalist"))
        {
            this.actionBars[6] = 452;
            this.spellList[0] = 452;

            if (characterClass.Equals("Pyromancer"))
            {
                this.primaryStat = "Intellect";
                this.classID = 1;
            }
            else if (characterClass.Equals("Earth Warden"))
            {
                this.primaryStat = "Intellect";
                //this.classID = 1;
            }
            else if (characterClass.Equals("Frost Guardian"))
            {
                this.primaryStat = "Intellect";
                //this.classID = 1;
            }
            else //if (characterClass.Equals("Thunderlord"))
            {
                this.primaryStat = "Intellect";
                //this.classID = 1;
            }
        }
        else if (characterRace.Equals("Exiled"))
        {
            this.actionBars[6] = 451;
            this.spellList[0] = 451;

            if (characterClass.Equals("Blademaster"))
            {
                this.primaryStat = "Strength";
                this.classID = 0;
                this.actionBars[0] = 0;
                this.spellList[1] = 1;
                this.actionBars[0] = 1;
            }
            else if (characterClass.Equals("Monk"))
            {
                this.primaryStat = "Agility";
                //this.classID = 1;
            }
            else if (characterClass.Equals("Assassin"))
            {
                this.primaryStat = "Agility";
                //this.classID = 1;
            }
            else //if (characterClass.Equals("Siegesmith"))
            {
                this.primaryStat = "Strength";
                //this.classID = 1;
            }
        }
        else
        {
            this.actionBars[6] = 453;
            this.spellList[0] = 453;

            if (characterClass.Equals("Templar"))
            {
                this.primaryStat = "Intellect";
                //this.classID = 1;
            }
            else if (characterClass.Equals("Shadowblade"))
            {
                this.primaryStat = "Agility";
                //this.classID = 1;
            }
            else if (characterClass.Equals("Dragoon"))
            {
                this.primaryStat = "Strength";
                //this.classID = 1;
            }
            else //if (characterClass.Equals("Archmagus"))
            {
                this.primaryStat = "Intellect";
                //this.classID = 1;
            }
        }
    }

    public void levelUp()
    {
        exp = exp - maxexp;
        level++;
        maxexp *= 1.25f;
        //hp += 10;
        primaryStatValue += 10;
        expMultiplier *= 1.1f;

        checkForSpell();

        SaveLoad.Save();
    }

    public void checkForSpell()
    {
        if (raceName.Equals("Primalist"))
        {
            if (className.Equals("Pyromancer"))
            {

            }
            else if (className.Equals("Earth Warden"))
            {

            }
            else if (className.Equals("Frost Guardian"))
            {

            }
            else //if (characterClass.Equals("Thunderlord"))
            {

            }
        }
        else if (raceName.Equals("Exiled"))
        {
            if (className.Equals("Blademaster"))
            {
                learnSpellsBlademaster();
            }
            else if (className.Equals("Monk"))
            {

            }
            else if (className.Equals("Assassin"))
            {

            }
            else //if (characterClass.Equals("Siegesmith"))
            {

            }
        }
        else // draconic
        {
            if (className.Equals("Templar"))
            {

            }
            else if (className.Equals("Shadowblade"))
            {

            }
            else if (className.Equals("Dragoon"))
            {

            }
            else //if (characterClass.Equals("Archmagus"))
            {

            }
        }

    }

    public void learnSpellsBlademaster()
    {
        if (level == 2)
        {
            learnSpell(2);

            learnSpell(3);

            learnSpell(4);
        }
        else if (level == 3)
        {
            learnSpell(5);
            learnSpell(6);
            learnSpell(7);
        }

    }

    public void addToActionBar(int addSpell)
    {
        for (int x = 0; x < 12; x++)
        {
            if (actionBars[x] == 0)
            {
                actionBars[x] = addSpell;
                break;
            }
        }
    }

    public void addToSpellList(int addSpell)
    {
        bool added = false;
        for (int x = 0; x < 50; x++)
        {
            if (spellList[x] == 0)
            {
                spellList[x] = addSpell;
                added = true;
                break;
            }
        }
        if (added == false)
        {
            //when list is full
        }
    }

    public void learnSpell(int learnSpell)
    {
        addToActionBar(learnSpell);
        addToSpellList(learnSpell);
        SaveLoad.Save();
    }

    public void addToInventory(item addItem)
    {
        bool added = false;
        for (int x = 0; x < 40; x++)
        {
            if (inventory[x] == null)
            {
                inventory[x] = addItem;
                added = true;
                break;
            }
        }
        if (added == false)
        {
            addToMailbox(new mail("Inventory Full", "This item has been sent to your mailbox because your inventory was full at the time of stashing.", addItem, 0));
        }
        SaveLoad.Save();
    }

    public bool InventoryFull()
    {
        for (int x = 0; x < inventory.Length; x++)
        {
            if (inventory[x] == null)
            {
                return false;
            }
        }
        return true;
    }

    public bool MailFull()
    {
        for (int x = 0; x < mailbox.Length; x++)
        {
            if (mailbox[x] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void addToMailbox(mail newMail)
    {
        bool added = false;
        for (int x = 0; x < mailbox.Length; x++)
        {
            if (mailbox[x] == null)
            {
                mailbox[x] = newMail;
                added = true;
                youveGotMail = true;
                break;
            }
        }
        if (added == false)
        {
            //when mailbox is full
        }
        SaveLoad.Save();
    }

    public void equipItem(item equipItem)
    {
        if (equipItem.equipable)
        {
            if (equipped[equipItem.equipSlot] == null)
            {
                equipped[equipItem.equipSlot] = equipItem;
            }
            else
            {
                addToInventory(equipped[equipItem.equipSlot]);
                equipped[equipItem.equipSlot] = equipItem;
            }
        }
        else
        {
            //item not equipable
        }
        SaveLoad.Save();
    }

    public void addToLoot(item equipItem)
    {
        for (int x = 0; x < 6; x++)
        {
            if (loot[x] == null)
            {
                loot[x] = equipItem;
                break;
            }
        }
        SaveLoad.Save();
    }

    public void clearLoot()
    {
        for (int x = 0; x < 6; x++)
        {

            loot[x] = null;

        }
        SaveLoad.Save();
    }

    public void unlockJournalEntry(int x)
    {
        if (!journalEntryUnlock[x])
        {
            journalEntryUnlock[x] = true;
        }
    }

    
}
