using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class CharacterCreationManager : MonoBehaviour {

    public GameObject popup;
    public GameObject namepopup;
    public Image raceButton;
    public Image classButton;
    public Image playerAvatar;

    //add image for background?

    public UnityEngine.UI.Text RaceText;
    public UnityEngine.UI.Text ClassText;
    public UnityEngine.UI.Text NameText;

    public UnityEngine.UI.Text infotext;
    public UnityEngine.UI.Text nameInput;
    public UnityEngine.UI.Text ErrorText;

    public string characterName = "";
    public string characterRace = "";
    public string characterClass = "";
    public string characterSprite = "";

    public string[] classes;
    public string[] classButtons;
    public string[] classLore;
    public string[] classSprites;

    public int raceIndex;
    public int classIndex;
    
    public string[] races = new string[] { "Primalist", "Exiled", "Draconic" };
    public string[] Primalistclasses = new string[] {"Pyromancer", "Earth Warden", "Frost Guardian", "Thunderlord"};
    public string[] Exiledclasses2 = new string[] { "Blademaster", "Monk", "Ranger", "Siegesmith" };
    public string[] Draconicclasses = new string[] { "Templar", "Shadowblade", "Dragoon", "Archmagus" };
    //public string[] CultistClasses = new string[] { "Deathspeaker", "Tyrant"};

    public string[] raceButtons = new string[] { "Sprites/UI/Squares/blue", "Sprites/UI/Squares/red", "Sprites/UI/Squares/black" };
    public string[] PrimalistButtons = new string[] { "Sprites/UI/Squares/pyro", "Sprites/UI/Squares/earth", "Sprites/UI/Squares/ice", "Sprites/UI/Squares/thunder" };
    public string[] ExiledButtons = new string[] { "Sprites/UI/Squares/blade", "Sprites/UI/Squares/monk", "Sprites/UI/Squares/ranger", "Sprites/UI/Squares/siege" };
    public string[] DraconicButtons = new string[] { "Sprites/UI/Squares/priest", "Sprites/UI/Squares/shadow", "Sprites/UI/Squares/dragon", "Sprites/UI/Squares/archmagus" };

    public string[] PrimalistSprites = new string[] { "Sprites/PlayerModels/pyromancer", "Sprites/PlayerModels/earthwarden", "Sprites/PlayerModels/frostguardian", "Sprites/PlayerModels/thunderlord" };
    public string[] ExiledSprites = new string[] { "Sprites/PlayerModels/blademaster", "Sprites/PlayerModels/monk", "Sprites/PlayerModels/ranger", "Sprites/PlayerModels/siegesmith" };
    public string[] DraconicSprites = new string[] { "Sprites/PlayerModels/templar", "Sprites/PlayerModels/shadow", "Sprites/PlayerModels/dragoon", "Sprites/PlayerModels/archmagus" };

    public string[] raceLore2 = new string[] {
        "The shamanistic Primalists are a group native to Dresh'nar, gifted with the power of the elements. Since the Draconic invasion, the Primalists have been rallying to reclaim their homelands.",
        "The Exiled are descendants of Primalists who abandoned the ritualistic way of life to pursue technology. The Exiled are skilled in physical combat, often relying on metal rather than magic.",
        "Once a powerful galactic empire, the scattered Draconic have found themselves on Dresh'nar, seeking the key to defeating an ancient evil. The Draconic are extremely competent fighters, gifted both magically and physically."
    };

    public string[] PrimalistLore = new string[] {
        "\nPyromancer lore",
        "\nEarth Warden lore",
        "\nFrost Guardian lore",
        "\nThunderlord lore"
    };

    public string[] ExiledLore = new string[] {
        "\nBlademaster lore",
        "\nMonk lore",
        "\nRanger lore",
        "\nSiegesmith lore"
    };

    public string[] DraconicLore = new string[] {
        "\nTemplar lore",
        "\nShadowblade lore",
        "\nDragoon lore",
        "\nArchmagus lore"
    };

    void Start()
    {
        popup.SetActive(false);
        namepopup.SetActive(false);
        raceIndex = 0;
        classIndex = 0;
        refresh();
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

    public void refresh()
    {
        setRace();
        setClass();

        characterRace = races[raceIndex];
        characterClass = classes[classIndex];
        characterSprite = classSprites[classIndex];
        RaceText.text = characterRace;
        ClassText.text = characterClass;
        

        infotext.text = raceLore2[raceIndex] + "\n" + classLore[classIndex];

        raceButton.overrideSprite = Resources.Load<Sprite>(raceButtons[raceIndex]);
        classButton.overrideSprite = Resources.Load<Sprite>(classButtons[classIndex]);
        //playerAvatar.overrideSprite = Resources.Load<Sprite>(classSprites[classIndex]);
    }

    public void setRace()
    {
        characterRace = races[raceIndex];
        if (raceIndex == 0)
        {
            classes = Primalistclasses;
            classButtons = PrimalistButtons;
            classLore = PrimalistLore;
            classSprites = PrimalistSprites;
        }
        else if (raceIndex == 1)
        {
            classes = Exiledclasses2;
            classButtons = ExiledButtons;
            classLore = ExiledLore;
            classSprites = ExiledSprites;
        }
        else //raceIndex = 2
        {
            classes = Draconicclasses;
            classButtons = DraconicButtons;
            classLore = DraconicLore;
            classSprites = DraconicSprites;
        }
    }

    public void nextRace()
    {
        raceIndex = (raceIndex + 1) % races.Length;
        classIndex = 0;
        refresh();
    }

    public void nextClass()
    {
        classIndex = (classIndex + 1) % classes.Length;
        refresh();
    }

    public void prevRace()
    {
        raceIndex = ((raceIndex - 1)+races.Length)%races.Length;
        classIndex = 0;
        refresh();
    }

    public void prevClass()
    {
        classIndex = ((classIndex - 1)+ classes.Length)% classes.Length;
        refresh();
    }

    public void setClass()
    {
        characterClass = classes[classIndex];
    }

    public void openPopup()
    {
        if (!characterName.Equals(""))
        {
            popup.SetActive(true);
        }
        else
        {
            UpdateErrorText("You must enter a name.");
        }
        
    }

    public void closePopup()
    {
        popup.SetActive(false);
    }

    public void openNamePopup()
    {
        namepopup.SetActive(true);
    }

    public void closeNamePopup()
    {
        namepopup.SetActive(false);
    }

    public void confirmName()
    {
        if (nameEligibility(nameInput.text))
        {
            characterName = nameInput.text;
            NameText.text = characterName;
            closeNamePopup();
        }
        else
        {
        }
        
    }

    public bool nameEligibility(string nameToCheck)
    {
        if (nameToCheck.Length > 12)
        {
            UpdateErrorText("Your name length must be under 13 characters longs.");
            return false;
        }
        if (nameToCheck.Length < 1){
            UpdateErrorText("You must enter a name.");
            return false;
        }
        if (nameToCheck.Equals(" "))
        {
            UpdateErrorText("Invalid name.");
            return false;
        }
            return true;
    }

    public void createCharacter()
    {
            Game.current = new Game(new CharacterSave(characterName, characterClass, characterRace, characterSprite));
            Game.current.player.equipped = new item().beginnerGear();

            SaveLoad.savedGames.Add(Game.current);
            SaveLoad.setFavorite();
            SaveLoad.saveToDeleteOnSave = Game.current;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/DPSQuest.save"); //you can call it anything you want
            bf.Serialize(file, SaveLoad.savedGames);
            file.Close();

            SceneManager.LoadScene("Menu");
            // or tutorial mode
    }

    public void ReturnToMenu()
    {
        if (Game.current != null)
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            UpdateErrorText("You must create a character first.");
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
