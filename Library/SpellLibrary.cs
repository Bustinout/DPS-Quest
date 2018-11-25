using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellLibrary : MonoBehaviour {

    public static Spell[] SL = new Spell [455];
    public static int poopa;

    public static Spell temp;
    public static int[] tempArray;

	// Use this for initialization
	void Start () {

    }

    public static void initializeSpells()
    {
        SL[0] = null;
        SL[1] = new Spell("Furious Slash", 1f, 0f, -10f, false, false, 150, 0f, 0f, 0f, "Slash at the target, dealing damage.", "Spellicons/slashingattack", false, 1f, 0.5f, 0.5f);
        SL[2] = new Spell("Piercing Strike", 5f, 1f, 20f, false, true, 300, 0f, 0f, 0f, "Focus intently and stab at the target, dealing heavy damage.", "Spellicons/piercingattack", false, 1f, 0.6f, 0.5f);
            temp = new Spell("Rejuvenating Anger", 10f, 0f, 0f, false, false, 0f, 0f, 0f, 0f, "Fuel your life with rage.", "Spellicons/rejuvenatinganger", false, 0f, 0f, 0f);
            temp.setHot(5, 5, 100);
        SL[3] = temp;
            temp = new Spell("Triple Strike", 10f, 0f, 20f, false, true, 0f, 0f, 0f, 0f, "Strike at the target three times at lightning speed.", "Spellicons/lightningstrikes", false, 1.1f, 0.6f, 0.5f);
            temp.setChanneled(0.7f, 3, 200);
        SL[4] = temp;
            temp = new Spell("Bleeding Slash", 10f, 0f, 15f, false, false, 0, 0f, 0f, 0f, "Slash at the target, causing them to bleed.", "Spellicons/bleedattack", false, 0.2f, 0.2f, 0.2f);
            temp.setDot(5, 5, 150);
        SL[5] = temp;
            temp = new Spell("WarCry", 10f, 0.5f, 0f, false, false, 0f, 50, 0f, 0f, "Crazy buffs.", "Spellicons/Halting Breath", false, 0f, 0f, 0f);
            tempArray = new int[] { 500, 500, 500, 500, 500, 500, 500, 500, 500, 500 };
            temp.setBuff(tempArray, 10);
        SL[6] = temp;
            temp = new Spell("Debuff Move", 10f, 0.5f, 0f, false, false, 0f, 0f, 0f, 0f, "Crazy buffs.", "Spellicons/Halting Breath", false, 0f, 0f, 0f);
            tempArray = new int[] { 500, 500, 500, 500, 500, 500, 500, 500, 500, 500 };
            temp.setDebuff(tempArray, 10);
        SL[7] = temp;


        SL[451] = new Spell("Kill Move", 1f, 0f, 0f, false, false, 200000, 0f, 0f, 0f, "Kill the guy.", "Spellicons/eyeofdeath", true, 0f, 0f, 0f); //replace with a racial move
        SL[452] = new Spell("Kill Move", 1f, 0f, 0f, false, false, 200000, 0f, 0f, 0f, "Kill the guy.", "Spellicons/eyeofdeath", true, 0f, 0f, 0f); //replace with a racial move
        SL[453] = new Spell("Kill Move", 1f, 0f, 0f, false, false, 200000, 0f, 0f, 0f, "Kill the guy.", "Spellicons/eyeofdeath", true, 0f, 0f, 0f); //replace with a racial move
    }

}
