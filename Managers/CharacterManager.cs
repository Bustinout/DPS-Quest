using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {

    public UnityEngine.UI.Text ClassText;
    public UnityEngine.UI.Text GoldText;
    public UnityEngine.UI.Text itemInfoText;
    public UnityEngine.UI.Text HPText;

    public UnityEngine.UI.Text primaryStatText;
    public UnityEngine.UI.Text staminaText;
    public UnityEngine.UI.Text armorText;
    public UnityEngine.UI.Text magicDefenseText;
    public UnityEngine.UI.Text dodgeText;
    public UnityEngine.UI.Text criticalHitText;
    public UnityEngine.UI.Text bonusPowerText;
    public UnityEngine.UI.Text weaponDamageText;
    public UnityEngine.UI.Text cdrText;
    public UnityEngine.UI.Text lifestealText;
    public UnityEngine.UI.Text primaryStatTypeText;

    public UnityEngine.UI.Text test1;

    public GameObject popup;
    public GameObject EquipButton;

    public gearSlotButton slot0;
    public gearSlotButton slot1;
    public gearSlotButton slot2;
    public gearSlotButton slot3;
    public gearSlotButton slot4;
    public gearSlotButton slot5;
    public gearSlotButton slot6;
    public gearSlotButton slot7;
    public gearSlotButton slot8;
    public gearSlotButton slot9;
    public gearSlotButton slot10;

    public Character self;
    public Slider _expBar;

    public bool CharacterSelect;

    public item currentlySelectedItem;
    public int currentlySelectedSlot;

    //For CharSelect
    public Game[] saves;
    public int saveNum;
    //public CharacterSave selectedChar;
    public UnityEngine.UI.Text nameText;
    public UnityEngine.UI.Text levelText;

    // Use this for initialization
    void Start () {
        _expBar.value = Game.current.player.exp / Game.current.player.maxexp * 100;
        ClassText.text = Game.current.player.raceName + " " + Game.current.player.className;
        GoldText.text = "Gold: " + CurrencyConverter.Instance.GetCurrencyToString(Game.current.player.gold);
        updateStats();
        if (CharacterSelect == true)
        {
            saves = SaveLoad.savedGames.ToArray();
            setSaveArrayNumber();
            test1.text = "Character " + (saveNum+1).ToString();
            popup.SetActive(false);
        }
        else
        {
            EquipButton.SetActive(false);
        }
        
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                ReturnToMenu();
            }
        }
    }

    public void refreshAll()
    {
        slot0.refresh();
        slot1.refresh();
        slot2.refresh();
        slot3.refresh();
        slot4.refresh();
        slot5.refresh();
        slot6.refresh();
        slot7.refresh();
        slot8.refresh();
        slot9.refresh();
        slot10.refresh();
    }

    public void updateStats()
    {
        primaryStatText.text = CurrencyConverter.Instance.convertDamage(self.primaryStat);
        staminaText.text = CurrencyConverter.Instance.convertDamage(self.stamina);
        armorText.text = CurrencyConverter.Instance.convertDamage(self.armor);
        magicDefenseText.text = CurrencyConverter.Instance.convertDamage(self.magicres);
        dodgeText.text = CurrencyConverter.Instance.convertDamage(self.dodge) + "%";
        criticalHitText.text = CurrencyConverter.Instance.convertDamage(self.crit) + "%";
        bonusPowerText.text = CurrencyConverter.Instance.convertDamage(self.bonuspower);
        weaponDamageText.text = CurrencyConverter.Instance.convertDamage(self.weapondamage);
        cdrText.text = CurrencyConverter.Instance.convertDamage(self.cdr) + "%";
        lifestealText.text = CurrencyConverter.Instance.convertDamage(self.lifesteal) + "%";
        primaryStatTypeText.text = Game.current.player.primaryStat +":";
        float hpCalc = Game.current.player.hp + (Game.current.player.stamina * 10) + (Mathf.RoundToInt(Game.current.player.primaryStatValue * 0.4f) * 10);
        HPText.text = "HP: " + hpCalc + "/" + hpCalc;
    }

    public void UnequipItem()
    {
        if (currentlySelectedItem != null)
        {
            Game.current.player.addToInventory(currentlySelectedItem);
            Game.current.player.equipped[currentlySelectedSlot] = null;
            currentlySelectedItem = null;
            currentlySelectedSlot = 99;
            itemInfoText.text = "";
            self.calcStats();
            updateStats();
            refreshAll();
            EquipButton.SetActive(false);
            SaveLoad.Save();
        }
        else
        {
            //error for nothing equipped
        }

    }




    //Stuff for CharacterSelect
    public void setSaveArrayNumber()
    {
        for (int x = 0; x < saves.Length; x++)
        {
            if (saves[x].favorite == true)
            {
                saveNum = x;
            }
        }
    }

    public void refreshChar()
    {
        CharacterSave selectedChar = saves[saveNum].player;
        nameText.text = selectedChar.characterName;
        _expBar.value = selectedChar.exp / selectedChar.maxexp * 100;
        ClassText.text = selectedChar.raceName + " " + selectedChar.className;
        GoldText.text = "Gold: " + CurrencyConverter.Instance.GetCurrencyToString(selectedChar.gold);

        int displayStat = selectedChar.primaryStatValue;
        int displayStamina = selectedChar.stamina;
        int displayArmor = selectedChar.armor;
        int displayDodge = selectedChar.dodge;
        int displayMagicRes = selectedChar.magicres;
        int displayLifesteal = selectedChar.lifesteal;
        int displayCrit = selectedChar.crit;
        int displayBonusPower = selectedChar.bonuspower;
        int displayCDR = selectedChar.cdr;
        int displayDamage = selectedChar.damage;
        

        foreach (item x in selectedChar.equipped)
        {
            if (x != null)
            {
                displayStat += x.primaryStat;
                displayStamina += x.stamina;
                displayArmor += x.armor;
                displayDodge += x.dodge;
                displayMagicRes += x.magicres;
                displayLifesteal += x.lifesteal;
                displayCrit += x.crit;
                displayBonusPower += x.bonuspower;
                displayCDR += x.cdr;
                displayDamage += x.damage;
            }
        }

        int displayHP = ((int)selectedChar.hp) + (displayStamina * 10) + (Mathf.RoundToInt(displayStat * 0.4f) * 10);

        HPText.text = "HP: " + CurrencyConverter.Instance.convertDamage(displayHP) + "/" + CurrencyConverter.Instance.convertDamage(displayHP);
        primaryStatText.text = CurrencyConverter.Instance.convertDamage(displayStat);
        staminaText.text = CurrencyConverter.Instance.convertDamage(displayStamina);
        armorText.text = CurrencyConverter.Instance.convertDamage(displayArmor);
        magicDefenseText.text = CurrencyConverter.Instance.convertDamage(displayMagicRes);
        dodgeText.text = CurrencyConverter.Instance.convertDamage(displayDodge)+"%";
        criticalHitText.text = CurrencyConverter.Instance.convertDamage(displayCrit) + "%";
        bonusPowerText.text = CurrencyConverter.Instance.convertDamage(displayBonusPower);
        weaponDamageText.text = CurrencyConverter.Instance.convertDamage(displayDamage);
        cdrText.text = CurrencyConverter.Instance.convertDamage(displayCDR) + "%";
        lifestealText.text = CurrencyConverter.Instance.convertDamage(displayLifesteal) + "%";
        primaryStatTypeText.text = selectedChar.primaryStat + ":";
        

        levelText.text = "lvl " + (selectedChar.level).ToString();

        slot0.refreshSelect(selectedChar);
        slot1.refreshSelect(selectedChar);
        slot2.refreshSelect(selectedChar);
        slot3.refreshSelect(selectedChar);
        slot4.refreshSelect(selectedChar);
        slot5.refreshSelect(selectedChar);
        slot6.refreshSelect(selectedChar);
        slot7.refreshSelect(selectedChar);
        slot8.refreshSelect(selectedChar);
        slot9.refreshSelect(selectedChar);
        slot10.refreshSelect(selectedChar);

    }
    

    public void ReturnToMenu()
    {
        SaveLoad.Save();
        SceneManager.LoadScene("Menu");
    }

    public void toInventory()
    {
        SaveLoad.Save();
        SceneManager.LoadScene("Inventory");
    }

    public void toSpellbook()
    {
        SaveLoad.Save();
        SceneManager.LoadScene("Spellbook");
    }

    public void LoadCharacter()
    {
        SaveLoad.loadSave(saves[saveNum]);
    }

    public void openPopup()
    {
        popup.SetActive(true);
    }

    public void closePopup()
    {
        popup.SetActive(false);
    }

    public void DeleteCharacter()
    {
        if (saves.Length == 1) //only one save
        {
            SaveLoad.savedGames.Remove(saves[saveNum]);
            Game.current = null;
            SaveLoad.saveEmpty();
            NewGame();
        }
        else if (saveNum == saves.Length-1) //deleting the last save of array
        {
            SaveLoad.savedGames.Remove(saves[saveNum]);
            if (Game.current != saves[saveNum])
            {
                SaveLoad.Save();
            }
            else
            {
                Game.current = saves[saveNum - 1];
                SaveLoad.deleteSave();
            }

            //display prev character
            saveNum -= 1;
            refreshChar();
            test1.text = "Character " + (saveNum + 1).ToString();

            //set new array
            saves = SaveLoad.savedGames.ToArray();
        }
        else //deleting file
        {
            SaveLoad.savedGames.Remove(saves[saveNum]);
            if (Game.current != saves[saveNum])
            {
                SaveLoad.Save();
            }
            else
            {
                Game.current = saves[saveNum + 1];
                SaveLoad.deleteSave();
            }
            
            //display next character
            saveNum += 1;
            refreshChar();
            saveNum -= 1;

            //set new array
            saves = SaveLoad.savedGames.ToArray();
        }
        popup.SetActive(false);
    }

    public void NewGame()
    {
        SaveLoad.NewGame();
    }

    public void NextCharacter()
    {
        if (saves.Length != 1)
        {
            saveNum = (saveNum + 1) % saves.Length;
            test1.text = "Character " + (saveNum + 1).ToString();
            refreshChar();
        }
    }

    public void PrevCharacter()
    {
        if (saves.Length != 1)
        {
            saveNum = ((saveNum + saves.Length) - 1) % saves.Length;
            test1.text = "Character " + (saveNum + 1).ToString();
            refreshChar();
        }
    }
}
