using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Ghost : MonoBehaviour
{
    private Character self;
    private float multiplier;

    public void enemyInitialize()
    {

        
        self.enemytype = "Normal";
        self.characterName = "Ghost King";

        
        if (Game.current.player.level > 5)
        {
            self.level = 5;
        }
        else
        {
            self.level = Game.current.player.level;
        }

        multiplier = Mathf.Pow(1.2f, self.level);

        self.gold = Mathf.Round(2000 * multiplier);
        self.exp = Mathf.Round(1000 * multiplier);

        self.maxHP = Mathf.Round(1000 * multiplier);
        self.curHP = Mathf.Round(1000 * multiplier);
        self.isAI = true;

        self.NameText.text = self.characterName;
        self.levelText.text = "lvl " + self.level.ToString();
        self.UpdateCall();

        
        
    }

    void Start()
    {

        self = GetComponentInParent<Character>();
        
        self.ability1.setAbility("Melee Strike", 0.5f, 0, 0, false, false, 5, 0, 0, 0, 0f, 0f, 0f);
        self.ability2.setAbility("Shadowbolt", 2f, 2f, 0, false, true, 25, 0, 0, 0, 0f, 0f, 0f);
        self.ability3.setAbility("Shadowstrike", 4f, 0f, 0, false, false, 20, 0, 0, 0, 0f, 0f, 0f);
        self.ability4.setAbility("Dark Rejuvenation", 8f, 1.5f, 0, false, true, 0, 50, 0, 0, 0f, 0f, 0f);
        self.ability5.setAbility("Dark Awakening", 8f, 5f, 0, false, true, 50, 100, 0, 1.5f, 0f, 0f, 0f);

        self.charModel.overrideSprite = Resources.Load<Sprite>("Sprites/EnemyModels/enemy");
        self.BG.overrideSprite = Resources.Load<Sprite>("Sprites/Backgrounds/NightTimeWoods");

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
