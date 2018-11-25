using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellbookManager : MonoBehaviour {

    public UnityEngine.UI.Text spellInfoText;
    public UnityEngine.UI.Text ErrorText;
    public UnityEngine.UI.Text AddingButtonText;
    public UnityEngine.UI.Text PageNumberText;
    public UnityEngine.UI.Text detailToggleText;

    public Image AddingButtonImage;

    public GameObject popup;

    public bool actionbarSelected;
    public bool spellbookSelected;

    public bool addingSpell;
    public int spellBeingAdded; //spellID
    public int selectedSpell4Info; //spellID

    public int selectedActionBarSlot;

    public int spellbookPage = 1;

    public ability ability0;
    public ability ability1;
    public ability ability2;
    public ability ability3;
    public ability ability4;
    public ability ability5;
    public ability ability6;
    public ability ability7;
    public ability ability8;
    public ability ability9;
    public ability ability10;
    public ability ability11;

    public ability spellbook0;
    public ability spellbook1;
    public ability spellbook2;
    public ability spellbook3;
    public ability spellbook4;
    public ability spellbook5;
    public ability spellbook6;
    public ability spellbook7;
    public ability spellbook8;
    public ability spellbook9;
    public ability spellbook10;
    public ability spellbook11;
    public ability spellbook12;
    public ability spellbook13;
    public ability spellbook14;
    public ability spellbook15;
    public ability spellbook16;
    public ability spellbook17;

    public bool detailedMode;

    // Use this for initialization
    void Start () {
        spellbookPage = 1;
        refreshActionBars();
        refreshSpellbook();
        PageNumberText.text = "1/5";
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Return();
            }
        }
    }

    public void toggleDetailed()
    {
        if (detailedMode)
        {
            detailedMode = false;
            detailToggleText.text = "Show Detailed Info";
            if (selectedSpell4Info != 0)
            {
                spellInfoText.text = SpellLibrary.SL[selectedSpell4Info].abilityName + "\n" + SpellLibrary.SL[selectedSpell4Info].tooltip;
            }
        }
        else
        {
            detailedMode = true;
            detailToggleText.text = "Show Simple Info";
            if (selectedSpell4Info != 0)
            {
                spellInfoText.text = SpellLibrary.SL[selectedSpell4Info].abilityName + "\n" + SpellLibrary.SL[selectedSpell4Info].detailedInfo();
            }
        }
    }
    
    public void refreshActionBars()
    {
        ability0.refresh();
        ability1.refresh();
        ability2.refresh();
        ability3.refresh();
        ability4.refresh();
        ability5.refresh();
        ability6.refresh();
        ability7.refresh();
        ability8.refresh();
        ability9.refresh();
        ability10.refresh();
        ability11.refresh();
    }

    public void refreshSpellbook()
    {
        spellbook0.refreshSpellBook();
        spellbook1.refreshSpellBook();
        spellbook2.refreshSpellBook();
        spellbook3.refreshSpellBook();
        spellbook4.refreshSpellBook();
        spellbook5.refreshSpellBook();
        spellbook6.refreshSpellBook();
        spellbook7.refreshSpellBook();
        spellbook8.refreshSpellBook();
        spellbook9.refreshSpellBook();
        spellbook10.refreshSpellBook();
        spellbook11.refreshSpellBook();
        spellbook12.refreshSpellBook();
        spellbook13.refreshSpellBook();
        spellbook14.refreshSpellBook();
        spellbook15.refreshSpellBook();
        spellbook16.refreshSpellBook();
        spellbook17.refreshSpellBook();
    }

    public void nextPage()
    {
        if(spellbookPage == 5)
        {
            spellbookPage = 1;
        }
        else
        {
            spellbookPage += 1;
        }
        PageNumberText.text = spellbookPage + "/5";
        refreshSpellbook();
    }

    public void prevPage()
    {
        if (spellbookPage == 1)
        {
            spellbookPage = 5;
        }
        else
        {
            spellbookPage -= 1;
        }
        PageNumberText.text = spellbookPage + "/5";
        refreshSpellbook();
    }

    public void Return()
    {
        SaveLoad.Save();
        SceneManager.LoadScene("Character");
    }

    public void addSpellToActionBar()
    {
        if (!addingSpell)
        {
            if (spellbookSelected)
            {
                UpdateErrorText("Select a slot in your actionbar to replace.");
                addingSpell = true;
                AddingButtonText.text = "Cancel";
                AddingButtonImage.overrideSprite = Resources.Load<Sprite>("Sprites/UI/No");
                popup.SetActive(true);
            }
            else
            {
                UpdateErrorText("You must select a spell from your spellbook to learn.");
            }
        }
        else
        {
            addingSpell = false;
            popup.SetActive(false);
            AddingButtonText.text = "Add Spell";
            AddingButtonImage.overrideSprite = Resources.Load<Sprite>("Sprites/UI/Yes");
        }
    }

    public void removeSpellFromActionBar()
    {
        if (actionbarSelected)
        {
            Game.current.player.actionBars[selectedActionBarSlot] = 0;
            refreshActionBars();
            actionbarSelected = false;
            spellInfoText.text = "";
            SaveLoad.Save();
        }
        else
        {
            UpdateErrorText("You must select a spell from your actionbar to remove.");
        }
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
