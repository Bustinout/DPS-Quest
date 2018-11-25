using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {

    public UnityEngine.UI.Text HPText;
    public UnityEngine.UI.Text StatusText;
    public UnityEngine.UI.Text ErrorText;
    
    public UnityEngine.UI.Text NameText;
    public UnityEngine.UI.Text interruptedText;
    public UnityEngine.UI.Text levelText;
    public Image charModel;
    public Image BG;

    public PlayerLog playerLog;
    public Slider _HPBar;
    public castingBarAnchor CBA;
    public channelingBar CHB;
    public stunBar STUNBAR;
    public Character enemy;

    public string characterName;
    public string enemytype;
    public float gold;
    public float exp;
    
    public int level;

    public float maxHP;
    public float curHP;
    public float shield;
    public float power;

    public bool isAI;

    public bool isCasting = false;
    public bool isChanneling = false;
    public float currentCastTime;
    public float maxCastTime;
    public ability currentCasted;

    public bool isStunned = false;
    public float stunDuration;
    public float timeStunned;

    public float totalDamageDealt;
    public DPS DPSInfo;

    //for AI
    public ability ability1;
    public ability ability2;
    public ability ability3;
    public ability ability4;
    public ability ability5;

    public CharacterManager CharacterManage;

    public bool charPage;
    
    //combat stats
    public int primaryStat;
    public int stamina;
    public int armor;
    public int dodge;
    public int magicres;
    public int lifesteal;
    public int crit;
    public int bonuspower;
    public int cdr;
    public int weapondamage;

    public bool isBattle;
    public float resource;
    public GameObject Blademaster;

    
    public bool paused = false;

    public void calcStats()
    {
        gainStatsFromChar();
        gainStatsFromGear();
        
    }

    public void gainStatsFromGear()
    {
        foreach (item x in Game.current.player.equipped)
        {
            if (x != null)
            {
                primaryStat += x.primaryStat;
                stamina += x.stamina;
                armor += x.armor;
                dodge += x.dodge;
                magicres += x.magicres;
                lifesteal += x.lifesteal;
                crit += x.crit;
                bonuspower += x.bonuspower;
                cdr += x.cdr;
                weapondamage += x.damage;
            }
        }
    }

    public void gainStatsFromChar()
    {

        primaryStat = Game.current.player.primaryStatValue;
        stamina = Game.current.player.stamina;
        armor = Game.current.player.armor;
        dodge = Game.current.player.dodge;
        magicres = Game.current.player.magicres;
        lifesteal = Game.current.player.lifesteal;
        crit = Game.current.player.crit;
        bonuspower = Game.current.player.bonuspower;
        cdr = Game.current.player.cdr;
        weapondamage = Game.current.player.damage;

    }


    // Use this for initialization
    void Start () {
        if (!isAI)
        {
            calcStats();
            NameText.text = Game.current.player.characterName;
            levelText.text = "lvl " + Game.current.player.level.ToString();
            maxHP = Game.current.player.hp + (stamina * 10) + (Mathf.RoundToInt(primaryStat * 0.4f) * 10); //stamina and primstat
            curHP = maxHP;
            if (charPage)
            {
                CharacterManage.updateStats();
            }
            if (isBattle)
            {
                setClass();
            }
        }
        else
        {

            addAIScript();
            maxHP = 5;
            curHP = 5;
            
        }
        totalDamageDealt = 0;
        UpdateCall();
    }

    public void addAIScript()
    {
        int zone = PlayerPrefs.GetInt("Zone");
        int battle = PlayerPrefs.GetInt("Battle");

        if (zone == 0)
        {
            if (battle == 0)
            {
                gameObject.AddComponent<AI_Boar>();
            }
            else if (battle == 1)
            {
                gameObject.AddComponent<AI_Boar>();
            }
            else if (battle == 2)
            {
                gameObject.AddComponent<AI_Boar>();
            }
            else if (battle == 3)
            {
                gameObject.AddComponent<AI_Boar>();
            }
            else
            {
                gameObject.AddComponent<AI_Boar>(); //boss
            }
        }
        
        else if (zone == 1)
        {
            if (battle == 0)
            {
                gameObject.AddComponent<AI_Boar>();
            }
            else if (battle == 1)
            {
                gameObject.AddComponent<AI_Boar>();
            }
            else if (battle == 2)
            {
                gameObject.AddComponent<AI_Boar>();
            }
            else if (battle == 3)
            {
                gameObject.AddComponent<AI_Boar>();
            }
            else
            {
                gameObject.AddComponent<AI_Boar>(); //boss
            }
        }

    }

    
    public void UpdateCall()
    {
        if (curHP > maxHP)
        {
            curHP = maxHP;
        }
        if (curHP <= 0)
        {
            DPSInfo.StopCoroutine("DPSTick");
            curHP = 0;
            if (currentCasted != null)
            {
                currentCasted.interrupted();
            }
            if (isStunned)
            {
                isStunned = false;
                STUNBAR.clearStunBar();
                StopCoroutine("StunTick");
            }
            playerLog.AddEvent(characterName + " has been slain by " + enemy.characterName + ".");
            if (!isAI)
            {
                Game.current.player.deaths += 1; //DEATHS STATS
                SceneManager.LoadScene("BattleEndScreen");
            }
            if (isAI)
            {
                //PlayerPrefs.SetFloat("playerGold", PlayerPrefs.GetFloat("playerGold") + gold);
                PlayerPrefs.SetFloat("expGain", exp);
                PlayerPrefs.SetFloat("goldGain", gold);
                PlayerPrefs.SetInt("battlewon", 1);
                PlayerPrefs.SetInt("EnemyLevel", level);
                PlayerPrefs.SetString("EnemyType", enemytype);
                SceneManager.LoadScene("BattleEndScreen");
            }
            
        }

        if (_HPBar != null)
        {
            _HPBar.value = curHP / maxHP * 100;
        }

        if (shield > maxHP)
        {
            shield = maxHP;
        }
        if (shield > 0)
        {
            HPText.text = "HP: " + CurrencyConverter.Instance.convertDamage(curHP) + "/" + CurrencyConverter.Instance.convertDamage(maxHP) + " +(" + CurrencyConverter.Instance.convertDamage(shield) + ")";
        }
        else
        {
            HPText.text = "HP: " + CurrencyConverter.Instance.convertDamage(curHP) + "/" + CurrencyConverter.Instance.convertDamage(maxHP);
        }

    }

    public void UpdateStatusText(string statusText)
    {
        StopCoroutine("FadeTextToZeroAlpha");
        StatusText.text = statusText;
        StartCoroutine(FadeTextToZeroAlpha(2f, StatusText));
    }

    public void UpdateInterruptedText(string interruptText)
    {
        StopCoroutine("FadeTextToZeroAlpha");
        interruptedText.text = interruptText;
        StartCoroutine(FadeTextToZeroAlpha(2f, interruptedText));
    }

    public void UpdateErrorText(string errorText)
    {
        StopCoroutine("FadeTextToZeroAlpha");
        ErrorText.text = errorText;
        StartCoroutine(FadeTextToZeroAlpha(3f, ErrorText));
    }

    public void UpdateCastBar(string abilityname, float currentCD, float maxCD)
    {
        CBA.UpdateCastBar2(abilityname, currentCD, maxCD);
    }

    public void UpdateChannelBar(string abilityname, float currentChannel, float maxChannel)
    {
        CHB.UpdateChannelBar(abilityname, currentChannel, maxChannel);
    }

    public void UpdateStunBar()
    {
        STUNBAR.UpdateStunBar(timeStunned, stunDuration);
    }

    //credits - LeftyRighty
    //https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/
    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }


    IEnumerator StunTick()
    {
        while (true)
        {
            if (timeStunned < stunDuration)
            {
                timeStunned += 0.05f;
                UpdateStunBar();
            }
            else
            {
                isStunned = false;
                STUNBAR.clearStunBar();
                updateResourceShade();
                if (curHP > 0)
                {
                    playerLog.AddEvent(characterName + " is no longer stunned.");
                }
                StopCoroutine("StunTick");
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    public ability actionbar0;
    public ability actionbar1;
    public ability actionbar2;
    public ability actionbar3;
    public ability actionbar4;
    public ability actionbar5;
    public ability actionbar6;
    public ability actionbar7;
    public ability actionbar8;
    public ability actionbar9;
    public ability actionbar10;
    public ability actionbar11;

    public void updateResourceShade()
    {
        if (!isAI)
        {
            actionbar0.updateResourceShade();
            actionbar1.updateResourceShade();
            actionbar2.updateResourceShade();
            actionbar3.updateResourceShade();
            actionbar4.updateResourceShade();
            actionbar5.updateResourceShade();
            actionbar6.updateResourceShade();
            actionbar7.updateResourceShade();
            actionbar8.updateResourceShade();
            actionbar9.updateResourceShade();
            actionbar10.updateResourceShade();
            actionbar11.updateResourceShade();
        }
    }

    public void setClass()
    {
        if (Game.current.player.classID == 0)
        {
            Blademaster.SetActive(true);
        }
        else //if 
        {

        }
    }

}
