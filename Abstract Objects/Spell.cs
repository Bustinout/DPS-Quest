using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spell {

    public string abilityName;
    public float cooldown;
    public float castTime;
    public float resourceCost;
    public bool interrupts;
    public bool interruptable;

    public float damage;
    public float heal;
    public float shield;
    public float stun;

    public string tooltip;
    public string spriteLocation;

    public bool magic;

    //buff
    public bool isBuff;
    public int[] buffStats;
    public float buffDuration;

    //debuff
    public bool isDebuff;
    public int[] debuffStats;
    public float debuffDuration;

    //dot
    public bool isDot;
    public float dotDamage;
    public float ticks;
    public float dotDuration;

    //hot
    public bool isHot;
    public float hotHeal;
    public float hotTicks;
    public float hotDuration;

    //SCALING
    public float weaponDamageScale;
    public float primaryStatScale;
    public float bonusPowerScale;

    //channeled
    public bool isChanneled;
    public float channeledDamage;
    public float channeledTicks;
    public float channeledDuration;

    public Spell()
    {

    }

    //spell no buff
    public Spell(string abilityName1, float cooldown1, float castTime1, float resourceCost1, bool interrupts1, bool interruptable1, float damage1, float heal1, float shield1, float stun1, string tooltip1, string spriteLocation1, bool magic1, float weaponScale1, float primScale1, float bonuspowerScale1)
    {

        abilityName = abilityName1;
        cooldown = cooldown1;
        castTime = castTime1;
        resourceCost = resourceCost1;
        interrupts = interrupts1;
        interruptable = interruptable1;
        damage = damage1;
        heal = heal1;
        shield = shield1;
        stun = stun1;
        tooltip = tooltip1;
        spriteLocation = spriteLocation1;
        isBuff = false;
        isDot = false;
        isDebuff = false;
        isHot = false;
        magic = magic1;

        weaponDamageScale = weaponScale1;
        primaryStatScale = primScale1;
        bonusPowerScale = bonuspowerScale1;
    }

    public void setBuff(int[] inputStats, float inputDuration)
    {
        isBuff = true;
        buffStats = inputStats;
        buffDuration = inputDuration;
    }

    public void setDebuff(int[] inputStats, float inputDuration)
    {
        isDebuff = true;
        debuffStats = inputStats;
        debuffDuration = inputDuration;
    }

    public void setDot(float inputDuration, float inputTicks, float inputDamage)
    {
        isDot = true;
        dotDuration = inputDuration;
        ticks = inputTicks;
        dotDamage = inputDamage;
    }

    public void setHot(float inputDuration, float inputTicks, float inputHeal)
    {
        isHot = true;
        hotDuration = inputDuration;
        hotTicks = inputTicks;
        hotHeal = inputHeal;
    }

    public void setChanneled(float inputDuration, float inputTicks, float inputDamage)
    {
        isChanneled = true;
        channeledDuration = inputDuration;
        channeledTicks = inputTicks;
        channeledDamage = inputDamage;
    }

    public string[] resourceNames = new string[] { "rage", "mana", "steam" };

    public string detailedInfo()
    {
        string returnString = "";
        
        if (resourceCost > 0)
        {
            returnString += "Costs " + resourceCost + " " + resourceNames[Game.current.player.classID] + ".";
        }
        else if (resourceCost < 0)
        {
            returnString += "Generates " + (0 - resourceCost) + " " + resourceNames[Game.current.player.classID] + ".";
        }
        else
        {
            returnString += "No cost.";
        }

        if (castTime > 0)
        {
            returnString += "\n" + castTime + " second cast time.";
            if (!interruptable)
            {
                returnString += "\nCannot be interrupted.";
            }
        }
        returnString += "\n" + cooldown + " second cooldown.";

        if (interrupts)
        {
            returnString += "\nInterrupts Enemy.";
        }
        
        if (damage > 0)
        {
            if (magic)
            {
                returnString += "\nDeals " + damage + " base magic damage.";
            }
            else
            {
                returnString += "\nDeals " + damage + " base physical damage.";
            }
            
        }
        if (heal > 0)
        {
            returnString += "\nHeals for " + heal + " base amount.";
        }
        if (shield > 0)
        {
            returnString += "\nGrants " + shield + " base amount absorbtion.";
        }

        if (isBuff)
        {
            returnString += "\nGive buffs for " + buffDuration + " seconds.";
        }
        if (isDebuff)
        {
            returnString += "\nGive debuffs for " + debuffDuration + " seconds.";
        }
        if (isDot)
        {
            if (magic)
            {
                returnString += "\nDeals " + dotDamage + " base magic damage over " + dotDuration + " seconds.";
            }
            else
            {
                returnString += "\nDeals " + dotDamage + " base physical damage over " + dotDuration + " seconds.";
            }
        }
        if (isHot)
        {
            returnString += "\nHeals for " + hotHeal + " base amount over " + hotDuration + " seconds.";
        }
        if (isChanneled)
        {
            returnString += "\nDeals " + channeledDamage + " base physical damage channeled over " + channeledDuration + " seconds.";
        }
        return returnString;
    }

}
