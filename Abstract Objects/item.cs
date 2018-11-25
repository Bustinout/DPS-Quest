using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class item {
    public string itemName;
    public string itemInfo;

    public bool equipable;
    public int equipSlot;
    public string slotName;
    public string spriteLocation;
    /*
     *  0 - helmet
     *  1 - shoulder
     *  2 - chest
     *  3 - bracer
     *  4 - gloves
     *  5 - belt
     *  6 - pants
     *  7 - boots
     *  8 - weapon
     *  9 - relic
     *  10 - necklace
     *  
     */
    public string rarity;
    public int level;
    // common, uncommon, rare, epic,  legendary, ancient,
    // white   green     blue  purple orange    red/
    // 60      21.5      10    5      2.5       1
    // 30      27        24    12     5         2
    public int primaryStat;
    public int stamina;
    public int armor;
    public int dodge;
    public int magicres;
    public int lifesteal;
    public int crit;
    public int bonuspower;
    public int cdr;
    public int damage;
    public int sell;

    public int upgradeLevel; // 0/5

    public item()
    {

    }


    public item(string itemName1, string itemInfo1, bool equipable1, int equipSlot1, string rarity1, int primaryStat1, int stamina1, int armor1, int dodge1, int magicres1, int lifesteal1, int crit1, int bonuspower1, int cdr1, int damage1, int level1, string spriteLocation1, int sell1)
    {
        itemName = itemName1;
        itemInfo = itemInfo1;
        equipable = equipable1;
        equipSlot = equipSlot1;
        rarity = rarity1;
        slotName = getSlotName(equipSlot1);
        primaryStat = primaryStat1;
        stamina = stamina1;
        armor = armor1;
        dodge = dodge1;
        magicres = magicres1;
        lifesteal = lifesteal1;
        crit = crit1;
        bonuspower = bonuspower1;
        cdr = cdr1;
        damage = damage1;
        spriteLocation = spriteLocation1;
        level = level1;
        sell = sell1;
        upgradeLevel = 0;
        
    }

    public void levelup()
    {
        primaryStat = (int) (primaryStat * 1.1);
        stamina = (int)(stamina * 1.1);
        armor = (int)(armor * 1.1);
        dodge = (int)(dodge * 1.1);
        magicres = (int)(magicres * 1.1);
        lifesteal = (int)(lifesteal * 1.1);
        crit = (int)(crit * 1.1);
        bonuspower = (int)(bonuspower * 1.1);
        cdr = (int)(cdr * 1.1);
        damage = (int)(damage * 1.1);
        sell = (int)(sell * 1.1);
        upgradeLevel += 1;
    }

    public item generateCommonItem(int level)
    {
        int generatedSlot = Random.Range(0, 11);
        return generateItem("Common", level, generatedSlot);
    }

    public item generateUncommonItem(int level)
    {
        int generatedSlot = Random.Range(0, 11);
        return generateItem("Uncommon", level, generatedSlot);
    }

    public item generateRareItem(int level)
    {
        int generatedSlot = Random.Range(0, 11);
        return generateItem("Rare", level, generatedSlot);
    }

    public item generateEpicItem(int level)
    {
        int generatedSlot = Random.Range(0, 11);
        return generateItem("Epic", level, generatedSlot);
    }

    public item generateLegendaryItem(int level)
    {
        int generatedSlot = Random.Range(0, 11);
        return generateItem("Legendary", level, generatedSlot);
    }

    public item generateAncientItem(int level)
    {
        int generatedSlot = Random.Range(0, 11);
        return generateItem("Ancient", level, generatedSlot);
    }

    public float[] typeMultipliers = {0.7f, 0.9f,  1f, 0.35f, 0.35f, 0.45f, 0.9f, 0.4f, 1.0f, 0.6f, 0.4f};

    public item generateItem(string rarity, int level, int slot)
    {
        float typeMultiplier = typeMultipliers[slot];
        float rarityMultiplier = 0;

        string itemName1 = "";
        string itemInfo1 = "";

        int armor1 = 0;
        int damage1 = 0;
        string spriteLocation1;

        int[] bonusArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };

        if (rarity.Equals("Common"))
        {
            rarityMultiplier = 1f;
            if (slot < 8)
            {
                int addValue = (int)(level * 8 * typeMultiplier * rarityMultiplier);
                armor1 += addValue;
                armor1 += (int)(addValue * .25 * ((Random.value * 4) - 2));
            }
            if ((slot == 8) | (slot == 9))
            {
                int addValue = (int)(level * 20 * typeMultiplier * rarityMultiplier);
                damage1 += addValue;
                damage1 += (int)(addValue * .25 * ((Random.value * 4) - 2));
            }
            bonusArray = generateBonusStats(0, slot, typeMultiplier, rarityMultiplier, level);
        }

        else if (rarity.Equals("Uncommon"))
        {
            rarityMultiplier = 1.1f;
            if (slot < 8)
            {
                int addValue = (int)(level * 8 * typeMultiplier * rarityMultiplier);
                armor1 += addValue;
                armor1 += (int)(addValue * .25 * ((Random.value * 4) - 2));
            }
            if ((slot == 8) | (slot == 9))
            {
                int addValue = (int)(level * 20 * typeMultiplier * rarityMultiplier);
                damage1 += addValue;
                damage1 += (int)(addValue * .25 * ((Random.value * 4) - 2));
            }
            bonusArray = generateBonusStats(1, slot, typeMultiplier, rarityMultiplier, level);
        }

        else if (rarity.Equals("Rare"))
        {
            rarityMultiplier = 1.25f;
            if (slot < 8)
            {
                int addValue = (int)(level * 8 * typeMultiplier * rarityMultiplier);
                armor1 += addValue;
                armor1 += (int)(addValue * .25 * ((Random.value * 4) - 2));
            }
            if ((slot == 8) | (slot == 9))
            {
                int addValue = (int)(level * 20 * typeMultiplier * rarityMultiplier);
                damage1 += addValue;
                damage1 += (int)(addValue * .25 * ((Random.value * 4) - 2));
            }
            bonusArray = generateBonusStats(2, slot, typeMultiplier, rarityMultiplier, level);
        }

        else if (rarity.Equals("Epic"))
        {
            rarityMultiplier = 1.45f;
            if (slot < 8)
            {
                int addValue = (int)(level * 8 * typeMultiplier * rarityMultiplier);
                armor1 += addValue;
                armor1 += (int)(addValue * .25 * ((Random.value * 4) - 2));
            }
            if ((slot == 8) | (slot == 9))
            {
                int addValue = (int)(level * 20 * typeMultiplier * rarityMultiplier);
                damage1 += addValue;
                damage1 += (int)(addValue * .25 * ((Random.value * 4) - 2));
            }
            bonusArray = generateBonusStats(3, slot, typeMultiplier, rarityMultiplier, level);
        }

        else if (rarity.Equals("Legendary"))
        {
            rarityMultiplier = 1.7f;
            if (slot < 8)
            {
                int addValue = (int)(level * 8 * typeMultiplier * rarityMultiplier);
                armor1 += addValue;
                armor1 += (int)(addValue * .25 * ((Random.value * 4) - 2));
            }
            if ((slot == 8) | (slot == 9))
            {
                int addValue = (int)(level * 20 * typeMultiplier * rarityMultiplier);
                damage1 += addValue;
                damage1 += (int)(addValue * .25 * ((Random.value * 4) - 2));
            }
            bonusArray = generateBonusStats(4, slot, typeMultiplier, rarityMultiplier, level);
        }

        else if (rarity.Equals("Ancient"))
        {
            rarityMultiplier = 2f;
            if (slot < 8)
            {
                int addValue = (int)(level * 8 * typeMultiplier * rarityMultiplier);
                armor1 += addValue;
                armor1 += (int)(addValue * .25 * ((Random.value * 4) - 2));
            }
            if ((slot == 8) | (slot == 9))
            {
                int addValue = (int)(level * 20 * typeMultiplier * rarityMultiplier);
                damage1 += addValue;
                damage1 += (int)(addValue * .25 * ((Random.value * 4) - 2));
            }
            bonusArray = generateBonusStats(5, slot, typeMultiplier, rarityMultiplier, level);
        }

        

        itemName1 = itemSuffix(rarity) + " " + itemMiddleName(slot);
        spriteLocation1 = itemIcon(slot);
        return new item(itemName1, itemInfo1, true, slot, rarity, bonusArray[0], bonusArray[1], armor1, bonusArray[5], bonusArray[2], bonusArray[6], bonusArray[4], bonusArray[7], bonusArray[3], damage1, level, spriteLocation1, (int)(level*10*typeMultiplier*rarityMultiplier));
        //                                                        //primStat,    stamina,       armor,  dodge,         mres,          lifesteal,     crit,          bonuspower,    cdr,           damage 
    }

    public int[] generateBonusStats(int numberOfStats, int slot, float typeMultiplier, float rarityMultiplier, int level)
    {
        int[] returnArray = new int[] {0, 0, 0, 0, 0, 0, 0, 0};
        
        int counter = 0;

        while (counter < numberOfStats)
        {
            int random = Random.Range(0, 8);
            if (random == 0)
            {
                if (returnArray[0] == 0) //primaryStat
                {
                    int addValue = (int)(level * 5 * typeMultiplier * rarityMultiplier);
                    returnArray[0] = Mathf.Max((addValue + (int)(addValue * .25 * ((Random.value * 4) - 2))), 1);
                    counter++;
                }
            }
            else if (random == 1)
            {
                if (returnArray[1] == 0) //stamina
                {
                    int addValue = (int)(level * 3 * typeMultiplier * rarityMultiplier);
                    returnArray[1] = Mathf.Max((addValue + (int)(addValue * .25 * ((Random.value * 4) - 2))), 1);
                    counter++;
                }
            }
            else if ((random == 2) & (slot != 8) & (slot != 9))
            {
                if (returnArray[2] == 0) //magic res
                {
                    int addValue = (int)(level * 1.2 * typeMultiplier * rarityMultiplier);
                    returnArray[2] = Mathf.Max((addValue + (int)(addValue * .25 * ((Random.value * 4) - 2))), 1);
                    counter++;
                }
            }
            else if ((random == 3) & (slot != 2) & (slot != 8) & (slot != 9) & (slot != 6))
            {
                if (returnArray[3] == 0) //cdr
                {
                    int addValue = (int)(5 * typeMultiplier * rarityMultiplier);
                    returnArray[3] = Mathf.Max((addValue + (int)(addValue * .25 * ((Random.value * 4) - 2))), 1);
                    counter++;
                }
            }
            else if ((random == 4) & (slot != 1) & (slot != 2) & (slot != 6))
            {
                if (returnArray[4] == 0) //crit
                {
                    int addValue = (int)(5 * typeMultiplier * rarityMultiplier);
                    returnArray[4] = Mathf.Max((addValue + (int)(addValue * .25 * ((Random.value * 4) - 2))), 1);
                    counter++;
                }
            }
            else if ((random == 5) & (slot != 0) & (slot != 1) & (slot != 2) & (slot != 6) & (slot != 8) & (slot != 9))
            {
                if (returnArray[5] == 0) //dodge
                {
                    int addValue = (int)(5 * typeMultiplier * rarityMultiplier);
                    returnArray[5] = Mathf.Max((addValue + (int)(addValue * .25 * ((Random.value * 4) - 2))), 1);
                    counter++;
                }
            }
            else if ((random == 6) & (slot != 0) & (slot != 1) & (slot != 2) & (slot != 6))
            {
                if (returnArray[6] == 0) //lifesteal
                {
                    int addValue = (int)(3 * typeMultiplier * rarityMultiplier);
                    returnArray[6] = Mathf.Max((addValue + (int)(addValue * .25 * ((Random.value * 4) - 2))), 1);
                    counter++;
                }
            }
            else if ((random == 7) & (slot != 0) & (slot != 1))
            {
                if (returnArray[7] == 0) //bonuspower
                {
                    int addValue = (int)(level * 3 * typeMultiplier * rarityMultiplier);
                    returnArray[7] = Mathf.Max((addValue + (int)(addValue * .25 * ((Random.value * 4) - 2))), 1);
                    counter++;
                }
            }
        }

        return returnArray;
    }


    public string getSlotName(int i)
    {
        if (i == 0)
        {
            return "Helmet";
        }
        else if (i == 1)
        {
            return "Shoulders";
        }
        else if (i == 2)
        {
            return "Chest";
        }
        else if (i == 3)
        {
            return "Bracers";
        }
        else if (i == 4)
        {
            return "Gloves";
        }
        else if (i == 5)
        {
            return "Belt";
        }
        else if (i == 6)
        {
            return "Pants";
        }
        else if (i == 7)
        {
            return "Boots";
        }
        else if (i == 8)
        {
            return "Weapon";
        }
        else if (i == 9)
        {
            return "Relic";
        }
        else
        {
            return "Necklace";
        }
    }

    public string[] commonSuffixes = new string[] { "Abandoned", "Sticky", "Reeking", "Light", "Heavy", "Melted", "Dented", "Counterfeit", "Scratched", "Itchy", "Weak", "Chipped", "Primitive", "Battlescarred" };
    public string[] uncommonSuffixes = new string[] { "Shiny", "Bandit's", "Outlaw's", "Abused", "Unhappy", "Hunting", "Knight's", "Mage's", "Drained", "Magical" };
    public string[] rareSuffixes = new string[] { "Giant's", "Bloody", "Cursed", "Pristine", "Invigorating", "Protector's", "Wizard's", "Enchanted", "Exquisite", "Barbarian's", "Spiked", "Fortified" };
    public string[] epicSuffixes = new string[] { "Tormented", "Holy", "Fading", "Divine", "Damned", "Templar's", "Vengeful", "Wrathful", "Primal", "Savage's", "Righteous", "Imbued", "Bloodthirsty" };
    public string[] legendarySuffixes = new string[] { "Living", "Unleashed", "Unholy", "Invincible", "Pulsating", "Writhing", "Draconic", "Blessed", "Fabled", "Hungering" };
    public string[] ancientSuffixes = new string[] { "Lucifer's", "Whispering", "Busted" };

    public string[] helmetIcons = new string[] { "ItemIcons/Helmets/helmet1", "ItemIcons/Helmets/helmet2", "ItemIcons/Helmets/helmet3", "ItemIcons/Helmets/helmet4", "ItemIcons/Helmets/helmet5" };
    public string[] shoulderIcons = new string[] { "ItemIcons/Shoulders/shoulders1", "ItemIcons/Shoulders/shoulders2", "ItemIcons/Shoulders/shoulders3", "ItemIcons/Shoulders/shoulders4", "ItemIcons/Shoulders/shoulders5" };
    public string[] chestIcons = new string[] { "ItemIcons/Chests/chest1", "ItemIcons/Chests/chest2", "ItemIcons/Chests/chest3", "ItemIcons/Chests/chest4", "ItemIcons/Chests/chest5" };
    public string[] bracerIcons = new string[] { "ItemIcons/Bracers/bracer1" };
    public string[] gloveIcons = new string[] { "ItemIcons/Gloves/shittygloves" };
    public string[] beltIcons = new string[] { "ItemIcons/Belts/belt1" };
    public string[] pantsIcons = new string[] { "ItemIcons/Pants/shittypants" };
    public string[] BootsIcons = new string[] { "ItemIcons/Boots/boots1" };
    public string[] neckIcons = new string[] { "ItemIcons/Necks/neck1", "ItemIcons/Necks/neck2", "ItemIcons/Necks/neck3"};

    public string[] helmetNames = new string[] { "Hat", "Helmet", "Headpiece", "Headdress", "Cap" };
    public string[] shoulderNames = new string[] { "Shoulderguards", "Shoulders", "Shoulderplate", "Pauldron" };
    public string[] chestNames = new string[] { "Armor", "Chestpiece", "Chestpiece", "Chest", "Ribguard", "Mail", "Chainmail", "Plate" };
    public string[] bracerNames = new string[] { "Wrists", "Wristwraps", "Wraps", "Bracers"};
    public string[] gloveNames = new string[] { "Gloves", "Handguards", "Handwraps", "Gauntlets", "Mittens" };
    public string[] beltNames = new string[] { "Belt", "Girdle", "Strap", "Sash", "Waistband" };
    public string[] pantsNames = new string[] { "Legplates", "Pants", "Breeches", "Trousers", "Pantaloons" };
    public string[] bootsNames = new string[] { "Boots", "Shoes", "Footwear", "Greaves" };
    public string[] neckNames = new string[] { "Pendant", "Necklace", "Jewelry", "Choker"};

    //weapon names
    public string[] BMWeaponsNames = new string[] { "Sword", "Longsword", "Slicer", "Claymore", "Slasher" };
    public string[] BMRelicNames = new string[] { "Relic" };

    //weapon icons
    public string[] BMWeaponIcons = new string[] { "ItemIcons/Weapons/Blademaster/weapon1", "ItemIcons/Weapons/Blademaster/weapon2", "ItemIcons/Weapons/Blademaster/weapon3", "ItemIcons/Weapons/Blademaster/weapon4", "ItemIcons/Weapons/Blademaster/weapon5" };
    public string[] BMRelicIcons = new string[] { "ItemIcons/Relics/relic1" };


    //add for other classes and also for relic
    public string[] weaponIcon(int classID)
    {
        string[] listOfIcons;

        if (classID == 0)
        {
            listOfIcons = BMWeaponIcons;
        }
        else //(classID == 1)
        {
            listOfIcons = BMWeaponIcons;
        }

        return listOfIcons;
    }

    public string[] weaponName(int classID)
    {
        string[] listOfNames;

        if (classID == 0)
        {
            listOfNames = BMWeaponsNames;
        }
        else //(classID == 1)
        {
            listOfNames = BMWeaponsNames;
        }

        return listOfNames;
    }

    public string[]relicIcon(int classID)
    {
        string[] listOfIcons;

        if (classID == 0)
        {
            listOfIcons = BMRelicIcons;
        }
        else //(classID == 1)
        {
            listOfIcons = BMRelicIcons;
        }

        return listOfIcons;
    }

    public string[] relicName(int classID)
    {
        string[] listOfNames;

        if (classID == 0)
        {
            listOfNames = BMRelicNames;
        }
        else //(classID == 1)
        {
            listOfNames = BMRelicNames;
        }

        return listOfNames;
    }

    public string itemIcon(int slot)
    {
        string[] listOfIcons;
        if (slot == 0)
        {
            listOfIcons = helmetIcons;
        }
        else if (slot == 1)
        {
            listOfIcons = shoulderIcons;
        }
        else if (slot == 2)
        {
            listOfIcons = chestIcons;
        }
        else if (slot == 3)
        {
            listOfIcons = bracerIcons;
        }
        else if (slot == 4)
        {
            listOfIcons = gloveIcons;
        }
        else if (slot == 5)
        {
            listOfIcons = beltIcons;
        }
        else if (slot == 6)
        {
            listOfIcons = pantsIcons;
        }
        else if (slot == 7)
        {
            listOfIcons = BootsIcons;
        }
        else if (slot == 8)
        {
            listOfIcons = weaponIcon(Game.current.player.classID);
        }
        else if (slot == 9)
        {
            listOfIcons = relicIcon(Game.current.player.classID);
        }
        else
        {
            listOfIcons = neckIcons;
        }
        return listOfIcons[Random.Range(0, listOfIcons.Length)];
    }

    public string itemMiddleName(int slot)
    {
        string[] listOfItemMiddleNames;
        if (slot == 0)
        {
            listOfItemMiddleNames = helmetNames;
        }
        else if (slot == 1)
        {
            listOfItemMiddleNames = shoulderNames;
        }
        else if (slot == 2)
        {
            listOfItemMiddleNames = chestNames;
        }
        else if (slot == 3)
        {
            listOfItemMiddleNames = bracerNames;
        }
        else if (slot == 4)
        {
            listOfItemMiddleNames = gloveNames;
        }
        else if (slot == 5)
        {
            listOfItemMiddleNames = beltNames;
        }
        else if (slot == 6)
        {
            listOfItemMiddleNames = pantsNames;
        }
        else if (slot == 7)
        {
            listOfItemMiddleNames = bootsNames;
        }
        else if (slot == 8)
        {
            listOfItemMiddleNames = weaponName(Game.current.player.classID);
            //for class
        }
        else if (slot == 9)
        {
            listOfItemMiddleNames = relicName(Game.current.player.classID);
        }
        else
        {
            listOfItemMiddleNames = neckNames;
        }
        return listOfItemMiddleNames[Random.Range(0, listOfItemMiddleNames.Length)];
    }

    public string itemSuffix(string rarity)
    {
        string[] listOfSuffixes;
        if (rarity.Equals("Uncommon"))
        {
            listOfSuffixes = uncommonSuffixes;
        }
        else if(rarity.Equals("Rare"))
        {
            listOfSuffixes = rareSuffixes;
        }
        else if (rarity.Equals("Epic"))
        {
            listOfSuffixes = epicSuffixes;
        }
        else if (rarity.Equals("Legendary"))
        {
            listOfSuffixes = legendarySuffixes;
        }
        else if (rarity.Equals("Ancient"))
        {
            listOfSuffixes = ancientSuffixes;
        }
        else
        {
            listOfSuffixes = commonSuffixes;
        }
        return listOfSuffixes[Random.Range(0, listOfSuffixes.Length)];
    }

    public string itemInfoString()
    {
        string returnLine = "";
        returnLine += itemName;
        if (equipable)
        {
            if (upgradeLevel == 0)
            {
                
            }
            else
            {
                returnLine += " (+" + upgradeLevel + ")";
            }
            returnLine += "\n" + "Level " + level + " " + rarity + " " + slotName + "\n";

            if (damage > 0)
            {
                returnLine += "\n" + CurrencyConverter.Instance.convertDamage(damage) + " Weapon Damage";
            }
            if (armor > 0)
            {
                returnLine += "\n" + CurrencyConverter.Instance.convertDamage(armor) + " Armor";
            }
            if (primaryStat > 0)
            {
                returnLine += "\n" + CurrencyConverter.Instance.convertDamage(primaryStat) + " " + Game.current.player.primaryStat;
            }
            if (stamina > 0)
            {
                returnLine += "\n" + CurrencyConverter.Instance.convertDamage(stamina) + " Stamina";
            }
            if (dodge > 0)
            {
                returnLine += "\n" + CurrencyConverter.Instance.convertDamage(dodge) + "% Dodge Chance";
            }
            if (magicres > 0)
            {
                returnLine += "\n" + CurrencyConverter.Instance.convertDamage(magicres) + " Magic Defense";
            }
            if (lifesteal > 0)
            {
                returnLine += "\n" + CurrencyConverter.Instance.convertDamage(lifesteal) + "% Lifesteal";
            }
            if (crit > 0)
            {
                returnLine += "\n" + CurrencyConverter.Instance.convertDamage(crit) + "% Critical Hit Chance";
            }
            if (bonuspower > 0)
            {
                returnLine += "\n" + CurrencyConverter.Instance.convertDamage(bonuspower) + " Bonus Power";
            }
            if (cdr > 0)
            {
                returnLine += "\n" + CurrencyConverter.Instance.convertDamage(cdr) + "% Cooldown Reduction";
            }
            
        }
        if (!itemInfo.Equals(""))
        {
            returnLine += "\n\n\"" + itemInfo+ "\"";
        }
        if (sell > 0)
        {
            returnLine += "\n\nSell Price: " + CurrencyConverter.Instance.GetCurrencyToString(sell);
        }

        return returnLine;
    }

    public item[] beginnerGear()
    {
        item[] beginnerSet = new item[11];

        beginnerSet[0] = new item().generateItem("Common", 1, 0);
        beginnerSet[1] = new item().generateItem("Common", 1, 1);
        beginnerSet[2] = new item().generateItem("Common", 1, 2);
        beginnerSet[3] = new item().generateItem("Common", 1, 3);
        beginnerSet[4] = new item().generateItem("Common", 1, 4);
        beginnerSet[5] = new item().generateItem("Common", 1, 5);
        beginnerSet[6] = new item().generateItem("Common", 1, 6);
        beginnerSet[7] = new item().generateItem("Common", 1, 7);
        beginnerSet[8] = new item().generateItem("Common", 1, 8);
        beginnerSet[9] = new item().generateItem("Common", 1, 9);
        beginnerSet[10] = new item().generateItem("Common", 1, 10);

        return beginnerSet;
    }
}
