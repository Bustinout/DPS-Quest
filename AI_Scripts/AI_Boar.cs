using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Boar : MonoBehaviour {

    private Character self;
    private float multiplier;
    private float combatMultipler;

    public void enemyInitialize()
    {


        self.enemytype = "Normal";
        self.characterName = "Boar";


        if (Game.current.player.level > 5)
        {
            self.level = 5;
        }
        else
        {
            self.level = Game.current.player.level;
        }

        multiplier = Mathf.Pow(1.2f, self.level);
        combatMultipler = Mathf.Pow(1.8f, self.level);

        
        self.primaryStat = (int) (40 * combatMultipler);
        self.stamina = (int) (5 * combatMultipler);
        self.armor = (int) (5 * combatMultipler);
        self.dodge = 3;
        self.magicres = 0;
        self.lifesteal = 0;
        self.crit = 5;
        self.bonuspower = (int) (40 * combatMultipler);
        self.cdr = 0;
        self.weapondamage = (int) (40 * combatMultipler);
        


        float goldValue = 80 * multiplier;
        goldValue += Mathf.Round((float)(goldValue * .25 * ((Random.value * 4) - 2)));
        self.gold = goldValue;
        self.exp = Mathf.Round(100 * multiplier);

        self.maxHP = Mathf.Round(500 * multiplier * 2);
        self.curHP = self.maxHP;
        self.isAI = true;

        self.NameText.text = self.characterName;
        self.levelText.text = "lvl " + self.level.ToString();
        self.UpdateCall();



    }

    void Start()
    {
        Game.current.player.monsterCardUnlock[0] = true;
        self = GetComponentInParent<Character>();

        self.ability1.setAbility("Headbutt", 1, 0, 0, false, false, 80, 0, 0, 0, 0f, 0f, 0f);
        self.ability2.setAbility("Gore", 8, 0, 0, false, true, 100, 0, 0, 0, 0f, 0f, 0f);
        self.ability3.setAbility("Charge", 4, 2, 0, false, false, 100, 0, 0, 1, 0f, 0f, 0f);
        self.ability4.setAbility("Ground Stomp", 8f, 0.5f, 0, false, true, 80, 0, 0, 1.5f, 0f, 0f, 0f);
        self.ability5.setAbility("Eat Berries", 8f, 2, 0, false, true, 0, 150, 0, 0, 0f, 0f, 0f);

        self.charModel.overrideSprite = Resources.Load<Sprite>("Sprites/EnemyModels/boar");
        self.BG.overrideSprite = Resources.Load<Sprite>("Sprites/Backgrounds/jungle");

        enemyInitialize();

        StartCoroutine("AITick");

    }

    IEnumerator AITick()
    {
        while (true)
        {
            if (self.curHP > 0)
            {
                float randAbility = Random.value * 100;
                if (randAbility < 20)
                {
                    self.ability1.abilityClicked();
                }
                else if (randAbility < 40)
                {
                    self.ability2.abilityClicked();
                }
                else if (randAbility < 60)
                {
                    self.ability3.abilityClicked();
                }
                else if (randAbility < 80)
                {
                    self.ability4.abilityClicked();
                }
                else
                {
                    self.ability5.abilityClicked();
                }


            }
            else
            {

                StopCoroutine("AITick");
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
