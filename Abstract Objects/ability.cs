using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ability : MonoBehaviour {

    public GameObject cdBar;

    public string abilityName;
    public float cooldown;
    public float currentCooldown;
    public float castTime;
    public float resourceCost;
    public bool interrupts;

    public bool interruptable;
    public float currentCastTime;

    public Character self;
    
    public float damage;
    public float heal;
    public float shield;
    public float stun;

    public bool magic;
    public string tooltip;

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
    public float dotTicks;
    public float dotDuration;

    //hot
    public bool isHot;
    public float hotHeal;
    public float hotTicks;
    public float hotDuration;

    //scaling values
    public float weaponDamageScale;
    public float primaryStatScale;
    public float bonusPowerScale;

    //channeled
    public bool isChanneled;
    public float channeledDamage;
    public float channeledTicks;
    public float channeledDuration;

    public bool spellBookMode;
    public bool spellBookAbility;

    private Slider _CDBar;
    public int slot;

    public Image spellIcon;
    public UnityEngine.UI.Text itemInfoBox;

    public CharacterManager CM;
    public SpellbookManager SBM;

    public GameObject resourceShade;
    public bool spellExists;

    //for ai, change later to spell
    public void setAbility(string abilityName1, float cooldown1, float castTime1, float resourceCost1, bool interrupts1, bool interruptable1, float damage1, float heal1, float shield1, float stun1, float wdscale1, float psscale1, float bpscale1)
    {
        abilityName = abilityName1;
        cooldown = cooldown1;
        currentCooldown = cooldown;
        castTime = castTime1;
        resourceCost = resourceCost1;
        interrupts = interrupts1;
        interruptable = interruptable1;
        damage = damage1;
        heal = heal1;
        shield = shield1;
        stun = stun1;

        weaponDamageScale = wdscale1;
        primaryStatScale = psscale1;
        bonusPowerScale = bpscale1;
    }

    public void loadSpell(int spellID)
    {
        Spell copySpell = SpellLibrary.SL[spellID];
        if (copySpell.isBuff)
        {
            isBuff = true;
            buffStats = copySpell.buffStats;
            buffDuration = copySpell.buffDuration;
        }
        if (copySpell.isDebuff)
        {
            isDebuff = true;
            debuffStats = copySpell.debuffStats;
            debuffDuration = copySpell.debuffDuration;
        }
        if (copySpell.isDot)
        {
            isDot = true;
            dotDamage = copySpell.dotDamage;
            dotTicks = copySpell.ticks;
            dotDuration = copySpell.dotDuration;
        }
        if (copySpell.isHot)
        {
            isHot = true;
            hotHeal = copySpell.hotHeal;
            hotTicks = copySpell.hotTicks;
            hotDuration = copySpell.hotDuration;
        }
        if (copySpell.isChanneled)
        {
            isChanneled = true;
            channeledDamage = copySpell.channeledDamage;
            channeledTicks = copySpell.channeledTicks;
            channeledDuration = copySpell.channeledDuration;
        }

        abilityName = copySpell.abilityName;
        cooldown = copySpell.cooldown;
        castTime = copySpell.castTime;
        resourceCost = copySpell.resourceCost;
        interrupts = copySpell.interrupts;
        interruptable = copySpell.interruptable;
        damage = copySpell.damage;
        heal = copySpell.heal;
        shield = copySpell.shield;
        stun = copySpell.stun;
        tooltip = copySpell.tooltip;

        weaponDamageScale = copySpell.weaponDamageScale;
        primaryStatScale = copySpell.primaryStatScale;
        bonusPowerScale = copySpell.bonusPowerScale;

        spellIcon.overrideSprite = Resources.Load<Sprite>(copySpell.spriteLocation);
    }

    public bool spellbookSlotEmpty;
    public void refresh() //used in spellbook
    {
        if (Game.current.player.actionBars[slot] != 0)
        {
            spellbookSlotEmpty = false;
            loadSpell(Game.current.player.actionBars[slot]);
        }
        else
        {
            spellbookSlotEmpty = true;
            spellIcon.overrideSprite = Resources.Load<Sprite>("Spellicons/nospell");
        }
    }

    public bool spellbookSlotEmptySpellBook;
    public void refreshSpellBook()
    {
        int pageOffset = 18 * (SBM.spellbookPage - 1);
        if (Game.current.player.spellList[slot + pageOffset] != 0)
        {
            spellbookSlotEmptySpellBook = false;
            loadSpell(Game.current.player.spellList[slot + (18 * (SBM.spellbookPage - 1))]);
        }
        else
        {
            spellbookSlotEmptySpellBook = true;
            spellIcon.overrideSprite = Resources.Load<Sprite>("Spellicons/nospell");
        }
    }

    // Use this for initialization
    void Start () {
        if (!spellBookMode)
        {
            if (!self.isAI)
            {
                _CDBar = GetComponentInChildren<Slider>();

                if (cdBar != null)
                {
                    cdBar.SetActive(false);
                }
                
                
                if (Game.current.player.actionBars[slot] != 0)
                {
                    spellExists = true;
                    loadSpell(Game.current.player.actionBars[slot]);
                    cooldown *= (1 - (self.cdr / 100)); //cdr
                }
                else
                {
                    spellExists = false;
                }
            }
            currentCooldown = cooldown;
            currentCastTime = 0;
        }
    }

    public void displayInfo()
    {
        itemInfoBox.text = abilityName + "\n" + tooltip;
        CM.currentlySelectedItem = null;
        CM.EquipButton.SetActive(false);
    }

    public void clickedInSpellBook()
    {
        if (SBM.addingSpell)
        {
            SBM.UpdateErrorText("Select a slot in your actionbar to replace.");
        }
        else
        {
            if (!spellbookSlotEmptySpellBook)
            {
                SBM.spellBeingAdded = Game.current.player.spellList[slot + (18 * (SBM.spellbookPage - 1))];
                SBM.selectedSpell4Info = SBM.spellBeingAdded;

                displayDetailedInfo();


                SBM.actionbarSelected = false;
                SBM.spellbookSelected = true;
            }
            else
            {
                SBM.spellBeingAdded = 0;
                SBM.selectedSpell4Info = 0;
                SBM.spellInfoText.text = "";
                SBM.actionbarSelected = false;
                SBM.spellbookSelected = false;
            }
        }
    }

    public void clickedInActionBar()
    {
        if (SBM.addingSpell)
        {
            //delete duplicate first
            for (int x = 0; x < 12; x++)
            {
                if (Game.current.player.actionBars[x] == SBM.spellBeingAdded)
                {
                    Game.current.player.actionBars[x] = 0;
                }
            }

            Game.current.player.actionBars[slot] = SBM.spellBeingAdded;
            SBM.spellInfoText.text = "";
            SBM.addingSpell = false;
            SBM.AddingButtonText.text = "Add Spell";
            SBM.AddingButtonImage.overrideSprite = Resources.Load<Sprite>("Sprites/UI/Yes");
            SBM.spellbookSelected = false;
            SBM.actionbarSelected = false;

            SBM.refreshActionBars();
            SBM.popup.SetActive(false);
            SaveLoad.Save();
        }
        else
        {
            if (!spellbookSlotEmpty)
            {
                SBM.selectedSpell4Info = Game.current.player.actionBars[slot];
                displayDetailedInfo();
                SBM.selectedActionBarSlot = slot;
                SBM.actionbarSelected = true;
                SBM.spellbookSelected = false;
            }
            else
            {
                SBM.spellInfoText.text = "";
                SBM.selectedSpell4Info = 0;
                SBM.actionbarSelected = false;
                SBM.spellbookSelected = false;
            }
        }
    }

    public void displayDetailedInfo()
    {
        if (SBM.detailedMode)
        {
            SBM.spellInfoText.text = abilityName + "\n" + SpellLibrary.SL[SBM.selectedSpell4Info].detailedInfo();
        }
        else
        {
            SBM.spellInfoText.text = abilityName + "\n" + tooltip;
        }
    }

    public void abilityClicked()
    {
        if (!spellExists && !self.isAI)
        {

        }
        else
        {
            if (self.curHP > 0) {
                if (self.enemy.curHP > 0) {

                    if (self.paused)
                    {
                        if (!self.isAI)
                        {
                            self.UpdateErrorText("The game is paused.");
                        }
                    }

                    else
                    {
                        if (self.isStunned)
                        {
                            if (!self.isAI)
                            {
                                self.UpdateErrorText("You are stunned.");
                            }
                        }
                        else
                        {
                            if (self.isCasting)
                            {
                                if (!self.isAI)
                                {
                                    self.UpdateErrorText("You are in the middle of casting.");
                                }
                            }
                            else
                            {
                                if (self.isChanneling)
                                {
                                    self.currentCasted.interruptSelf();
                                    self.currentCasted = null;
                                }

                                if (castTime > 0)
                                {
                                    if (currentCooldown >= cooldown)
                                    {
                                        if (resourceCost > self.resource)
                                        {
                                            if (!self.isAI)
                                            {
                                                self.UpdateErrorText("You don't have enough " + resourceNames[Game.current.player.classID] + ".");
                                            }
                                        }
                                        else
                                        {
                                            self.currentCasted = this;
                                            self.isCasting = true;
                                            StartCoroutine("CastingTick");
                                        }
                                    }
                                    else
                                    {
                                        if (!self.isAI)
                                        {
                                            self.UpdateErrorText("That ability is on cooldown.");
                                        }
                                    }
                                }

                                else
                                {
                                    if (currentCooldown >= cooldown)
                                    {
                                        if (resourceCost > self.resource)
                                        {
                                            if (!self.isAI)
                                            {
                                                self.UpdateErrorText("You don't have enough " + resourceNames[Game.current.player.classID] + ".");
                                            }
                                        }
                                        else
                                        {
                                            abilityUse(damage, heal, shield);
                                        }
                                    
                                    }
                                    else
                                    {
                                        if (!self.isAI)
                                        {
                                            self.UpdateErrorText("That ability is on cooldown.");
                                        }
                                    }
                                }
                            }
                        }
                    }


                }
                else
                {
                    if (!self.isAI)
                    {
                        self.UpdateErrorText("Your opponent is dead.");
                    }
                }

            }

            else
            {
                if (!self.isAI)
                {
                    self.UpdateErrorText("You are dead.");
                }
            }
        }
    }

    public string[] resourceNames = new string[] { "rage", "mana", "steam" };
    // set it up with a class id or something in character save for quick access


    public void useResource()
    {
        if (resourceCost >= 0)
        {
            self.resource -= resourceCost;
        }
        else
        {
            self.resource += (0 - resourceCost);
        }
    }

    public void abilityUse(float damage1, float heal1, float shield1)
    {
        if (interrupts)
        {
            if (self.enemy.isCasting)
            {
                self.enemy.currentCasted.interrupted();
                self.enemy.currentCasted = null;
                //AddEvent message is inside interrupted()
            }
            else
            {
                self.playerLog.AddEvent(self.characterName + "'s [" + abilityName + "] failed to interrupt " + self.enemy.characterName + ".");
            }
        }
        
        if (isBuff)
        {
            buffSelf();
        }
        if (isDebuff)
        {
            debuffEnemy();
        }
        if (isDot)
        {
            dotEnemy();
        }
        if (isHot)
        {
            hotSelf();
        }
        if (isChanneled)
        {
            channelSpell();
        }

        useResource();

        dealDamage(damage1); //stun is in here (because it can be dodged)

        
        if (heal1 > 0)
        {
            healDamage(heal1);
        }

        if (shield1 > 0)
        {
            shield1 += Mathf.RoundToInt(self.bonuspower + (self.primaryStat * 0.2f)); //BP and primstat
            self.playerLog.AddEvent(self.characterName + "'s [" + abilityName + "] shields for " + CurrencyConverter.Instance.convertDamage(shield1) + " damage.");
        }

        currentCooldown = 0;
        if (!self.isAI)
        {
            cdBar.SetActive(true);
            resourceShade.SetActive(true);
        }
        
        StartCoroutine("CooldownTick");

        
        self.shield += shield1;

        self.UpdateCall();
        self.enemy.UpdateCall();
    }

    public void healDamage(float heal1)
    {
        heal1 += Mathf.RoundToInt(
            (self.bonuspower * bonusPowerScale ) +
            (self.primaryStat * primaryStatScale)
            ); //BP and primstat

        self.UpdateStatusText(heal1 + " Healed!");
        self.playerLog.AddEvent(self.characterName + "'s [" + abilityName + "] heals for " + CurrencyConverter.Instance.convertDamage(heal1) + " HP.");
        self.curHP += heal1;

        self.UpdateCall(); // needed?
    }

    public void dealDamage(float damage1)
    {
        if (damage1 > 0)
        {
            damage1 += Mathf.RoundToInt(
                (Mathf.Max(self.weapondamage, 0) * weaponDamageScale) +
                (Mathf.Max(self.bonuspower, 0) * bonusPowerScale) +
                (Mathf.Max(self.primaryStat, 0) * primaryStatScale)
                );
            //weapondamage + primStat + bonusP

            if (magic)
            {
                damage1 -= Mathf.Max(self.enemy.magicres, 0); //mres
                if (damage1 <= 0)
                {
                    damage1 = 1;
                }
            }
            else
            {
                damage1 -= Mathf.Max(self.enemy.armor, 0); //armor
                if (damage1 <= 0)
                {
                    damage1 = 1;
                }
            }

            int dodgeRoll = Random.Range(0, 100);
            if (dodgeRoll <= Mathf.Max(self.enemy.dodge, 0)) //dodge
            {
                self.enemy.UpdateStatusText("Miss!");
                self.playerLog.AddEvent(self.characterName + "'s [" + abilityName + "] missed!");
                self.DPSInfo.UpdateCall();
            }
            else
            {
                
                int critRoll = Random.Range(0, 100);
                if (critRoll < Mathf.Max(self.crit, 0))
                {
                    damage1 = damage1 * 1.5f; //crit
                    self.enemy.UpdateStatusText(CurrencyConverter.Instance.convertDamage(damage1) + " Critical Hit!");
                    self.playerLog.AddEvent(self.characterName + "'s [" + abilityName + "] critically hits for " + CurrencyConverter.Instance.convertDamage(damage1) + " damage.");
                }
                else
                {
                    self.enemy.UpdateStatusText(CurrencyConverter.Instance.convertDamage(damage1) + " Dealt!");
                    self.playerLog.AddEvent(self.characterName + "'s [" + abilityName + "] deals " + CurrencyConverter.Instance.convertDamage(damage1) + " damage.");
                }
                self.totalDamageDealt += damage1;
                self.curHP += (Mathf.Max(self.lifesteal, 0) * damage1) / 100; //lifesteal
                self.DPSInfo.UpdateCall();
                if (stun > 0)
                {
                    if (self.enemy.curHP > 0)
                    {
                        stunEnemy();
                    }

                }
            }
        

            if (self.enemy.shield > 0)
            {
                if (self.enemy.shield > damage1)
                {
                    self.enemy.shield -= damage1;
                }
                else
                {
                    self.enemy.curHP -= damage1 - self.enemy.shield;
                    self.enemy.shield = 0;

                }
            }
            else
            {
                self.enemy.curHP -= damage1;
            }

            self.UpdateCall();
            self.enemy.UpdateCall();
        }
    }




    public void interrupted()
    {   
        if (interruptable)
        {
            if (self.isCasting)
            {
                self.isCasting = false;
                self.UpdateCastBar("", 0, 1);
                self.UpdateInterruptedText("Interrupted!");
                currentCastTime = 0;
                StopCoroutine("CastingTick");
            }
            else if (self.isChanneling)
            {
                self.isChanneling = false;
                self.UpdateChannelBar("", 0, 1);
                self.UpdateInterruptedText("Interrupted!");
                currentChanneledTime = 0;
                StopCoroutine("ChanneledTick");
            }
            self.playerLog.AddEvent(self.enemy.characterName + " successfully interrupts " + self.characterName + "'s [" + abilityName + "]!");
            
        }
        else
        {
            self.UpdateStatusText("Interrupt Failed!");
            self.playerLog.AddEvent(self.enemy.characterName + " failed to interrupt " + self.characterName + "'s [" + abilityName + "]!");
        }
    }

    public void interruptSelf()
    {
        if (self.isCasting)
        {
            self.isCasting = false;
            self.UpdateCastBar("", 0, 1);
            self.UpdateInterruptedText("Interrupted!");
            currentCastTime = 0;
            StopCoroutine("CastingTick");
        }
        else if (self.isChanneling)
        {
            self.isChanneling = false;
            self.UpdateChannelBar("", 0, 1);
            self.UpdateInterruptedText("Interrupted!");
            currentChanneledTime = 0;
            StopCoroutine("ChanneledTick");
        }
        self.playerLog.AddEvent(self.characterName + "'s [" + abilityName + "] interrupted!");
    }

    public void stunEnemy()
    {
        if (self.enemy.currentCasted != null)
        {
            self.enemy.currentCasted.interrupted();
        }

        self.enemy.isStunned = true;
        self.enemy.stunDuration = stun;
        self.enemy.timeStunned = 0;
        self.enemy.StartCoroutine("StunTick");
        self.playerLog.AddEvent(self.characterName + "'s [" + abilityName + "] stuns " + self.enemy.characterName + " for " + stun + " seconds.");
    }

    public bool buffActive;
    public void buffSelf() //can debuff self with negatives
    {
        buffActive = false;
        self.playerLog.AddEvent(self.characterName + " gains the effect of [" + abilityName + "].");
        StartCoroutine("BuffTick");
    }

    public bool debuffActive;
    public void debuffEnemy()
    {
        debuffActive = false;
        self.playerLog.AddEvent(self.enemy.characterName + " is afflicted with [" + abilityName + "].");
        StartCoroutine("DebuffTick");
    }

    public float currentDotTime;
    public float dotInterval;
    public float tickDamage;
    public void dotEnemy()
    {
        currentDotTime = 0;
        dotInterval = dotDuration / (dotTicks + 1); //first tick doesnt do damage
        tickDamage = dotDamage / dotTicks;
        self.playerLog.AddEvent(self.enemy.characterName + " is afflicted with [" + abilityName + "].");
        StartCoroutine("DotTick");
    }

    public float currentHotTime;
    public float hotInterval;
    public float tickHeal;
    public void hotSelf()
    {
        currentHotTime = 0;
        hotInterval = hotDuration / (hotTicks + 1); //first tick doesnt heal
        tickHeal = hotHeal / hotTicks;
        self.playerLog.AddEvent(self.characterName + " gains [" + abilityName + "].");
        StartCoroutine("HotTick");
    }

    public float currentChanneledTime;
    public float channeledInterval;
    public float channeledTickDamage;
    public float nextChanneledTick; // for castbar
    public void channelSpell()
    {
        self.currentCasted = this;
        self.isChanneling = true;
        currentChanneledTime = 0;
        channeledInterval = channeledDuration / (channeledTicks + 1); //first tick doesnt do damage
        nextChanneledTick = channeledInterval;
        channeledTickDamage = channeledDamage / channeledTicks;
        self.playerLog.AddEvent(self.enemy.characterName + " channels [" + abilityName + "].");
        StartCoroutine("ChanneledTick");
    }


    IEnumerator CooldownTick()
    {
        while (true)
        {
            if(currentCooldown < cooldown)
            {
                currentCooldown += 0.1f;
                if (!self.isAI)
                {
                 _CDBar.value = currentCooldown / cooldown * 100;

                if (currentCooldown > cooldown)
                    {
                        _CDBar.value = 0;
                        cdBar.SetActive(false);
                        StopCoroutine("CooldownTick");
                    }

                }
            }
            else
            {
                if (!self.isAI)
                {
                _CDBar.value = 0;
                cdBar.SetActive(false);
                }
                StopCoroutine("CooldownTick");
            }
            yield return new WaitForSeconds(0.1f);
        }
    }



    IEnumerator CastingTick()
    {
        while (true)
        {
            if (currentCastTime < castTime)
            {
                currentCastTime += 0.05f;
                self.UpdateCastBar(abilityName, currentCastTime, castTime);
                //update slider
            }
            else
            {
                abilityUse(damage, heal, shield);
                self.isCasting = false;
                self.currentCasted = null;
                self.UpdateCastBar("", 0, 1);
                currentCastTime = 0;
                StopCoroutine("CastingTick");
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

  
    
    IEnumerator BuffTick()
    {
        while (true)
        {
            if (buffActive == false)
            {
                self.primaryStat += buffStats[0];
                self.stamina += buffStats[1];
                self.armor += buffStats[2];
                self.dodge += buffStats[3];
                self.magicres += buffStats[4];
                self.lifesteal += buffStats[5];
                self.crit += buffStats[6];
                self.bonuspower += buffStats[7];
                self.cdr += buffStats[8];
                self.weapondamage += buffStats[9];
                buffActive = true;
            }
            else
            {
                self.primaryStat -= buffStats[0];
                self.stamina -= buffStats[1];
                self.armor -= buffStats[2];
                self.dodge -= buffStats[3];
                self.magicres -= buffStats[4];
                self.lifesteal -= buffStats[5];
                self.crit -= buffStats[6];
                self.bonuspower -= buffStats[7];
                self.cdr -= buffStats[8];
                self.weapondamage -= buffStats[9];
                buffActive = false;
                self.playerLog.AddEvent(self.characterName + "'s [" + abilityName + "] expires.");
                StopCoroutine("BuffTick");
            }
            yield return new WaitForSeconds(buffDuration);
        }
    }

    IEnumerator DebuffTick()
    {
        while (true)
        {
            if (debuffActive == false)
            {
                self.enemy.armor -= debuffStats[0];
                self.enemy.dodge -= debuffStats[1];
                self.enemy.magicres -= debuffStats[2];
                self.enemy.lifesteal -= debuffStats[3];
                self.enemy.crit -= debuffStats[4];
                self.enemy.weapondamage -= debuffStats[5];
                debuffActive = true;
            }
            else
            {
                self.enemy.armor += debuffStats[0];
                self.enemy.dodge += debuffStats[1];
                self.enemy.magicres += debuffStats[2];
                self.enemy.lifesteal += debuffStats[3];
                self.enemy.crit += debuffStats[4];
                self.enemy.weapondamage += debuffStats[5];
                debuffActive = false;
                self.playerLog.AddEvent(self.enemy.characterName + " is no longer afflicted with [" + abilityName + "].");
                StopCoroutine("DebuffTick");
            }
            yield return new WaitForSeconds(debuffDuration);
        }
    }

    IEnumerator DotTick()
    {
        while (true)
        {
            if (currentDotTime < dotDuration)
            {
                if (currentDotTime == 0)
                {
                    //no damage on start
                }
                else
                {
                    dealDamage(tickDamage);
                }
                currentDotTime += dotInterval;
            }
            else
            {
                currentDotTime = 0;
                self.playerLog.AddEvent(self.enemy.characterName + " is no longer afflicted with [" + abilityName + "].");
                StopCoroutine("DotTick");
            }
            yield return new WaitForSeconds(dotInterval);
        }
    }

    IEnumerator HotTick()
    {
        while (true)
        {
            if (currentHotTime < hotDuration)
            {
                if (currentHotTime == 0)
                {
                    //no damage on start
                }
                else
                {
                    healDamage(tickHeal);
                }
                currentHotTime += hotInterval;
            }
            else
            {
                currentHotTime = 0;
                self.playerLog.AddEvent(self.enemy.characterName + "'s [" + abilityName + "] expires.");
                StopCoroutine("HotTick");
            }
            yield return new WaitForSeconds(hotInterval);
        }
    }

    IEnumerator ChanneledTick()
    {
        while (true)
        {
            if (currentChanneledTime < channeledDuration)
            {
                if (currentChanneledTime == 0)
                {
                    //no damage on first tick
                }
                else
                {
                    if (currentChanneledTime >= nextChanneledTick)
                    {
                        dealDamage(channeledTickDamage);
                        nextChanneledTick += channeledInterval;
                    }
                }
                currentChanneledTime += 0.05f;
                self.UpdateChannelBar(abilityName, currentChanneledTime, channeledDuration);
            }
            else
            {
                self.isChanneling = false;
                self.currentCasted = null;
                self.UpdateChannelBar("", 0, 1);
                currentChanneledTime = 0;
                StopCoroutine("ChanneledTick");
            }
            yield return new WaitForSeconds(0.05f);
        }
    }




    public void updateResourceShade()
    {
        if (spellExists)
        {
            if (self.isStunned)
            {
                resourceShade.SetActive(true);
            }
            else
            {
                if (currentCooldown >= cooldown)
                {
                    if (self.resource >= resourceCost)
                    {
                        resourceShade.SetActive(false);
                    }
                    else
                    {
                        resourceShade.SetActive(true);
                    }
                }
            }
        }
 
    }


}
