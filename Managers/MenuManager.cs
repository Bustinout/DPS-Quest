using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public GameObject mailIcon;
    public GameObject MailWindow;

    public UnityEngine.UI.Text ErrorText;
    public UnityEngine.UI.Text CharName;
    public UnityEngine.UI.Text LevelText;

    void Start()
    {
        SpellLibrary.initializeSpells();
        Debug.Log("loaded/intilizaerd");
        SaveLoad.Load();

        if(Game.current == null)
        {
            SaveLoad.NewGame();
        }
        else
        {
            CharName.text = Game.current.player.characterName;
            LevelText.text = "Lvl " + Game.current.player.level;

            if (Game.current.player.youveGotMail)
            {
                mailIcon.SetActive(true);
            }
        }

        //SaveLoad.Save();
    }

    public void displayMail()
    {
        MailWindow.SetActive(true);
    }

    public void closeMail()
    {
        MailWindow.SetActive(false);
    }

    public void Battle()
    {
        SaveLoad.Save();
        SceneManager.LoadScene("BattleSelect");
    }   

    public void Character()
    {
        SaveLoad.Save();
        SceneManager.LoadScene("Character");
    }

    public void Inventory()
    {
        SaveLoad.Save();
        SceneManager.LoadScene("Inventory");
    }

    public void Shop()
    {
        SaveLoad.Save();
        SceneManager.LoadScene("Shop");
    }

    public void CharacterSelect()
    {
        SaveLoad.Save();
        SceneManager.LoadScene("CharacterSelect");
    }

    public void Journal()
    {
        SaveLoad.Save();
        SceneManager.LoadScene("Journal");
    }

    public void UpdateErrorText(string errorText)
    {
        StopCoroutine("FadeTextToZeroAlpha");
        ErrorText.text = errorText;
        StartCoroutine(FadeTextToZeroAlpha(3f, ErrorText));
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
